using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirst.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Wartość")]
        public decimal Value { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ExpenseSubCategory> SubCategories { get; set; }
    }
}