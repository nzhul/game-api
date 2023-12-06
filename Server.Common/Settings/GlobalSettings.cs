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

        public Attributes Attributes { get; set; }
    }

    public class Attributes
    {
        public int HPPerMight { get; set; } // 2

        public int ManaPerKnowledge { get; set; } // 12
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
