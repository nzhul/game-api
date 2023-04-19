using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Application.Features.Users.Models;
using Server.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Users
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, ListResult<UserListDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetUsersHandler(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListResult<UserListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken: cancellationToken);

            query = query
                .Skip(request.Skip)
                .Take(request.Take)
                .AsQueryable();

            var projected = await query
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ListResult<UserListDto>()
            {
                Count = totalCount,
                Items = projected
            };
        }
    }



    public class GetUsersQuery : ListingParams, IRequest<ListResult<UserListDto>>
    {
    }
}
