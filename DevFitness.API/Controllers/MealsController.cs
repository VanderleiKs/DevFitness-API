using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        public MealsController(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllMeals(int userId)
        {
            var allMeals = _dbContext.Meals.Where(m => m.UserId == userId && m.Active);

            return Ok(allMeals.Select(m => new MealWiewModel(m.Id, m.Description, m.Calories, m.Date)));
        }

        [HttpGet("{mealId}")]
        public IActionResult GetMeal(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if(meal == null)
            {
                return NotFound();
            }
            return Ok(new MealWiewModel(meal));
        }

        [HttpPost]
        public IActionResult PostMeal(int userId, [FromBody] CreateMealInputModel i)
        {
            var meal = new Meal(i.Description, i.Calories, i.Date, userId);
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetMeal), new {userId = userId, mealId = meal.Id}, i);
        }

        [HttpPut("{mealId}")]
        public IActionResult PutMeal(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            if (meal == null) 
                return NotFound();
            meal.Update(inputModel);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{mealId}")]
        public IActionResult DeleteMeal(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            if (meal == null)
                return NotFound();
            meal.Deactivate();
            return NoContent();
        }
    }
}
