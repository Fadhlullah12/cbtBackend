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

    public class SubAdminController : ControllerBase
    {
        ISubAdminService _subAdminService;
        public SubAdminController(ISubAdminService subAdminService)
        {
            _subAdminService = subAdminService;
        }

        [HttpPost("Approve")]
        public async Task<IActionResult> ApproveSubAdmin(string id)
        {
            bool response = await _subAdminService.ApproveSubAdminAsync(id);
            return Ok(response);
        }
        [HttpPost("Reject")]
        public async Task<IActionResult> RejectSubAdmin(string id)
        {
            bool response = await _subAdminService.RejectSubAdminAsync(id);
            return Ok(response);
        }
         [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse<CreateSubAdminRequestModel>>> Login([FromBody] CreateSubAdminRequestModel model)
        {
            var response = await _subAdminService.CreateSubAdminAsync(model);
            return Ok(response);
        }
    }
}