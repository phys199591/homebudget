namespace EntityFrameworkCodeFirst.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkCodeFirst.DAL.EFCFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EntityFrameworkCodeFirst.DAL.EFCFContext";
        }

        protected override void Seed(EntityFrameworkCodeFirst.DAL.EFCFContext context)
        {
            var categories = new List<Category>()
            {
                new Category() { Name = "�ywno��" },
                new Category() { Name = "Odzie�" },
                new Category() { Name = "Samoch�d" }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(e => e.Name, c));
            context.SaveChanges();

            var expenses = new List<Expense>()
            {
                new Expense()
                {
                    Name = "kurtka",
                    Value = 279,
                    Date = DateTime.Today,
                    CategoryId = categories.Single(c => c.Name == "Odzie�").Id
                },
                new Expense()
                {
                    Name = "bu�ki",
                    Value = 1.2m,
                    Date = DateTime.Today,
                    CategoryId = categories.Single(c => c.Name == "�ywno��").Id
                },
                new Expense()
                {
                    Name = "paliwo",
                    Value = 150.35m,
                    Date = DateTime.Today,
                    CategoryId = categories.Single(c => c.Name == "Samoch�d").Id
                }
            };
            expenses.ForEach(e => context.Expenses.Add(e));
            context.SaveChanges();
        }
    }
}
