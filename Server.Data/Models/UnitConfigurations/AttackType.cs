namespace Server.Data.UnitConfigurations
{
    public enum AttackType
    {
        /// <summary>
        /// Most melee units
        /// Slashing attacks do extra damage against Light armor, and reduced damage to Bastion armor.
        /// </summary>
        Slashing,

        /// <summary>
        /// Most ranged attackers
        /// Piercing attacks do extra damage to Unarmored units, and reduced damage to Heavy and Bastion armor.
        /// </summary>
        Piercing,

        /// <summary>
        /// Most siege units
        /// Blunt attacks do extra damage to Bastion armor and Unarmored units, and reduced damage to Light armor.
        /// </summary>
        Blunt,

        /// <summary>
        /// Most spellcasters
        /// Arcane attacks do extra damage against Heavy armor, and reduced damage to Light and Bastion armor.
        /// Arcane attacks do zero damage to magic-immune units.
        /// </summary>
        Arcane,
    }
}
