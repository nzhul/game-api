﻿using System.ComponentModel.DataAnnotations;
using Server.Models.Heroes;

namespace Server.Models.Realms.Input
{
    public class AvatarWithHeroCreationDto
    {
        [Required]
        public string HeroName { get; set; }

        public HeroFaction HeroFaction { get; set; }

        public HeroClass HeroClass { get; set; }
    }
}