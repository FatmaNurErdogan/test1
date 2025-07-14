using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Data;
using test1.Models;

namespace test1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<UserController> _logger;
       
        public UserController(AppDbContext context, ILogger<UserController> logger)
        {
            this.context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation("Fetching all users from the database");
            var users = context.Users.ToList();
            return Ok(users);
        }
        [HttpGet]
        [Route("{userID:int}")]
        public IActionResult GetUserByID(int userID)
        {

            if (userID <= 0)
            {
                return BadRequest("Invalid user ID");
            }
            _logger.LogInformation("Fetching the user with ID {userID} ", userID);
            var user = context.Users.Find(userID);

            if (user == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Fetching user with ID {UserID}", userID);   
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            _logger.LogInformation("Trying to add a new user with ID {userID} " , addUserDto);
            var UserEntity = new User()
            {
                userID = addUserDto.userID,
                name = addUserDto.name,
                lastname = addUserDto.lastname,
                email = addUserDto.email,
                password = addUserDto.password
            };
            context.Users.Add(UserEntity);
            context.SaveChanges();
            _logger.LogInformation("User with ID {userID} has been successfully added.", UserEntity.userID);
            return Ok(UserEntity);

        }
        [HttpPut]
        [Route("{userID:int}")]
        public IActionResult UpdateUser(int userID, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Updating user with ID {userID}", userID);
            var user = context.Users.Find(userID);
            user.name = updateUserDto.name;
            user.lastname = updateUserDto.lastname;
            user.email = updateUserDto.email;
            user.password = updateUserDto.password;
            context.SaveChanges();
            _logger.LogInformation("User with ID {userID} has been successfully updated.", userID);
            return Ok(user);
        }
        [HttpDelete]
        [Route("{userID:int}")]
        public IActionResult DeleteUser(int userID)
        {
            _logger.LogInformation("Deleting user with ID {userID}", userID);
            var user = context.Users.Find(userID);
            if (user == null)
            {
                return NotFound("User not found");
            }
            context.Users.Remove(user);
            context.SaveChanges();
            _logger.LogInformation("User with ID {userID} has been successfully deleted.", userID);
            return Ok("User has been succesfully deleted.");
        }
    }
}
