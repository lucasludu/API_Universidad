using Application.Parameters;

namespace Application.Feautures._career.Queries.GetAllCareerQueries
{
    public class GetAllCareerParameters : RequestParameters
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; } // 🔸 nuevo: null = todas, true = activas, false = inactivas
    }
}
