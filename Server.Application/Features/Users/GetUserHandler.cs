using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Application.Features.Users.Models;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Common.Mappings;
using Server.Data;
using Server.Data.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Users
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDetailedDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetUserHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDetailedDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (dbUser == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));
            }

            return _mapper.Map<UserDetailedDto>(dbUser);
        }
    }

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public record GetUserQuery(int UserId) : IRequest<UserDetailedDto>;

    public class UserDetailedDto : IMapFrom<User>
    {
        public int Id { get; set; }

        public int MMR { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }

        public int GameId { get; set; }

        public Guid? BattleId { get; set; }

        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}
