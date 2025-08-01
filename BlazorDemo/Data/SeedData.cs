﻿using BlazorDemo.Model;

namespace BlazorDemo.Data
{
    public class SeedData
    {
        public static void Initialize(PizzaStoreContext context)
        {
            if (!context.Specials.Any())
            {
                context.Specials.AddRange(new List<PizzaSpecial>
                {
                    new PizzaSpecial
                    {
                        Name = "The Baconatorizor",
                        BasePrice =  11.99M,
                        Description = "It has EVERY kind of bacon",
                        ImageUrl="img/pizzas/bacon.jpg"
                    },
                    new PizzaSpecial
                    {
                        Name = "Buffalo chicken",
                        BasePrice =  12.75M,
                        Description = "Spicy chicken, hot sauce, and blue cheese, guaranteed to warm you up",
                        ImageUrl="img/pizzas/meaty.jpg"
                    },
                    new PizzaSpecial
                    {
                        Name = "Veggie Delight",
                        BasePrice =  11.5M,
                        Description = "It's like salad, but on a pizza",
                        ImageUrl="img/pizzas/salad.jpg"
                    },
                    new PizzaSpecial
                    {
                        Name = "Margherita",
                        BasePrice =  9.99M,
                        Description = "Traditional Italian pizza with tomatoes and basil",
                        ImageUrl="img/pizzas/margherita.jpg"
                    },
                    new PizzaSpecial
                    {
                        Name = "Basic Cheese Pizza",
                        BasePrice =  11.99M,
                        Description = "It's cheesy and delicious. Why wouldn't you want one?",
                        ImageUrl="img/pizzas/cheese.jpg"
                    },
                    new PizzaSpecial
                    {
                        Name = "Classic pepperoni",
                        BasePrice =  10.5M,
                        Description = "It's the pizza you grew up with, but Blazing hot!",
                        ImageUrl="img/pizzas/pepperoni.jpg"
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
