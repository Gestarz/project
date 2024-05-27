using MicroService.ItemApi.Manager;
using MicroService.Shared.Request.Item;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.ItemApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemManager _itemManager;
        public ItemController(ItemManager itemManager)
        {
            _itemManager = itemManager;
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] GetItemRequest request)
        {
            var item = _itemManager.GetItem(request);
            return Ok(item);
        }

        // get all
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var items = _itemManager.GetAll();
            return Ok(items);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateItemRequest request)
        {
            var item = _itemManager.CreateItem(request);
            return Ok(item);
        }

        [HttpPost("Buy")]
        public IActionResult Buy(BuyItemRequest request)
        {
            var result = _itemManager.BuyItem(request);
            return Ok(result);
        }

        [HttpPost("UpdateAmount")]
        public IActionResult UpdateAmount(UpdateAmountRequest request)
        {
            var itemInfo = _itemManager.UpdateAmount(request);
            return Ok(itemInfo);
        }
    }
}
