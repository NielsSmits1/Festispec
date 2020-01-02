using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FestiSpec.Domain.Model;
using Newtonsoft.Json;

namespace FestiSpecWebsite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            NameValueCollection nvc = Request.Form;


            string userName = "", password = "";
            if (!string.IsNullOrEmpty(nvc["Username"]))
            {
                userName = nvc["Username"];
            }

            if (!string.IsNullOrEmpty(nvc["Wachtwoord"]))
            {
                password = nvc["Wachtwoord"];
            }

            using (var context = new FestiSpecEntities1())
            {
                var targetPerson = context.Inspecteur
                    .FirstOrDefault(e => e.Wachtwoord == password && e.Username == userName);

                if (targetPerson == null)
                {
                    //todo give error
                    //failed to login
                    return RedirectToAction("Index", "Login", null);
                }
                else
                {
                    //login ok
                    string userData = JsonConvert.SerializeObject(targetPerson.Username);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                        1, targetPerson.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(120), false, userData
                    );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                    Response.Cookies.Add(faCookie);
                }
            }
            return RedirectToAction("Index", "Home", null);
        }

        [Authorize]
        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }
    }
}