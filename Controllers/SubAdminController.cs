using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using cbtBackend.Services.MailService;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/subadmins")]

    public class SubAdminController : ControllerBase
    {
        ISubAdminService _subAdminService;
        IMailMessageService _mailService;
        public SubAdminController(ISubAdminService subAdminService, IMailMessageService mailService)
        {
            _mailService = mailService;
            _subAdminService = subAdminService;
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveSubAdmin(string id)
        {
            bool response = await _subAdminService.ApproveSubAdminAsync(id);
            return Ok(response);
        }
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectSubAdmin(string id)
        {
            bool response = await _subAdminService.RejectSubAdminAsync(id);
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<CreateSubAdminRequestModel>>> Login([FromBody] CreateSubAdminRequestModel model)
        {
            var response = await _subAdminService.CreateSubAdminAsync(model);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<SubAdminDto>>> GetAllSubAdmins()
        {
            var response = await _subAdminService.GetAllSubAdminsAsync();
            return Ok(response);
        }
        [HttpGet("unapproved")]
        public async Task<ActionResult<BaseResponse<SubAdminDto>>> GetUnApprovedSubAdmins()
        {
            var response = await _subAdminService.GetUnApprovedSubAdminsAsync();
            return Ok(response);
        }
        [HttpPost("message")]
        public async Task<ActionResult<BaseResponse<SubAdminDto>>> SendMessage(MessageDto message)
        {
            bool response = await _mailService.SendPlainMessage(message);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok();
            
        }
    }
}