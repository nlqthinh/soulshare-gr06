using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoulShare_Group06.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                // Attempt to retrieve the user from the session
                var user = (SoulShare_Group06.Models.Customer)System.Web.HttpContext.Current.Session["user"];

                // If the user is not logged in, redirect to the login page
                if (user == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                // If the user is logged in, display the home page
                return View();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                // For simplicity, you can redirect to the login page
                return RedirectToAction("Login", "Login");
            }
            //CookieHelper.Create("user-email", "customer", DateTime.Now.AddDays(10));
            //string user = CookieHelper.Get("user-email");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Clear the user session
            Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Home", "Home");
        }

    }
}