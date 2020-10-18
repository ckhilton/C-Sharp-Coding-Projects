using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
	public class InsureeController : Controller
	{
		private InsuranceEntities db = new InsuranceEntities();

		// GET: Insuree
		public ActionResult Index()
		{
			return View(db.Insurees.ToList());
		}

		// GET: Insuree/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Insuree insuree = db.Insurees.Find(id);
			if (insuree == null)
			{
				return HttpNotFound();
			}
			return View(insuree);
		}

		// GET: Insuree/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Insuree/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
		{
			if (ModelState.IsValid)
			{
				db.Insurees.Add(insuree);
				db.SaveChanges();
				Details(insuree.Id);
				return View("Quote");
			}

			return View(insuree);
		}

		////GET: Insuree/Quote/5
		//public ActionResult Quote(int? id)
		//{

		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Insuree insuree = db.Insurees.Find(id);
		//	if (insuree == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(insuree);
		//}

		// POST: Insuree/Quote/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Quote([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
		//{
		//	var insuree = db.Insurees.Find(id);
		//	decimal quote = insuree.Quote;

		//	// age logic
		//	quote = (DateTime.Now.Year - insuree.DateOfBirth.Year > 25) ? quote + 25.00M : quote;

		//	return View(insuree);


			//quote = (DateTime.Now.Year - data.dob.Value.Year < 18) ? quote + 25.00M : quote;
			//quote = (DateTime.Now.Year - data.dob.Value.Year > 100) ? quote + 25.00M : quote;

			//// automobile logic
			//quote = (data.car_year < 2000) ? quote + 25.00M : quote;
			//quote = (data.car_year > 2015) ? quote + 25.00M : quote;
			//quote = (data.car_make.ToLower() == "porsche") ? quote + 25.00M : quote;
			//quote = (data.car_make.ToLower() == "porsche" && data.car_model.Contains("carrera")) ? quote + 25.00M : quote;

			//// risk logic
			////TO DO quote = (data.tickets > 4) ? quote + (data.tickets * 10).Value : quote;
			////TO DO quote = (data.dui > 0) ? quote + (Decimal.Multiply(quote, .25M)): quote;
			//quote = (data.coverage_type.ToLower() == "full") ? quote + (Decimal.Multiply(quote, .25M)) : quote;
		//}

		// GET: Insuree/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Insuree insuree = db.Insurees.Find(id);
			if (insuree == null)
			{
				return HttpNotFound();
			}
			return View(insuree);
		}

		// POST: Insuree/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
		{
			if (ModelState.IsValid)
			{
				db.Entry(insuree).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(insuree);
		}

		// GET: Insuree/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Insuree insuree = db.Insurees.Find(id);
			if (insuree == null)
			{
				return HttpNotFound();
			}
			return View(insuree);
		}

		// POST: Insuree/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Insuree insuree = db.Insurees.Find(id);
			db.Insurees.Remove(insuree);
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
