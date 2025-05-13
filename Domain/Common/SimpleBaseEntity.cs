using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class SimpleBaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string? CreatedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
