using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/results")]
    public class ResultController : ControllerBase
    {
        IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<BaseResponse<ICollection<StudentResultsDto>>>> GetStudentResults(string studentId)
        {
            var response = await _resultService.GetStudentResultAsync(studentId);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult<BaseResponse<ICollection<StudentResultsDto>>>> GetSubjectResult(string subjectId)
        {
            var response = await _resultService.GetSubjectResultAsync(subjectId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
         [HttpGet("exam/{examId}")]
        public async Task<ActionResult< BaseResponse<ICollection<ExamResultsDto>>>> GetExamResults(string examId)
        {
            var response = await _resultService.GetExamResultsAsync(examId);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}