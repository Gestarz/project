using MicroService.Shared.Request.User;
using MicroService.UserApi.Manager;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserManager _userManager;
        private readonly TokenManager _tokenManager;
        private readonly ResponseManager _responseManager;

        public UserController(UserManager userManager, TokenManager tokenManager, ResponseManager responseManager)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] GetUserRequest request)
        {
            var user = _userManager.GetUser(request.Guid);
            return Ok(_responseManager.Create(user));
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] GetUserRequest request)
        {
            var user = _userManager.GetAll();
            return Ok(_responseManager.Create(user));
        }

        [HttpPost("Buy")]
        public IActionResult Buy(BuyRequest request)
        {
            var result = _userManager.Buy(request);
            return Ok(_responseManager.Create(result));
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateUserRequest request)
        {
            var user = _userManager.Add(request);
            return Ok(_responseManager.Create(user));
        }

        [HttpPost("AddMoney")]
        public IActionResult AddMoney(AddMoneyToUserRequest request)
        {
            if (!_tokenManager.CheckToken(request.SecretToken))
            {
                return StatusCode(403, "Invalid token");
            }
            var user = _userManager.AddMoney(request);
            return Ok(_responseManager.Create(user));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateUserRequest request)
        {
            var user = _userManager.Update(request);
            return Ok(_responseManager.Create(user));
        }


    }
}
