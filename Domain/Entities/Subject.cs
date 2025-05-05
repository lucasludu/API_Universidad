using Domain.Common;

namespace Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public int Year { get; set; }              // 🔹 Año (1, 2, etc.)
        public int Semester { get; set; }          // 🔹 Cuatrimestre (1 o 2)

        // Foreign key and relationship
        public int CareerId { get; set; }
        public Career Career { get; set; }
    }

}
