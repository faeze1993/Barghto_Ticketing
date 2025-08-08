
using Microsoft.AspNetCore.Mvc;
using Barghto_Ticketing.Interfaces.Auth;

namespace Barghto_Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Dtos.AuthenticateReqDto request)
        {
            return Ok(await _authService.Authenticate(request.Email, request.Password));
        }
    }
}
