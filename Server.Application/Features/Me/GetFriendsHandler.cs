using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Application.Features.Users.Models;
using Server.Common;
using Server.Data;
using Server.Data.Users;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.Me
{
    public class GetFriendsHandler : IRequestHandler<GetFriendsQuery, ListResult<UserListDto>>
    {
        private readonly DataContext _context;
        private readonly ISessionData _sessionData;
        private readonly IMapper _mapper;

        public GetFriendsHandler(
            DataContext context,
            ISessionData sessionData,
            IMapper mapper)
        {
            _context = context;
            _sessionData = sessionData;
            _mapper = mapper;
        }

        public async Task<ListResult<UserListDto>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Where(u => u.RecievedFriendRequests
                                .Any(f => f.SenderId == _sessionData.UserId &&
                                    f.RecieverId == u.Id && f.State == FriendshipState.Approved) ||
                            u.SendFriendRequests
                                .Any(f => f.SenderId == u.Id &&
                                    f.RecieverId == _sessionData.UserId && f.State == FriendshipState.Approved));

            var totalCount = await query.CountAsync(cancellationToken);
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

    public class GetFriendsQuery : ListingParams, IRequest<ListResult<UserListDto>>
    {
    }
}
