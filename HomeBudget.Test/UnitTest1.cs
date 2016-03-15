using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;
using DAL.Models;
using System.Collections.Generic;

namespace HomeBudget.Test
{
    [TestClass]
    public class ExpenseControllerTest
    {
        DAL.IRepository _service;
        ExpenseController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _service = new MockRepository();
            _controller = new ExpenseController(_service);
        }

        [TestMethod]
        public void DetailsWithIdNull()
        {
            var task = _controller.Details(null);
            task.Wait();

            var result = task.Result;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(((HttpStatusCodeResult)result).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DetailsWithExistingId()
        {
            var task = _controller.Details(1);
            task.Wait();

            var result = (ViewResult)task.Result;
            //czy model.id == 1?
            Assert.AreEqual(null, result.Model);
        }

        [TestMethod]
        public void DetailsWithNotExistingId()
        {
            var task = _controller.Details(-1);
            task.Wait();

            var result = (ViewResult)task.Result;

            Assert.AreEqual(null, result.Model);
        }
    }

    public class MockRepository : DAL.IRepository
    {
        public void CreateCategory(Category c)
        {
            throw new NotImplementedException();
        }

        public void CreateExpense(Expense e)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteExpense(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Expense GetExpense(int id)
        {
            return null;
        }

        public IEnumerable<Expense> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category c)
        {
            throw new NotImplementedException();
        }

        public void UpdateExpense(Expense e)
        {
            throw new NotImplementedException();
        }
    }
}
