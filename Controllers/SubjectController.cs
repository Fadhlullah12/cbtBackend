using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/subjects")]
    public class SubjectController : ControllerBase
    {
        ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateSubjectRequestModel>>> Register([FromForm] CreateSubjectRequestModel model)
        {
            var response = await _subjectService.CreateSubjectAsync(model);
            if (response.Status == false)
            {
                return Conflict(response.Message);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<ICollection<SubjectDto>>>> GetSubjects()
        {
             var response = await _subjectService.ViewAllSubjectAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpGet("students")]
        public async Task<ActionResult<BaseResponse<ICollection<StudentDto>>>> GetSubjectStudents(string Id)
        {
            var response = await _subjectService.ViewAllSubjectStudentAsync(Id);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpPost("upload/questions")]
        public async Task<ActionResult<bool>> UploadQuestions(UploadQuestionRequestModel model)
        {
            var response = await _subjectService.UploadSubjectQuestionsAsync(model);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
