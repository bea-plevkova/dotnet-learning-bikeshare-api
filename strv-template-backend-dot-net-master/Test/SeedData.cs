using System;
using Database;
using Database.Entities;

namespace Test
{
    public static class SeedData
    {
        public static void PopulateTestData(StrvTemplateDbContext dbContext)
        {
            dbContext.Products.Add(new Product() { Id = 1, Name = "Red Bag" });
            dbContext.Products.Add(new Product() { Id = 2, Name = "Green Bag" });
            dbContext.Products.Add(new Product() { Id = 3, Name = "Blue Bag" });
            dbContext.SaveChanges();
        }
    }
}