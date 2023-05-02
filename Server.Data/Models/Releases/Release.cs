using System;

namespace Server.Data.Models.Releases
{
    public class Release
    {
        public int Id { get; set; }

        /// <summary>
        /// Needs to be in the following format : Major.Minor.Build
        /// </summary>
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
