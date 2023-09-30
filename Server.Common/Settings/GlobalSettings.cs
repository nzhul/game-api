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
