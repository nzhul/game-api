using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Data.Models.Releases
{
    public class Release
    {
        [Key]
        public string Version { get; set; }

        public ReleaseType Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseNotes { get; set; }

        public bool IsPublic { get; set; }

        public string DownloadUrl { get; set; }
    }

    public enum ReleaseType
    {
        GameClient,
        Launcher
    }
}
