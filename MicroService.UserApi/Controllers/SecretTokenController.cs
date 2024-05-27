using MicroService.UserApi.Manager;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretTokenController : ControllerBase
    {
        private readonly TokenManager _tokenManager;
        private readonly ResponseManager _responseManager;

        public SecretTokenController(TokenManager tokenManager, ResponseManager responseManager)
        {
            _tokenManager = tokenManager;
            _responseManager = responseManager;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var token = _tokenManager.GetToken();
            return Ok(_responseManager.Create(token));
        }
    }
}
