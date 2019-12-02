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
        public void Login()
        {
            NameValueCollection nvc = Request.Form;
            var test = "test";
            test = "";

            string userName = "", password = "";
            if (!string.IsNullOrEmpty(nvc["Username"]))
            {
                userName = nvc["Username"];
            }

            if (!string.IsNullOrEmpty(nvc["Wachtwoord"]))
            {
                password = nvc["Wachtwoord"];
            }

            using (var context = new FestiSpecEntities())
            {
                //Window was corrupt
                var targetPerson = context.Inspecteur
                    .FirstOrDefault(e => e.Wachtwoord == password && e.Username == userName);

                if (targetPerson == null)
                {
                    Console.WriteLine("failed to login");
                    /*      MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                }
                else
                {
                    Console.WriteLine("login ok");
                    string userData = JsonConvert.SerializeObject(targetPerson.Username);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                        1, targetPerson.Username, DateTime.Now, DateTime.Now.AddMinutes(120), false, userData
                    );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                    Response.Cookies.Add(faCookie);

                    /*        MenuView menuView = new MenuView();
                            menuView.Show();*/

                }
            }
            //  HttpContext.Session["userId"] = user.usersid;
        }


    }
}