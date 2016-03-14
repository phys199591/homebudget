using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;

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
            _service = null;
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
}
