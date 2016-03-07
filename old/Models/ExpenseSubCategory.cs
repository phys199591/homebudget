using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirst.Models
{
    public class ExpenseSubCategory
    {
        public int ExpenseSubCategoryId { get; set; }
        public int ExpenseId { get; set; }
        public int CategoryId { get; set; }
        public decimal Value { get; set; }

        public virtual Expense Expense { get; set; }
        public virtual Category Category { get; set; }
    }
}