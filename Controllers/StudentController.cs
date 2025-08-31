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
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> Create([FromForm] CreateStudentRequestModel model)
        {
            var response = await _studentService.RegisterStudent(model);
            if (response.Status == false)
            {
                return Conflict(response.Message);
            }
            return Ok(response);
        }
        [HttpPost("RegisterStudents")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models)
        {
            var response = await _studentService.RegisterStudents(models);
            return Ok(response);
        }
        [HttpPost("AssignSubjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> AssignSubjects(AssignSubjectsRequestModel model)
        {
            var response = await _studentService.AssignSubjects(model);
            return Ok(response);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetAll()
        {
            var response = await _studentService.GetAllStudentsAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("GetSudentSubjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetSudentSubject(string studentId)
        {
            var response = await _studentService.ViewAllStudentSubjectAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("GetSudentExams")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetSudentExam(string studentId)
        {
            var response = await _studentService.ViewAllStudentExamsAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}