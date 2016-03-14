using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Wartość")]
        public decimal Value { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //subkategorie
    }
}
