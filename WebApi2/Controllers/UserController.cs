using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2.Data;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DbFirstDbContext _dbContext;
        public UserController(DbFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                if(users.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch(Exception)
            {
                return StatusCode(500, "An error occured 123");
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] Models.UserRequest request)
        {
            Users user = new Users();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.City = request.City;
            user.State = request.State;
            user.Country = request.Country;

            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            } 
            catch (Exception error)
            {
                return StatusCode(500, "An error occures");
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] Models.UserRequest request)
        {
            try
            {
               var user = _dbContext.Users.FirstOrDefault(row => row.Id == request.Id);
                if(user == null)
                {
                    return StatusCode(400, "User not found");
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.State = request.State;
                user.City = request.City;
                user.Country = request.Country;
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();

            } catch (Exception error){
                return StatusCode(500, "Error occured");
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(row => row.Id == Id);
                if (user == null)
                {
                    return StatusCode(400, "User not found");
                }
                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            } catch (Exception error)
            {
                return StatusCode(500, "An error occured");
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
        //private List<Models.UserRequest> GetUsers()
        //{
          //  return new List<Models.UserRequest>
           // {
            //    new Models.UserRequest { FirstName = "abc" }
            //};
        //}
    }
}