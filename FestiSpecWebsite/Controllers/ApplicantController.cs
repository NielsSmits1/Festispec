using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    public class ApplicantController : Controller
    {
        private FestiSpecEntities1 db = new FestiSpecEntities1();

        // GET: Applicant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applicant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Voornaam,Tussenvoegsel,Achternaam,Postcode,Huisnummer,Straatnaam,Geboortedatum,IBAN,Email,Telefoonnummer")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Applicant.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(applicant);
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
