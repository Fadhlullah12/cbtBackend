using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/subAdmins")]

    public class SubAdminController : ControllerBase
    {
        ISubAdminService _subAdminService;
        public SubAdminController(ISubAdminService subAdminService)
        {
            _subAdminService = subAdminService;
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveSubAdmin(string id)
        {
            bool response = await _subAdminService.ApproveSubAdminAsync(id);
            if (response == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectSubAdmin(string id)
        {
            bool response = await _subAdminService.RejectSubAdminAsync(id);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<CreateSubAdminRequestModel>>> Register([FromForm] CreateSubAdminRequestModel model)
        {
            var response = await _subAdminService.CreateSubAdminAsync(model);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<ICollection<SubAdminDto>>>> GetAll()
        {
            var response = await _subAdminService.GetAllSubAdminAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
         [HttpGet("unapproved")]
        public async Task<ActionResult<BaseResponse<ICollection<SubAdminDto>>>> GetUnApproved()
        {
            var response = await _subAdminService.GetAllUnApprovedAsync();
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
         [HttpPut]
        public async Task<ActionResult<BaseResponse<ICollection<SubAdminDto>>>> Update([FromForm]CreateSubAdminRequestModel model)
        {
            var response = await _subAdminService.UpdateSubAdminAsync(model);
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}