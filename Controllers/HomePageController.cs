using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/homepage")]
    public class HomePageController : ControllerBase
    {
        IHomePageService _homePageService;
        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [HttpGet("subAdmin")]
        public async Task<ActionResult<BaseResponse<HomePageDto>>> SubAdminDahsboardData()
        {
            var response = await _homePageService.SubAdminDahsboardData();
            if (response.Status == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}