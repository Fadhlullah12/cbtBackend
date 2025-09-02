using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/exams")]
    public class ExamController : ControllerBase
    {
        IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateExamResponseModel>>> CreateExam(CreateExamRequestModel model)
        {
            var response = await _examService.StartExamAsync(model);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }
        [HttpPost("end")]
        public async Task<ActionResult<BaseResponse<EndExamResponseModel>>> EndExam(string Id)
        {
            var response = await _examService.EndExamAsync(Id);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message );
        }

        [HttpGet("load")]
        public async Task<ActionResult<BaseResponse<ICollection<LoadExamsDto>>>> LoadAvailableExamAsync()
        {
            var response = await _examService.LoadAvailableExamAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        
         [HttpPost("submit")]
        public async Task<ActionResult<BaseResponse<SubmitExamDto>>> ExamResult(ExamSubmissionRequestModel model)
        {
            var response = await _examService.SubmitExam(model);
            return Ok(response);
        }
           [HttpGet]
        public async Task<ActionResult<BaseResponse<ICollection<ExamDto>>>> GetExam()
        {
            var response = await _examService.GetAllExamsAsync();
            if (response.Status == false)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}