using Server.Data.MapEntities;
using Server.Data.Models.MapEntities;
using System.Collections.Generic;

namespace Server.Data.UnitConfigurations
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

        public Attribute PrimaryAttribute { get; set; }

        public Faction Faction { get; set; }

        public int MovementPoints { get; set; }

        public int ActionPoints { get; set; }

        public int MinDamage { get; set; }

        public int MaxDamage { get; set; }

        public int Hitpoints { get; set; }

        public int Mana { get; set; }

        public bool UsesMana { get; set; }

        public int Armor { get; set; }

        /// <summary>
        /// Represents the Raw value. It is later on used to calculate percentage.
        /// </summary>
        public int MagicResist { get; set; }

        public int Might { get; set; }

        public float MightGain { get; set; }

        public int Dexterity { get; set; }

        public float DexterityGain { get; set; }

        public int Knowledge { get; set; }

        public float KnowledgeGain { get; set; }

        // Thing about if I will use this or I will use the Physical/Magic/Pure... damage types.
        // This allows for simplified game mechanic > Rock-Paper-Scissors.
        // Without this - all units will feel the same.
        public AttackType AttackType { get; set; }

        public ArmorType ArmorType { get; set; }

        // ---------- COSTS ----------

        public int BuildTime { get; set; }

        public int WoodCost { get; set; }

        public int OreCost { get; set; }

        public int GoldCost { get; set; }

        public int GemsCost { get; set; }

        public int FoodCost { get; set; }

        public int RetaliationPoints { get; set; }

        public virtual ICollection<Ability> Abilities { get; set; }

        public virtual ICollection<Upgrade> Upgrades { get; set; }
    }
}
