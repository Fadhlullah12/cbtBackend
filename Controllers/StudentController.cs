using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> Create([FromBody] CreateStudentRequestModel model)
        {
            var response = await _studentService.RegisterStudent(model);
            return Ok(response);
        }
        [HttpPost ("RegisterStudents")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models)
        {
            var response = await _studentService.RegisterStudents(models);
            return Ok(response);
        }
    }
}