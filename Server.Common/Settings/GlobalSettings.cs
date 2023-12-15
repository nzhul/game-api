namespace Server.Common.Settings
{
    public class GlobalSettings
    {
        public int ArmyMovementPoints { get; set; }

        /// <summary>
        /// In seconds.
        /// </summary>
        public int DayDuration { get; set; }

        public BattleSettings Battle { get; set; }

        public StatConstants StatConstants { get; set; }
    }

    public class StatConstants
    {
        public int HPPerMight { get; set; } // 22

        public int ManaPerKnowledge { get; set; } // 12

        public float ArmorPerDex { get; set; } // 0.16666f

        public float MagicResPerKnowledge { get; set; } // 0.1f

        public float ArmorFactor1 { get; set; } // 0.052f

        public float ArmorFactor2 { get; set; } // 0.048f

        public float MagicResistFactor1 { get; set; } // 0.006f > Controls the starting point of the MagicRes DR

        public float MagicResistFactor2 { get; set; } // 500f > Increasing this value decreses the increment for MagicRes DR between levels. Vise-versa - Decreasing the value - increases the increment

        public float DamagePerPrimaryAttribute { get; set; } // 1f
    }

    public class BattleSettings
    {
        /// <summary>
        /// In seconds.
        /// </summary>
        public int TurnDuration { get; set; }

        /// <summary>
        /// In seconds.
        /// </summary>
        public int IdleTimeout { get; set; }
    }
}
