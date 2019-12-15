using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    public class VragenlijstController : Controller
    {
        private FestiSpecEntities db = new FestiSpecEntities();

        // GET: Vragenlijst
        public ActionResult Index()
        {
            var vragenlijst = db.Vragenlijst.Include(v => v.Template1);
            return View(vragenlijst.ToList());
        }

        // GET: Vragenlijst/Details/5
        public ActionResult Details(int? id)
        {
            var userId = (int)(HttpContext.Session["userId"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);
            if (vragenlijst == null)
            {
                return HttpNotFound();
            }
            return View(vragenlijst);
        }

        // GET: Vragenlijst/Create
        public ActionResult Create()
        {
            ViewBag.Template_ID = new SelectList(db.Template, "ID", "Type");
            return View();
        }

        // POST: Vragenlijst/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titel,Versie,Template_ID,Opmerking,Is_Ingevuld")] Vragenlijst vragenlijst)
        {
            if (ModelState.IsValid)
            {
                db.Vragenlijst.Add(vragenlijst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Template_ID = new SelectList(db.Template, "ID", "Type", vragenlijst.Template_ID);
            return View(vragenlijst);
        }

        // GET: Vragenlijst/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);
            if (vragenlijst == null)
            {
                return HttpNotFound();
            }
            ViewBag.Template_ID = new SelectList(db.Template, "ID", "Type", vragenlijst.Template_ID);
            return View(vragenlijst);
        }

        // POST: Vragenlijst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titel,Versie,Template_ID,Opmerking,Is_Ingevuld")] Vragenlijst vragenlijst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vragenlijst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Template_ID = new SelectList(db.Template, "ID", "Type", vragenlijst.Template_ID);
            return View(vragenlijst);
        }

        // GET: Vragenlijst/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);
            if (vragenlijst == null)
            {
                return HttpNotFound();
            }
            return View(vragenlijst);
        }

        // POST: Vragenlijst/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);
            db.Vragenlijst.Remove(vragenlijst);
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
