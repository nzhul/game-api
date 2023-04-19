using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Common.Mappings;
using Server.Data;
using Server.Data.Users;
using System;
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
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ??
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));

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

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public int GameId { get; set; }

        public Guid? BattleId { get; set; }

    }
}
