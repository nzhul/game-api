using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Games
{
    public class EndGameHandler : IRequestHandler<EndGameCommand>
    {
        public async Task Handle(EndGameCommand request, CancellationToken cancellationToken)
        {
            // TODO: Add logging
            //throw new System.NotImplementedException();
            return;
        }
    }

    public class EndGameCommandValidator : AbstractValidator<EndGameCommand>
    {
        public EndGameCommandValidator()
        {
            RuleFor(r => r.GameId)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.WinnerId)
                .GreaterThanOrEqualTo(1);
        }
    }

    public record EndGameCommand(int GameId, int WinnerId) : IRequest;
}
