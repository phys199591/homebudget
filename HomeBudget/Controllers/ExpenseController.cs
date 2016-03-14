using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.Controllers
{
    public class ExpenseController : Controller
    {
        DAL.IRepository service;

        public ExpenseController(DAL.IRepository _service)
        {
            service = _service;
        }

        public async Task<ActionResult> Index()
        {
            return View(service.GetExpenses());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                return View(service.GetExpense(id.Value));
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }
}