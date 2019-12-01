using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestiSpec.Domain.Model;

namespace FestiSpecWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Applicant()
        {
            var ins = new NAW_inspecteur();
            return View(ins);
        }

        public ActionResult Contact()
        {
            return View();
        }
        

    }
}