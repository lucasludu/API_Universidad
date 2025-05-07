using Application.Feautures._career_subject.Commands.AddCareerSubjectCommands;
using Application.Feautures._career_subject.Commands.AddCareerSubjectsBatchCommands;
using Application.Parameters;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Models._career_subject.Request;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class CareerSubjectController : BaseApiController
    {
        [HttpPost("import-career-subjects")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportCareerSubjects([FromForm] SubjectImportRequest file)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = new MemoryStream();
            await file.File.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            var commands = new List<AddCareerSubjectCommand>();

            foreach (var row in worksheet.RowsUsed().Skip(1)) // saltar header
            {
                var subjectName = row.Cell(1).GetString().Trim();
                var subjectDescription = row.Cell(2).GetString().Trim();
                var careerName = row.Cell(3).GetString().Trim();
                var year = row.Cell(4).GetValue<int>();
                var semester = row.Cell(5).GetValue<int>();

                commands.Add(new AddCareerSubjectCommand (
                    new CareerSubjectRequest
                    {
                        SubjectName = subjectName,
                        SubjectDescription = subjectDescription,
                        CareerName = careerName,
                        Year = year,
                        Semester = semester
                    })
                );
            }

            var result = await Mediator.Send(new AddCareerSubjectsBatchCommand { SubjectsToAssign = commands });
            return Ok(result);
        }

    }
}
