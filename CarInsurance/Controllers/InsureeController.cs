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
				//SET 'decimal quote' TO BASE OF $50/MONTH
				decimal quote = 50.00M;

				// AGE LOGIC
				quote = (DateTime.Now.Year - insuree.DateOfBirth.Year < 19) ? quote + 100.00M : quote;
				quote = (DateTime.Now.Year - insuree.DateOfBirth.Year >= 19) ? quote + 100.00M : quote;
				quote = (DateTime.Now.Year - insuree.DateOfBirth.Year <= 25) ? quote + 100.00M : quote;
				quote = (DateTime.Now.Year - insuree.DateOfBirth.Year > 25) ? quote + 25.00M : quote;

				// AUTO LOGIC
				quote = (insuree.CarYear < 2000) ? quote + 25.00M : quote;
				quote = (insuree.CarYear > 2015) ? quote + 25.00M : quote;
				quote = (insuree.CarMake.ToUpper() == "PORSCHE") ? quote + 25.00M : quote;
				quote = (insuree.CarMake.ToUpper() == "PORSCHE" && insuree.CarModel.ToUpper().Contains("CARRERA")) ? quote + 50.00M : quote;

				// RISK LOGIC
				quote = (insuree.SpeedingTickets > 0) ? quote + (insuree.SpeedingTickets * 10) : quote;
				quote = (insuree.DUI == true) ? quote + (decimal.Multiply(quote, .25M)): quote;
				quote = (insuree.CoverageType == true) ? quote + (decimal.Multiply(quote, .50M)) : quote;

				insuree.Quote = quote;
				db.Insurees.Add(insuree);
				db.SaveChanges();
				Details(insuree.Id);
				return View("Quote");
			}

			return View(insuree);
		}
	}
}
