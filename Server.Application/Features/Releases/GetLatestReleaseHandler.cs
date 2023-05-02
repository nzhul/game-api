using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models.Releases;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Releases
{
    public class GetLatestReleaseHandler : IRequestHandler<GetLatestReleaseQuery, Release>
    {
        private readonly DataContext _context;

        public GetLatestReleaseHandler(DataContext context)
        {
            _context = context;
        }

        public Task<Release> Handle(GetLatestReleaseQuery request, CancellationToken cancellationToken)
        {
            return _context.Releases
                .Where(r => r.IsPublic && r.Type == request.ReleaseType)
                .OrderByDescending(r => r.ReleaseDate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetLatestReleaseValidator : AbstractValidator<GetLatestReleaseQuery>
    {
        public GetLatestReleaseValidator()
        {
            RuleFor(x => x.ReleaseType)
                .IsInEnum();
        }
    }

    public record GetLatestReleaseQuery(ReleaseType ReleaseType) : IRequest<Release>;
}
