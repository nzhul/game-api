using Server.Data.MapEntities;
using Server.Data.UnitConfigurations;
using System.Linq;

namespace Server.Data.Services.Abstraction
{
    public interface IUnitConfigurationsService
    {
        IQueryable<UnitConfiguration> GetConfigurations(CreatureType? creatureType);
    }
}
