using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkCodeFirst.DAL;
using EntityFrameworkCodeFirst.Models;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace EntityFrameworkCodeFirst.Controllers
{
    public class ExpenseController : Controller
    {
        private EFCFContext db = new EFCFContext();

        // GET: Expenses
        public ActionResult Index()
        {
            return View(db.Expenses.Include(e => e.Category).ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Value,Date,CategoryId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                Expense expense = await db.Expenses.FindAsync(id);
                if (expense == null)
                {
                    return HttpNotFound();
                }
                PopulateCategoryDropDownList(expense.CategoryId);
                return View(expense);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id.HasValue)
            {
                string[] fields = new string[] { "Name", "Value", "Date", "CategoryId" };

                var expense = await db.Expenses.FindAsync(id);
                if (expense == null)
                {
                    Expense e = new Expense();
                    TryUpdateModel(e, fields);
                    ModelState.AddModelError(string.Empty, "Ta pozycja została usunięta");
                    PopulateCategoryDropDownList();
                    return View(e);
                }
                if (TryUpdateModel(expense, fields))
                {
                    try
                    {
                        db.Entry(expense).OriginalValues["RowVersion"] = rowVersion;
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        var entry = ex.Entries.Single();
                        var client = (Expense)entry.Entity;//wprowadzane
                        var dbEntry = entry.GetDatabaseValues();//w bazie
                        if (dbEntry == null)
                        {
                            ModelState.AddModelError(string.Empty, "Ta pozycja została usunięta");
                        }
                        else
                        {
                            var databaseValues = (Expense)dbEntry.ToObject();

                            if (databaseValues.Name != client.Name)
                                ModelState.AddModelError("Name", "Aktualna wartość: " + databaseValues.Name);
                            if (databaseValues.Budget != clientValues.Budget)
                                ModelState.AddModelError("Budget", "Current value: "
                                    + String.Format("{0:c}", databaseValues.Budget));
                            if (databaseValues.StartDate != clientValues.StartDate)
                                ModelState.AddModelError("StartDate", "Current value: "
                                    + String.Format("{0:d}", databaseValues.StartDate));
                            if (databaseValues.InstructorID != clientValues.InstructorID)
                                ModelState.AddModelError("InstructorID", "Current value: "
                                    + db.Instructors.Find(databaseValues.InstructorID).FullName);
                            ModelState.AddModelError(string.Empty, "Ta pozycja została zedytowana");
                            expense.RowVersion = databaseValues.RowVersion;
                        }
                    }
                }
                return View(expense);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categories = from c in db.Categories
                             orderby c.Name
                             select c;
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", selectedCategory);
        }
    }
}
