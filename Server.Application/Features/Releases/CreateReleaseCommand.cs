using FluentValidation;
using MediatR;
using Server.Common;
using Server.Data;
using Server.Data.Models.Releases;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Releases
{
    public class CreateReleaseCommandHandler : IRequestHandler<CreateReleaseCommand>
    {
        private readonly DataContext _context;

        public CreateReleaseCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            // TODO: Use System.Version.Parse(request.Version) to validate the version.
            // https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
            // TODO: Do not allow lower versions to be inserted in database.

            var release = new Release()
            {
                Version = request.Version,
                DownloadUrl = request.DownloadUrl,
                Type = request.Type,
                IsPublic = true,
                ReleaseDate = DateTime.UtcNow,
                ReleaseNotes = request.ReleaseNotes ?? "n/a"
            };

            _context.Releases.Add(release);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class CreateReleaseCommandValidator : AbstractValidator<CreateReleaseCommand>
    {
        public CreateReleaseCommandValidator()
        {
            RuleFor(r => r.Version)
                .NotEmpty()
                .Matches(Constants.VersionRegex);
            RuleFor(r => r.DownloadUrl)
                .NotEmpty()
                .Matches(Constants.URLRegex)
                .MinimumLength(10)
                .MaximumLength(2000);
            RuleFor(r => r.Type)
                .IsInEnum();
        }
    }

    public record CreateReleaseCommand(string Version, string DownloadUrl, ReleaseType Type, string ReleaseNotes) : IRequest;
}
