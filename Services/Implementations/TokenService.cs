using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cbtBackend.Services.Implementations
{
    public class TokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        var token = GetTokenFromHeader();
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        return new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
    }

    private string GetTokenFromHeader()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }
        return null;
    }
}
}