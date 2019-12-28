using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    public class HomeController : Controller
    {
        private FestiSpecEntities _db = new FestiSpecEntities();

        public ActionResult Index()
        {
            try
            {
                var id = Convert.ToInt32(FormsAuthentication.Decrypt(Request.Cookies["Cookie1"].Value).Name);

                string name = "Welkom, " + _db.Inspecteur.Find(id)?.Username;
                ViewBag.Name = name;
            }
            catch
            {
                // ignored
            }

            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        

    }
}