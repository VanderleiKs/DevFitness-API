using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        public UsersController(DevFitnessDbContext dbContext    )
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(new UserWiewModel(user.Id, user.FullName, user.Height, user.Weight, user.BirthDate));
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] CreateUserInputModel i)
        {
            var user = new User(i.FullName, i.Height, i.Weight, i.BirthDate);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, i);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            user.Update(inputModel.Height, inputModel.Weight);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
