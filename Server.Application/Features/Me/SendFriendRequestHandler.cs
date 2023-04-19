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
    /// <summary>
    /// Creates new Friendship entity and set its status to None
    /// </summary>
    /// <param name="senderId">The one who sends the friend request</param>
    /// <param name="recieverUsernameOrEmail">The one who will recieve the friend request</param>
    /// <returns>Null if success. Error message on fail</returns>
    public class SendFriendRequestHandler : IRequestHandler<SendFriendRequestCommand>
    {
        private readonly DataContext _context;
        private readonly ISessionData _sessionData;

        public SendFriendRequestHandler(
            DataContext context,
            ISessionData sessionData)
        {
            _context = context;
            _sessionData = sessionData;
        }

        public async Task Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var me = await _context.Users.FirstOrDefaultAsync(u => u.Id == _sessionData.UserId, cancellationToken);
            var reciever = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.ReceiverUsername, cancellationToken) ??
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));

            var alreadyExist = await _context.Friendships
                .AnyAsync(f => f.SenderId == me.Id && f.RecieverId == reciever.Id, cancellationToken);

            if (alreadyExist)
            {
                throw new RestException(HttpStatusCode.BadRequest, new RestError(RestErrorCode.BadArgument, nameof(User), "Friend request already sent!"));
            }

            var newFriendship = new Friendship
            {
                SenderId = me.Id,
                Sender = me,
                Reciever = reciever,
                RecieverId = reciever.Id,
                State = FriendshipState.Pending,
                RequestTime = DateTime.UtcNow
            };

            me.SendFriendRequests.Add(newFriendship);
            reciever.RecievedFriendRequests.Add(newFriendship);
            _context.Friendships.Add(newFriendship);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
    {
        public SendFriendRequestCommandValidator()
        {
            RuleFor(r => r.ReceiverUsername)
                .NotEmpty()
                .MinimumLength(Constants.MinUsernameLength)
                .MaximumLength(Constants.MaxUsernameLength)
                .Matches(Constants.UsernameRegex);
        }
    }

    public record SendFriendRequestCommand(string ReceiverUsername) : IRequest;
}
