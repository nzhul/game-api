namespace Server.Data.UnitConfigurations
{
    public enum ArmorType
    {
        /// <summary>
        /// Most spellcasters
        /// Unarmored takes extra damage from Piercing, and Blunt attacks.
        /// </summary>
        Unarmored,

        /// <summary>
        /// Most ranged attackers
        /// Light armor takes extra damage from Slashing attacks, reduces damage from Piercing, Arcane, and Blunt attacks.
        /// </summary>
        Light,

        /// <summary>
        /// Most melee units
        /// Heavy armor takes extra damage from Arcane attacks.
        /// </summary>
        Heavy,

        /// <summary>
        /// Special siege units or In-Battle buildings (Ex: Towers).
        /// Bastion armor greatly reduces Piercing, Arcane, and Slashing attacks, but takes extra damage from Blunt attacks.
        /// </summary>
        Bastion
    }
}