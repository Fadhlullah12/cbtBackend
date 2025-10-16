using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/students")]
    public class StudentController : ControllerBase
    {
        IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> RegisterStudent([FromForm] CreateStudentRequestModel model)
        {
            var response = await _studentService.RegisterStudent(model);
            if (response.Status == false)
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpPost("bulk")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> RegisterStudents(IEnumerable<CreateStudentRequestModel> models)
        {
            var response = await _studentService.RegisterStudents(models);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("assign/subjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> AssignSubjects(AssignSubjectsRequestModel model)
        {
            var response = await _studentService.AssignSubjects(model);
            if (response == false)
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetAll()
        {
            var response = await _studentService.GetAllStudentsAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("subjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetSudentSubject(string studentId)
        {
            var response = await _studentService.ViewAllStudentSubjectAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("exams")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetSudentExam(string studentId)
        {
            var response = await _studentService.ViewAllStudentExamsAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("results/{Id}")]
        public async Task<ActionResult<BaseResponse<ICollection<ResultsDto>>>> GetStudentResult(string Id)
        {
            var response = await _studentService.ViewStudentResultAsync(Id);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("results")]
        public async Task<ActionResult<BaseResponse<ICollection<AllStudentsResultsdto>>>> GetStudentsResult()
        {
            BaseResponse<ICollection<AllStudentsResultsdto>>? response = await _studentService.ViewStudentsResultAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<StudentDto>>> UpdateStudent(UpdateStudentRequestModel model)
        {
            var response = await _studentService.UpdateStudent(model);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<StudentDto>>> DeleteStudent(string id)
        {
            var response = await _studentService.Delete(id);
            if (response == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpDelete ("subject/{studentSubjectId}")]
        public async Task<ActionResult<BaseResponse<StudentDto>>> DeleteStudentSubject(string studentSubjectId)
        {
            var response = await _studentService.DeleteStudentSubject(studentSubjectId);
            if (response == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        
    }
}