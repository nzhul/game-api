using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Common;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Data;
using Server.Data.Users;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Me
{
    public class BlockUserHandler : IRequestHandler<BlockUserCommand>
    {
        private readonly DataContext _context;
        private readonly ISessionData _sessionData;

        public BlockUserHandler(
            DataContext context,
            ISessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public async Task Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var me = await _context.Users.FirstOrDefaultAsync(u => u.Id == _sessionData.UserId, cancellationToken);
            var userToBlock = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UsernameToBlock, cancellationToken) ??
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));

            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.SenderId == me.Id && f.RecieverId == userToBlock.Id) || (f.SenderId == userToBlock.Id && f.RecieverId == me.Id), cancellationToken);

            if (friendship == null)
            {
                friendship = new Friendship
                {
                    SenderId = me.Id,
                    Sender = me,
                    Reciever = userToBlock,
                    RecieverId = userToBlock.Id,
                    State = FriendshipState.Blocked,
                    RequestTime = DateTime.UtcNow
                };

                me.SendFriendRequests.Add(friendship);
                userToBlock.RecievedFriendRequests.Add(friendship);
                _context.Friendships.Add(friendship);
            }

            friendship.State = FriendshipState.Blocked;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(r => r.UsernameToBlock)
                .NotEmpty()
                .MinimumLength(Constants.MinUsernameLength)
                .MaximumLength(Constants.MaxUsernameLength)
                .Matches(Constants.UsernameRegex);
        }
    }

    public record BlockUserCommand(string UsernameToBlock) : IRequest;
}
