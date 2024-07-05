using BCrypt.Net;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public IActionResult GetUserById(Guid Id)
        {
            var user = dbContext.Users.Find(Id);
            if (user is null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            var newUser = new User()
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = hashedPassword,
                IsActive = createUserDto.IsActive,
            };
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            
            return Ok(newUser);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public IActionResult UpdateUser(Guid Id, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(Id);
            if (user is null)
            {
                return NotFound("User to update not found");
            }
            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
            user.IsActive = updateUserDto.IsActive;
            
            dbContext.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public IActionResult DeleteUser(Guid Id) 
        {
            var user = dbContext.Users.Find(Id);
            if (user is null) 
            {
                return NotFound("User to deactivate not found");
            }
            user.IsActive = false;
            dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
