using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Data;
using test1.Models;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;
        public UserController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = context.Users.ToList();
            return Ok(users);
        }
        [HttpGet]
        [Route("{userID:int}")]
        public IActionResult GetUserByID(int userID)
        {
            var user = context.Users.Find(userID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
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
            return Ok(UserEntity);

        }
        [HttpPut]
        [Route("{userID:int}")]
        public IActionResult UpdateUser(int userID, UpdateUserDto updateUserDto)
        {
            var user = context.Users.Find(userID);
            user.name = updateUserDto.name;
            user.lastname = updateUserDto.lastname;
            user.email = updateUserDto.email;
            user.password = updateUserDto.password;
            context.SaveChanges();
            return Ok(user);
        }
        [HttpDelete]
        [Route("{userID:int}")]
        public IActionResult DeleteUser(int userID)
        {
            var user = context.Users.Find(userID);
            if (user == null)
            {
                return NotFound("User not found");
            }
            context.Users.Remove(user);
            context.SaveChanges();
            return Ok("User has been succesfully deleted.");
        }
    }
}
