using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Models._career.Request
{
    public class UploadSubjectsRequest
    {
        [FromForm] public int CareerId { get; set; }
        [FromForm] public IFormFile File { get; set; } = null!;
    }
}
