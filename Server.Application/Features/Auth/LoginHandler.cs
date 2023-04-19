using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Server.Application.Features.Users.Models;
using Server.Common;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Data.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Auth
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config,
            IMapper mapper,
            ILogger<LoginHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await _userManager.FindByNameAsync(request.Username);

            if (dbUser == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized,
                        new RestError(RestErrorCode.Unauthorized, nameof(User),
                        $"Invalid username or password"));

                // I am intentionally throwing unauthorized instead of NotFound for security reasons.
                //throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(dbUser, request.Password, false);

            if (loginResult.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == request.Username.ToUpper(), cancellationToken);

                var userToReturn = _mapper.Map<UserListDto>(appUser);

                _logger.LogInformation($"`{request.Username}` generated auth token!");

                return new LoginResult(await GenerateJwtToken(appUser), userToReturn);
            }

            throw new RestException(HttpStatusCode.Unauthorized,
                new RestError(RestErrorCode.Unauthorized, nameof(User),
                $"Invalid username or password"));
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .MinimumLength(Constants.MinUsernameLength)
                .MaximumLength(Constants.MaxUsernameLength)
                .Matches(Constants.UsernameRegex);

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(Constants.MinPasswordLength)
                .MaximumLength(Constants.MaxPasswordLength);
        }
    }

    public record LoginQuery(string Username, string Password) : IRequest<LoginResult>;

    public record LoginResult(string TokenString, UserListDto User);
}
