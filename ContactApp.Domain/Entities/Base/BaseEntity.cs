using System.ComponentModel.DataAnnotations;

namespace ContactApp.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdateBy { get; set; }
    }
}
