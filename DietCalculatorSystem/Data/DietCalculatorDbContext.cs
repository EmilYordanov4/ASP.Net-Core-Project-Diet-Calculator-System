using DietCalculatorSystem.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietCalculatorSystem.Data
{
    public class DietCalculatorDbContext : IdentityDbContext<User>
    {
        public DietCalculatorDbContext(DbContextOptions<DietCalculatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; init; }

        public DbSet<DeficitDiet> DeficitDiets { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
