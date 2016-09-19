using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _fortune_teller_mvc.Models;

namespace _fortune_teller_mvc.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.Color);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //YEARS TO RETIREMENT
            if (customer.Age % 2 == 0)
            //EVEN
            {
                ViewBag.NumberOfYears = 25;
            }
            //ODD
            else
            {
                ViewBag.NumberOfYears = 35;
            }

            //RESIDENCE
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.Location = "Location 0";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.Location = "Location 1";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.Location = "Location 2";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.Location = "Location 3";
            }
            else
            {
                ViewBag.Location = "Location 4";
            }

            //MODE OF TRANSPORTATION
            if (customer.ColorID == 1)
            {
                ViewBag.Transportation = "Trans 1";
            }
            else if (customer.ColorID == 2)
            {
                ViewBag.Transportation = "Trans 2";
            }
            else if (customer.ColorID == 3)
            {
                ViewBag.Transportation = "Trans 3";
            }
            else if (customer.ColorID == 4)
            {
                ViewBag.Transportation = "Trans 4";
            }
            else if (customer.ColorID == 5)
            {
                ViewBag.Transportation = "Trans 5";
            }
            else if (customer.ColorID == 6)
            {
                ViewBag.Transportation = "Trans 6";
            }
            else if (customer.ColorID == 7)
            {
                ViewBag.Transportation = "Trans 7";
            }

            //MONEY IN THE BANK
            var firstLetterOfMonth = customer.BirthMonth.BirthMonth1[0];
            var secondLetterOfMonth = customer.BirthMonth.BirthMonth1[1];
            var thirdLetterOfMonth = customer.BirthMonth.BirthMonth1[2];
            var wholeName = customer.FirstName + customer.LastName;

            if (wholeName.Contains(firstLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 500000;
            }
            else if (wholeName.Contains(secondLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 40000;
            }
            else if (wholeName.Contains(thirdLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 7000;
            }
            else
            {
                ViewBag.AmountOfMoney = 1000000;
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "FavoriteColor");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CusotmerID,FirstName,LastName,Age,NumberOfSiblings,ColorID,BirthMonthID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();



                //****Takes you to detail page after you create your fortune****
                return RedirectToAction("Details", new { id = customer.CusotmerID });

            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "FavoriteColor", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "FavoriteColor", customer.ColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CusotmerID,FirstName,LastName,Age,NumberOfSiblings,ColorID,BirthMonthID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "FavoriteColor", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
    }
}
