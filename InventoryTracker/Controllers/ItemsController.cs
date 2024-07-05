using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ItemsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllItems()
        {
            var allItems = dbContext.Items.ToList();
            return Ok(allItems);
        }
        [HttpGet]
        [Route("{Id:guid}")]
        public IActionResult GetItemByID(Guid Id)
        {
            var item = dbContext.Items.Find(Id);
            if (item is null) 
            {
                return NotFound("Item not found");
            }
            return Ok(item);    
        }
        [HttpPost]
        public IActionResult AddItem(AddItemDto addItemDto)
        {
            var newItem = new Items()
            {
                Name = addItemDto.Name,
                Description = addItemDto.Description,
                Price = addItemDto.Price,
                Quantity = addItemDto.Quantity,
            };
            dbContext.Items.Add(newItem);
            dbContext.SaveChanges();
            return Ok(newItem);
        }
        [HttpPut]
        [Route("{Id:guid}")]
        public IActionResult EditItem(Guid Id, UpdateItemDto updateItemDto)
        {
            var item = dbContext.Items.Find(Id);
            if (item is null) 
            {
                return NotFound("Item to update not found");
            }
            item.Name = updateItemDto.Name;
            item.Description = updateItemDto.Description;
            item.Price = updateItemDto.Price;
            item.Quantity = updateItemDto.Quantity;
            dbContext.SaveChanges();
            return Ok(item );
        }
        [HttpDelete]
        [Route("{Id:guid}")]
        public IActionResult DeleteItem(Guid Id) 
        {
            var item = dbContext.Items.Find(Id);
            if (item is null ) 
            {
                return NotFound("Item to delete not found");
            }
            dbContext.Items.Remove(item);
            dbContext.SaveChanges();
            return Ok("Item deleted successfully");
        }
    }
}
