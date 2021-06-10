using DevFitness.API.Core.Entities;
using System;

namespace DevFitness.API.Models.ViewModels
{
    public class MealWiewModel
    {
        public MealWiewModel(int id, string description, int calories, DateTime date)
        {
            Id = id;
            Description = description;
            Calories = calories;
            Date = date;
        }

        public MealWiewModel(Meal meal)
        {
            Id = meal.Id;
            Description = meal.Description;
            Calories = meal.Calories;
            Date = meal.Date;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public int Calories { get; private set; }
        public DateTime Date { get; private set; }
    }
}
