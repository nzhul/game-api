namespace Server.Data.MapEntities
{
    public enum CreatureType
    {
        // Underworld
        Spider,
        Bat,
        QuillBeast,
        Stinger,
        Occultist,
        Worm,
        Demon,

        // Human
        Slave,
        Bowman,
        Balista,
        Priest,
        Engineer,
        Scholar,
        DrillEngine,

        // Sanctuary Heroes
        Zealot,
        Assassin,
        Architect,

        // Underworld Heroes
        EmberKing,
        Warlord,
        Witch
    }

    public static class CreatureTypeExtensions
    {
        public static bool IsHero(this CreatureType type)
        {
            return type == CreatureType.Zealot ||
                   type == CreatureType.Assassin ||
                   type == CreatureType.Architect ||
                   type == CreatureType.EmberKing ||
                   type == CreatureType.Warlord ||
                   type == CreatureType.Witch;
        }
    }
}
