using EntityFrameworkCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirst.DAL
{
    public class EFCFContext : DbContext
    {
        public EFCFContext() : base ("EFCFContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ExpenseSubCategory> ExpenseSubCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Expense>().Property(p => p.RowVersion).IsConcurrencyToken();
            //modelBuilder.Entity<Expense>().HasKey(t => t.Id);//fluent API - klucz główny
            //modelBuilder.Entity<Expense>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Expense>().Property(t => t.Name).HasMaxLength(50);
            //modelBuilder.Entity<Expense>().Property(t => t.Name).IsRequired();

            modelBuilder.Entity<Expense>().HasMany(e => e.SubCategories).WithRequired(s => s.Expense);
            modelBuilder.Entity<Category>().HasMany(c => c.SubCategories).WithRequired(s => s.Category);
        }
    }
}