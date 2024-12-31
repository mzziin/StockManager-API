using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.ApiModels;
using StockManager.BLL.Services.AuthServices;

namespace StockManager.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.LoginUser(loginModel);

            if (response.Status)
                return Ok(new { status = "success", user = response.Data });
            else
                return BadRequest(new { status = "fail", message = response.Message });
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.RegisterUserOrAdmin(registerModel, "warehouse manager");

            if (response.Status)
                return Ok(new { status = "success", message = response.Message });
            else
                return BadRequest(new { status = "fail", message = response.Message });
        }

        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.RegisterUserOrAdmin(registerModel, "admin");

            if (response.Status)
                return Ok(new { status = "success", message = response.Message });
            else
                return BadRequest(new { status = "fail", message = response.Message });
        }
    }
}