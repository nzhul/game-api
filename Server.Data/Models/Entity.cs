using System.ComponentModel.DataAnnotations;

namespace Server.Data
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}