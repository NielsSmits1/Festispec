using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    [Authorize]
    public class AvailabilityController : Controller
    {
        private FestiSpecEntities db = new FestiSpecEntities();

        // GET: Availability
        public ActionResult Index()
        {
            var id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            var beschikbaarheid = db.Beschikbaarheid.Where(b => b.Inspecteur_ID == id).OrderByDescending(b => b.Datum).ToList();
            return View(beschikbaarheid);
        }

        // GET: Availability/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beschikbaarheid beschikbaarheid = db.Beschikbaarheid.Find(id);
            if (beschikbaarheid == null)
            {
                return HttpNotFound();
            }
            return View(beschikbaarheid);
        }

        // GET: Availability/Create
        public ActionResult Create()
        {
            var id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            ViewBag.Inspecteur_ID = new SelectList(db.Inspecteur.Where(b => b.ID == id).ToList(), "ID", "Username");
            return View();
        }

        // POST: Availability/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Inspecteur_ID,Datum")] Beschikbaarheid beschikbaarheid)
        {
            if (ModelState.IsValid)
            {
                db.Beschikbaarheid.Add(beschikbaarheid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Inspecteur_ID = new SelectList(db.Inspecteur, "ID", "Username", beschikbaarheid.Inspecteur_ID);
            return View(beschikbaarheid);
        }

        // GET: Availability/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beschikbaarheid beschikbaarheid = db.Beschikbaarheid.Find(id);
            if (beschikbaarheid == null)
            {
                return HttpNotFound();
            }
            ViewBag.Inspecteur_ID = new SelectList(db.Inspecteur, "ID", "Username", beschikbaarheid.Inspecteur_ID);
            return View(beschikbaarheid);
        }

        // POST: Availability/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Inspecteur_ID,Datum")] Beschikbaarheid beschikbaarheid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beschikbaarheid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inspecteur_ID = new SelectList(db.Inspecteur, "ID", "Username", beschikbaarheid.Inspecteur_ID);
            return View(beschikbaarheid);
        }

        // GET: Availability/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beschikbaarheid beschikbaarheid = db.Beschikbaarheid.Find(id);
            if (beschikbaarheid == null)
            {
                return HttpNotFound();
            }
            return View(beschikbaarheid);
        }

        // POST: Availability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beschikbaarheid beschikbaarheid = db.Beschikbaarheid.Find(id);
            db.Beschikbaarheid.Remove(beschikbaarheid);
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
