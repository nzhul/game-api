using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Server.Common;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Common.Mappings;
using Server.Data.Users;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(
            UserManager<User> userManager,
            IMapper mapper,
            ILogger<RegisterHandler> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded && result.Errors.Any())
            {
                var errors = result.Errors.Select(e =>
                    new RestError(e.Code, nameof(User), e.Description));

                throw new RestException(HttpStatusCode.BadRequest,
                    new RestError(
                        RestErrorCode.UserAlreadyExists,
                        nameof(User),
                        "Duplicated user",
                        errors.ToArray())
                    );
            }

            _logger.LogInformation($"New Registration with username: `{request.Username}`!");
        }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .MinimumLength(Constants.MinUsernameLength)
                .MaximumLength(Constants.MaxUsernameLength)
                .Matches(Constants.UsernameRegex)
                .WithMessage(Constants.UsernameValidationError);

            RuleFor(r => r.Email)
                .NotEmpty()
                .Matches(Constants.EmailRegexPattern)
                .WithMessage("Invalid email!");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(Constants.MinPasswordLength)
                .MaximumLength(Constants.MaxPasswordLength);

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .Equal(r => r.Password)
                .WithMessage("Passwords must match!");
        }
    }

    // I am not using record, because records does not have parameterless constructor,
    // this is a problem for MappingProfile.cs line 31
    public class RegisterCommand : IRequest, IMapTo<User>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
