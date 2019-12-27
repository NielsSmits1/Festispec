using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    [Authorize]
    public class QuestionnaireController : Controller
    {
        private FestiSpecEntities db = new FestiSpecEntities();

        // GET: Vragenlijst
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var Userid = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            if (!db.Inspectie.Include("Inspecteur").Any( i => i.Inspecteur.Any(k => k.ID == Userid)&& i.Inspectienummer == id ))
            {
                return View("NotPlannedInInspection");
            }
            var AnsweredQuestionnairse = db.Inspectie_Wel_Ingevuld_Vragenlijst.Include("Vragenlijst").Where(i => i.Inspecteur_ID == Userid && i.Inspectienummer == id);
            List<Vragenlijst> Questionnaires = db.Vragenlijst.Include("Inspectie").Where( i=> i.Inspectie.Any(k => k.Inspectienummer == id)).ToList();
            foreach(var item in AnsweredQuestionnairse)
            {
                int index = Questionnaires.FindIndex(i => i.ID == item.Vragenlijst.Stamt_af_van_ID);
                if(index >= 0)
                {
                    Questionnaires.RemoveAt(index);
                }
            }
            if(Questionnaires.Count == 0)
            {
                return View("AllQuestionnairesAnswered");
            }
            return View(Questionnaires);
        }

        // GET: Vragenlijst/Details/5
        public ActionResult Details(int? id)
        {
            var userId = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);

            vragenlijst.Bijlagevraag_vragenlijst = db.Bijlagevraag_vragenlijst.Where(v => v.Vragenlijst_ID == id).ToList();
            vragenlijst.Kaartvraag_vragenlijst = db.Kaartvraag_vragenlijst.Where(v => v.Vragenlijst_ID == id).ToList();
            vragenlijst.Meerkeuzevraag_vragenlijst = db.Meerkeuzevraag_vragenlijst.Where(v => v.Vragenlijst_ID == id).ToList();
            vragenlijst.Openvraag_vragenlijst = db.Openvraag_vragenlijst.Where(v => v.Vragenlijst_ID == id).ToList();
            vragenlijst.Tabelvraag_vragenlijst = db.Tabelvraag_vragenlijst.Where(v => v.Vragenlijst_ID == id).ToList();

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
