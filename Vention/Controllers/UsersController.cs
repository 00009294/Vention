using Microsoft.AspNetCore.Mvc;
using Vention.Models;
using Vention.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var user =  await this.userService.GetByIdAsync(id);
            if (ModelState.IsValid) return Ok(user);
            
            return NotFound();
            
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await this.userService.CreateAsync(user);
            return Ok("Added");
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            await this.userService.UpdateAsync(id, user);
            return Ok("Updated");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.userService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
