using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Data.Users;
using System.Collections.Generic;
using System;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Server.Common;
using Server.Application.Features.Users.Models;

namespace Server.Application.Features.Auth
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LoginHandler(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

        public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var dbUser = await _userManager.FindByNameAsync(query.Username);

            if (dbUser == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized,
                        new RestError(RestErrorCode.Unauthorized, nameof(User),
                        $"Invalid username or password"));

                // I am intentionally throwing unauthorized instead of NotFound for security reasons.
                //throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(dbUser, query.Password, false);

            if (loginResult.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == query.Username.ToUpper(),
                        cancellationToken: cancellationToken);

                var userToReturn = _mapper.Map<UserListDto>(appUser);

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
