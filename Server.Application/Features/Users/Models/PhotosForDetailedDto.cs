using Server.Common.Mappings;
using Server.Data.Users;
using System;

namespace Server.Application.Features.Users.Models
{
    public class PhotosForDetailedDto : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
    }
}
