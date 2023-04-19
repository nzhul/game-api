using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Common;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Data;
using Server.Data.Users;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Me
{
    public class ApproveFriendRequestHandler : IRequestHandler<ApproveFriendRequestCommand>
    {
        private readonly DataContext _context;
        private readonly ISessionData _sessionData;

        public ApproveFriendRequestHandler(
            DataContext context,
            ISessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public async Task Handle(ApproveFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var dbFriendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.Id == request.RequestId && f.RecieverId == _sessionData.UserId, cancellationToken: cancellationToken) ??
                    throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(Friendship), "Not Found"));

            if (dbFriendship.State == FriendshipState.Approved)
            {
                throw new RestException(HttpStatusCode.BadRequest, new RestError(RestErrorCode.BadArgument, nameof(Friendship), "Friend request already approved!"));
            }

            dbFriendship.State = FriendshipState.Approved;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class ApproveFriendRequestCommandValidator : AbstractValidator<ApproveFriendRequestCommand>
    {
        public ApproveFriendRequestCommandValidator()
        {
            RuleFor(r => r.RequestId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public record ApproveFriendRequestCommand(int RequestId) : IRequest;
}
