using cbtBackend.Dtos.RequestModels.UpdateRequstModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(UpdateAnswerRequestModel model)
        {
            var response = await _answerService.Update(model);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok();
        }

         [HttpDelete("{answerId}")]
        public async Task<ActionResult<bool>> Delete(string answerId)
        {
            var response = await _answerService.Delete(answerId);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok();
        }



    }
}