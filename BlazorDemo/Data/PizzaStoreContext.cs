﻿using BlazorDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorDemo.Data
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PizzaSpecial>  Specials { get; set; }
    }
}
