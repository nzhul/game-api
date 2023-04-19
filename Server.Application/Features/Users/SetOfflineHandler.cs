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
    public class SetOfflineHandler : IRequestHandler<SetOfflineCommand>
    {
        private readonly DataContext _context;

        public SetOfflineHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(SetOfflineCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ??
                throw new RestException(HttpStatusCode.NotFound, new RestError(RestErrorCode.BadArgument, nameof(User), "Not Found"));

            dbUser.ActiveConnection = -1;
            dbUser.OnlineStatus = 0;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class SetOfflineCommandValidator : AbstractValidator<SetOfflineCommand>
    {
        public SetOfflineCommandValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public record SetOfflineCommand(int UserId) : IRequest;
}
