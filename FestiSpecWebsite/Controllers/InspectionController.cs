using FestiSpec.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FestiSpecWebsite.Controllers
{
    [Authorize]
    public class InspectionController : Controller
    {
        private FestiSpecEntities db = new FestiSpecEntities();

        // GET: Availability
        public ActionResult Index()
        {
            var id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);
            var Inspecties = db.Inspectie.Include("Inspecteur").Where(i => i.Voltooid == false && i.Inspecteur.Any(k => k.ID == id)).ToList();
            return View(Inspecties);
        }

    }
}