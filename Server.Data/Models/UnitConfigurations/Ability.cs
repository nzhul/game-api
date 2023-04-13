using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.UnitConfigurations
{
    public class Ability
    {
        public Ability()
        {
            UnitConfigurations = new List<UnitConfiguration>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int ResourceCost { get; set; }

        public int ActionPointsCost { get; set; }

        public int MovementPointsCost { get; set; }

        /// <summary>
        /// How many times this ability can be upgraded. Ex: in Warcraft3 this is 3 for the heroes.
        /// </summary>
        public int Levels { get; set; }

        public virtual ICollection<UnitConfiguration> UnitConfigurations { get; set; }
    }
}
