using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Common.Enums;
using Server.Common.Errors;
using Server.Common.Exceptions;
using Server.Data;
using Server.Data.Users;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Users
{
    public class SetOnline : IRequestHandler<SetOnlineCommand>
    {
        private readonly DataContext _context;

        public SetOnline(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(SetOnlineCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ??
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));

            dbUser.ActiveConnection = request.ConnectionId;
            dbUser.OnlineStatus = 1;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class SetOnlineCommandValidator : AbstractValidator<SetOnlineCommand>
    {
        public SetOnlineCommandValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.ConnectionId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public record SetOnlineCommand(int UserId, int ConnectionId) : IRequest;
}
