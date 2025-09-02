using System.Security.Claims;
using cbtBackend.Services.Interfaces;

namespace cbtBackend.Services.Implementations
{
    public class GetCurrentUser : IGetCurrentUser
    {
        IHttpContextAccessor _httpContextAccessor;
        TokenService _tokenService;
        public GetCurrentUser(IHttpContextAccessor httpContextAccessor, TokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public string GetCurrentUserId()
    {
       var user = _tokenService.GetClaimsPrincipal();
       var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
       return userId!;
    }
    }
}