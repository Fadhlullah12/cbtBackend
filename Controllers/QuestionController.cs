using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/questions")]
    public class QuestionController : ControllerBase
    {
        IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("exam{examId}")]
        public async Task<ActionResult<BaseResponse<ExamResponse>>> LoadExamQuestions(string examId)
        {
            var response = await _questionService.LoadExamQuestions(examId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("subject{subjectId}")]
        public async Task<ActionResult<BaseResponse<BaseResponse<ICollection<QuestionDto>>>>> GetSubjectQuestions(string subjectId)
        {
            var response = await _questionService.GetSubjectQuestions(subjectId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<BaseResponse<ICollection<QuestionDto>>>>> Update(UpdateQuestionsRequestModel model)
        {
            var response = await _questionService.UpdateQuestion(model);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok();
        }
          [HttpDelete]
        public async Task<ActionResult<bool>> Delete(string questionId)
        {
            var response = await _questionService.Delete(questionId);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}