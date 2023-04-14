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

namespace Server.Application.Features.Users
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

        public SendFriendRequestHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.SenderId, cancellationToken);
            var reciever = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.ReceiverUsername, cancellationToken);

            if (reciever == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));
            }

            var alreadyExist = await _context.Friendships
                .AnyAsync(f => f.SenderId == sender.Id && f.RecieverId == reciever.Id, cancellationToken);

            if (alreadyExist)
            {
                throw new RestException(HttpStatusCode.BadRequest, new RestError(RestErrorCode.BadArgument, nameof(User), "Friend request already sent!"));
            }

            Friendship newFriendship = new Friendship
            {
                SenderId = sender.Id,
                Sender = sender,
                Reciever = reciever,
                RecieverId = reciever.Id,
                State = FriendshipState.Pending,
                RequestTime = DateTime.UtcNow
            };

            sender.SendFriendRequests.Add(newFriendship);
            reciever.RecievedFriendRequests.Add(newFriendship);
            _context.Friendships.Add(newFriendship);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
    {
        public SendFriendRequestCommandValidator()
        {
            RuleFor(r => r.SenderId)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.ReceiverUsername)
                .NotEmpty()
                .MinimumLength(Constants.MinUsernameLength)
                .MaximumLength(Constants.MaxUsernameLength)
                .Matches(Constants.UsernameRegex);
        }
    }

    public record SendFriendRequestCommand(int SenderId, string ReceiverUsername) : IRequest;
}
