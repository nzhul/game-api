using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.UnitConfigurations
{
    public class Upgrade
    {
        public Upgrade()
        {
            UnitConfigurations = new List<UnitConfiguration>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int WoodCost { get; set; }

        public int GoldCost { get; set; }

        public int TimeCost { get; set; }

        public virtual ICollection<UnitConfiguration> UnitConfigurations { get; set; }

        //public int HitpointBonus { get; set; }

        //public int AttackDamageBonus { get; set; }

        //public bool UnlockAbility { get; set; } // i might not need this

        //public int AbilityLevelBonus { get; set; } // Ex: Upgrade "Poison Spears" ability with one level.

        //public string AbilityAffected { get; set; } // Ex: "Poison Spears"
    }
}