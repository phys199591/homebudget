using EntityFrameworkCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirst.DAL
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseAlways<EFCFContext>
    {
        protected override void Seed(EFCFContext context)
        {
            var categories = new List<Category>()
            {
                new Category() { Name = "Żywność" },
                new Category() { Name = "Odzież" },
                new Category() { Name = "Samochód" }
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var expenses = new List<Expense>()
            {
                new Expense() { Name = "paliwo", Date=DateTime.Now, CategoryId = categories.FirstOrDefault(x => x.Name == "Samochód").Id },
                new Expense() { Name = "kurtka", Date=DateTime.Now, CategoryId = categories.FirstOrDefault(x => x.Name == "Odzież").Id },
                new Expense() { Name = "bułki", Date=DateTime.Now, CategoryId = categories.FirstOrDefault(x => x.Name == "Żywność").Id },
            };
            expenses.ForEach(e => context.Expenses.Add(e));
            context.SaveChanges();
        }
    }
}