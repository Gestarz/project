using MicroService.Gateway.Manager;
using MicroService.Shared.Request.Item;
using MicroService.Shared.Request.Notify;
using MicroService.Shared.Request.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroService.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {

        private readonly ILogger<GatewayController> _logger;
        private readonly ProxyCallManager _proxyCallManager;

        public GatewayController(ILogger<GatewayController> logger, ProxyCallManager proxyCallManager)
        {
            _logger = logger;
            _proxyCallManager = proxyCallManager;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser([FromQuery] GetUserRequest request)
        {
            _logger.LogInformation("TestController.Get() called");
            var result = _proxyCallManager.GetUser(request);
            _logger.LogInformation("TestController.Get() result: " + JsonConvert.SerializeObject(result));
            return Ok(result.Value);
        }

        // get item
        [HttpGet("GetItem")]
        public IActionResult GetItem([FromQuery] GetItemRequest request)
        {
            _logger.LogInformation("TestController.GetItem() called");
            return Ok(_proxyCallManager.GetItem(request));
        }

        //buy item
        [HttpPost("BuyItem")]
        public IActionResult BuyItem([FromBody] BuyRequest request)
        {
            _logger.LogInformation("TestController.BuyItem() called");
            var result = _proxyCallManager.BuyItem(request);
            _logger.LogInformation("TestController.BuyItem() result: " + JsonConvert.SerializeObject(result));
            return Ok(result.Value);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("TriggerBuyItem")]
        public IActionResult TiggerBuyItem([FromBody] BuyRequest request)
        {
            _logger.LogInformation("TestController.BuyItem() called");
            var result = _proxyCallManager.TriggerBuyItem(request);
            _logger.LogInformation("TestController.BuyItem() result: " + JsonConvert.SerializeObject(result));
            return Ok(result);
        }

        // notify 
        [HttpPost("Notify")]
        public IActionResult Notify([FromBody] NotifyUserRequest request)
        {
            _logger.LogInformation("TestController.Notify() called");
            _proxyCallManager.Notify(request);
            return Ok("Notify queued!");
        }


    }
}
