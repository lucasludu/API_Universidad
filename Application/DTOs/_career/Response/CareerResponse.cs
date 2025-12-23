namespace Application.DTOs._career.Response
{
    public class CareerResponse
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int DurationInYears { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
