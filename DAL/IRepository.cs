using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository
    {
        IEnumerable<Expense> GetExpenses();
        Expense GetExpense(int id);
        void CreateExpense(Expense e);
        void UpdateExpense(Expense e);
        void DeleteExpense(int id);

        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void CreateCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int id);
    }
}
