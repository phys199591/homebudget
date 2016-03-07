﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirst.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}