using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Server.Common;
using Server.Models.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(request);
            await _userManager.CreateAsync(newUser, request.Password);
        }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Username)
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

    public record RegisterCommand(
        string Username,
        string Email,
        string Password,
        string ConfirmPassword) : IRequest;
}
