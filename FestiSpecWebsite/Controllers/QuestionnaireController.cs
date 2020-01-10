using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FestiSpec.Domain.Model;
using FestiSpecWebsite.Models.QuestionnaireFolder;

namespace FestiSpecWebsite.Controllers
{
    [Authorize]
    public class QuestionnaireController : Controller
    {
        private FestiSpecEntities db = new FestiSpecEntities();
        

        // GET: Vragenlijst
        public ActionResult Index( int? id)
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
            QuestionnaireList questionnaireList = new QuestionnaireList();
            questionnaireList.questionnaires = Questionnaires;
            questionnaireList.id = id;
           
            return View(questionnaireList);
        }


        // GET: Vragenlijst/Details/5
        public ActionResult loadQuestionaire(int inspectionId, int? id)
        {
            var userId = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(!db.Inspectie.Any(i => i.Inspectienummer == inspectionId && i.Vragenlijst.Any(v => v.ID == id))){

                return View("NotPlannedInInspection");
            }
            Vragenlijst vragenlijst = db.Vragenlijst.Find(id);
            QuestionnaireVM questionnaire = new QuestionnaireVM(vragenlijst);
            questionnaire.loadQuestions();
            questionnaire.inspectionId = inspectionId;
            if (vragenlijst == null)
            {
                return HttpNotFound();
            }
            return View("Details", questionnaire);
        }
        ActionResult Details(QuestionnaireVM id)
        {
            return View(id);
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
        [HttpPost]

        
        public ActionResult PostDetails(int inspectionId, int? questionnaireID, QuestionnaireVM questionnaire)
        {
            var Userid = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            if (db.Inspectie_Wel_Ingevuld_Vragenlijst.Any(iwiv => iwiv.Vragenlijst.Stamt_af_van_ID == questionnaireID && iwiv.Inspectienummer == inspectionId && iwiv.Inspecteur_ID == Userid))
            {
                return View("NotPlannedInInspection");
            }
            
            if (ModelState.IsValid)
            {


                Vragenlijst  v = new Vragenlijst();
                v.Stamt_af_van_ID = questionnaire.ID;
                v.Versie = questionnaire.Version;
                v.Titel = questionnaire.Title;
                v.Is_Ingevuld = true;
                v.Actief = false;
                db.Vragenlijst.Add(v);
                db.SaveChanges();
                int dbid = v.ID;
                Inspectie_Wel_Ingevuld_Vragenlijst inspectie_Wel_Ingevuld_Vragenlijst = new Inspectie_Wel_Ingevuld_Vragenlijst();
                inspectie_Wel_Ingevuld_Vragenlijst.Inspecteur_ID = Userid;
                inspectie_Wel_Ingevuld_Vragenlijst.Vragenlijst_ID = v.ID;
                inspectie_Wel_Ingevuld_Vragenlijst.Inspectienummer = inspectionId;
                db.Inspectie_Wel_Ingevuld_Vragenlijst.Add(inspectie_Wel_Ingevuld_Vragenlijst);
                //foreach(var i in questionnaire.mapQuestionsList)
                //{
                //    Kaartvraag kv = new Kaartvraag();

                //}
                if(questionnaire.appendixQuestionsList != null)
                {
                    foreach (var i in questionnaire.appendixQuestionsList)
                    {
                        Bijlagevraag_vragenlijst bv = new Bijlagevraag_vragenlijst();
                        bv.Bijlagevraag_ID = i.Id;
                        bv.Vragenlijst_ID = dbid;
                        MemoryStream target = new MemoryStream();
                        i.imageFile.InputStream.CopyTo(target);
                        byte[] data = target.ToArray();
                        bv.FileBytes = data;
                        bv.Positie = i.ListPosition;
                        db.Bijlagevraag_vragenlijst.Add(bv);

                    }
                }
 
                if(questionnaire.openQuestionsList != null)
                {
                    foreach (var i in questionnaire.openQuestionsList)
                    {
                        Openvraag_vragenlijst ov = new Openvraag_vragenlijst();
                        ov.Openvraag_ID = i.Id;
                        ov.Vragenlijst_ID = dbid;
                        ov.Positie = i.ListPosition;
                        ov.Antwoord = i.Answer;
                        db.Openvraag_vragenlijst.Add(ov);
                    }
                }
                if(questionnaire.multipleChoiceQuestionsList != null)
                {
                    foreach (var i in questionnaire.multipleChoiceQuestionsList)
                    {
                        Meerkeuzevraag_vragenlijst mv = new Meerkeuzevraag_vragenlijst();
                        mv.Meerkeuzevraag_ID = i.Id;
                        mv.Vragenlijst_ID = dbid;
                        mv.Positie = i.ListPosition;
                        mv.Antwoord = i.Answer;
                        db.Meerkeuzevraag_vragenlijst.Add(mv);
                    }
                }
                if(questionnaire.TableQuestionsList != null)
                {
                    foreach (var i in questionnaire.TableQuestionsList)
                    {
                        for(int s = 0;s<i.Situations.Count(); s++)
                        {
                            Tabelvraag_antwoord ta = new Tabelvraag_antwoord();
                            ta.Situatie = i.Situations[s];
                            ta.Antwoord = i.Answers[s];
                            ta.Tabelvraag_ID = i.Id;
                            ta.Vragenlijst_ID = dbid;
                            db.Tabelvraag_antwoord.Add(ta);
                        }
                        Tabelvraag_vragenlijst tv = new Tabelvraag_vragenlijst();
                        tv.Positie = i.ListPosition;
                        tv.Tabelvraag_ID = i.Id;
                        tv.Vragenlijst_ID = dbid;
                        db.Tabelvraag_vragenlijst.Add(tv);
                        
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Inspection");

            }


            return View("Details", questionnaire);

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
