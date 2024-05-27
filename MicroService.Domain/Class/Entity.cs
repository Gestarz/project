using System.ComponentModel.DataAnnotations;

namespace MicroService.Domain.Class
{
    public class Entity
    {
        [Key]
        public Guid Guid { get; set; }
    }
}
