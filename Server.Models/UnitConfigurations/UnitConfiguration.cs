using Server.Models.MapEntities;
using System.Collections.Generic;

namespace Server.Models.UnitConfigurations
{
    /// <summary>
    /// Properties ending with ...Increment are used to define how much value the Hero unit will gain on each level.
    /// Example:
    ///     - BaseHP: 40
    ///     - Level: 5
    ///     - TotalHP: BaseHP + level*HPIncremenet = 40 + 5*10 = 40 + 50 = 90
    /// </summary>
    public class UnitConfiguration
    {
        public UnitConfiguration()
        {
            Abilities = new List<Ability>();
            Upgrades = new List<Upgrade>();
        }

        public int Id { get; set; }

        public CreatureType Type { get; set; }

        public Faction Faction { get; set; }

        public int MovementPoints { get; set; }

        public int ActionPoints { get; set; }

        public int MinDamage { get; set; }

        public int MinDamageIncrement { get; set; }

        public int MaxDamage { get; set; }

        public int MaxDamageIncrement { get; set; }

        public int Hitpoints { get; set; }

        public int HitpointsIncrement { get; set; }

        public int Mana { get; set; }

        public int ManaIncrement { get; set; }

        public int Armor { get; set; }

        public int ArmorIncrement { get; set; }

        public int Evasion { get; set; }

        public AttackType AttackType { get; set; }

        public ArmorType ArmorType { get; set; }

        public int BuildTime { get; set; }

        public int WoodCost { get; set; }

        public int OreCost { get; set; }

        public int GoldCost { get; set; }

        public int GemsCost { get; set; }

        public int FoodCost { get; set; }

        public virtual ICollection<Ability> Abilities { get; set; }

        public virtual ICollection<Upgrade> Upgrades { get; set; }
    }
}
