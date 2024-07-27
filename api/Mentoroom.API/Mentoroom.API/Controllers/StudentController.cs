using MediatR;
using Mentoroom.APPLICATION.Managements.StudentManagment.GetAllStudents;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mentoroom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IStudentRepository studentRepository, IMediator mediator) : Controller
    {
        private readonly IStudentRepository studentRepository = studentRepository;
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddStudent(string userId, string indexNumber)
        {
            var result = await studentRepository.AddStudent(new Student() { UserId = userId, IndexNumber = indexNumber });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await mediator.Send(new GetStudent());
            return Ok(students);
        }
    }
}
