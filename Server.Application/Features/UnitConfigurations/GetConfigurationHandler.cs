using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Server.Common.Mappings;
using Server.Data;
using Server.Data.MapEntities;
using Server.Data.UnitConfigurations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Features.UnitConfigurations
{
    public class GetConfigurationHandler : IRequestHandler<UnitConfigurationQuery, Dictionary<CreatureType, UnitConfigurationDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetConfigurationHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Dictionary<CreatureType, UnitConfigurationDto>> Handle(UnitConfigurationQuery request, CancellationToken cancellationToken)
        {
            var dbConfigQuery = _context.UnitConfigurations.AsQueryable();

            if (request.CreatureType.HasValue)
            {
                dbConfigQuery = dbConfigQuery.Where(x => x.Type == request.CreatureType);
            }

            var configsToReturn = dbConfigQuery.ProjectTo<UnitConfigurationDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(ConvertToDictionary(configsToReturn));
        }

        private Dictionary<CreatureType, UnitConfigurationDto> ConvertToDictionary(IQueryable<UnitConfigurationDto> configsToReturn)
        {
            Dictionary<CreatureType, UnitConfigurationDto> configs = new Dictionary<CreatureType, UnitConfigurationDto>();

            foreach (var config in configsToReturn)
            {
                if (!configs.ContainsKey(config.Type))
                {
                    configs.Add(config.Type, config);
                }
            }

            return configs;
        }
    }

    public record UnitConfigurationQuery(CreatureType? CreatureType) : IRequest<Dictionary<CreatureType, UnitConfigurationDto>>;

    public class UnitConfigurationDto : IMapFrom<UnitConfiguration>
    {
        public int Id { get; set; }

        public CreatureType Type { get; set; }

        public Faction Faction { get; set; }

        public int MovementPoints { get; set; }

        public int ActionPoints { get; set; }

        public int MinDamage { get; set; }

        public int MaxDamage { get; set; }

        public int Hitpoints { get; set; }

        public int Mana { get; set; }

        public bool UsesMana { get; set; }

        public int Armor { get; set; }

        public AttackType AttackType { get; set; }

        public ArmorType ArmorType { get; set; }

        public int CreatureLevel { get; set; }

        public int BuildTime { get; set; }

        public int WoodCost { get; set; }

        public int OreCost { get; set; }

        public int GoldCost { get; set; }

        public int GemsCost { get; set; }

        public int FoodCost { get; set; }

        public int RetaliationPoints { get; set; }
    }
}
