using Microsoft.AspNetCore.Mvc;
using Vention.Models;
using Vention.Services;


namespace Vention.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = this.userService.GellAll();
            if(ModelState.IsValid) return Ok(users);
           
            return BadRequest();

            
        }

        [HttpGet("name")]
        public async Task<ActionResult<IEnumerable<User>>> GetByName(bool ascending = true,int? maxUser = null)
        {
            var sortedUsers = await this.userService.GetAllByUserNameAsync(ascending);
            if(maxUser.HasValue && maxUser.Value > 0)
            {
                sortedUsers = sortedUsers.Take(maxUser.Value);
            }
            return Ok(sortedUsers);
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user =  await this.userService.GetByIdAsync(id);
            if (ModelState.IsValid) return Ok(user);
            
            return NotFound();
            
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await this.userService.CreateAsync(user);
            return Ok("Added");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            await this.userService.UpdateAsync(id, user);
            
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.userService.DeleteAsync(id);
            
            return Ok("Deleted");
        }
    }
}
