using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using cbtBackend.Services.MailService;
using MailKit;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/students")]
    public class StudentController : ControllerBase
    {
        IStudentService _studentService;
        IMailMessageService _mailMessageService;
        public StudentController(IStudentService studentService,IMailMessageService mailMessageService)
        {
            _mailMessageService = mailMessageService;
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
            return Ok(response);
        }
        [HttpPost("assign/subjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> AssignSubjects(AssignSubjectsRequestModel model)
        {
            var response = await _studentService.AssignSubjects(model);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetAll()
        {
            var response = await _studentService.GetAllStudentsAsync();
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("subjects")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> GetSudentSubject(string studentId)
        {
            var response = await _studentService.ViewAllStudentSubjectAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("subject/{subjectId}")]
        public async Task<ActionResult<BaseResponse<CreateStudentResponseModel>>> DeleteSudentSubject(string subjectId)
        {
            var response = await _studentService.DeleteStudentSubject(subjectId);
            if (response == false)
            {
                return BadRequest(response);
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
        public async Task<ActionResult<BaseResponse<ICollection<ResultDto>>>> GetStudentResult(string Id)
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
                return BadRequest(response);
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

        [HttpDelete("{Id}")]
        public async Task<ActionResult<BaseResponse<StudentDto>>> Delete(string Id)
        {
            var response = await _studentService.Delete(Id);
            if (response == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
         [HttpPost ("mail/result")]
        public async Task<ActionResult<BaseResponse<SendResultDto>>> SendMailResuts(SendResultDto model)
        {
            var response = await _mailMessageService.SendResult(model);
            if (response == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}