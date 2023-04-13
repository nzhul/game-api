using Server.Common.Mappings;
using System.Reflection;

namespace Server.Application.Mappings
{
    /// <summary>
    /// The mapping container class is used only to point to and contain the different mapping registrations.
    /// </summary>
    public class MappingContainer : MappingProfile
    {
        public MappingContainer()
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}
