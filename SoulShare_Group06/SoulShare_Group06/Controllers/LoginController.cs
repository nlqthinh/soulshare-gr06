using SoulShare_Group06.Models;
using SoulShare_Group06.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SoulShare_Group06.Controllers
{
    public class LoginController : Controller
    {
        DBContextSoulShare db = new DBContextSoulShare();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var f_password = GetMD5(password);
            //1.Kiem tra ten dang nhap - mat khau co giong ko => ve trang chu neu thieu thong tin
            if (string.IsNullOrEmpty(email) == true || string.IsNullOrEmpty(password) == true)
            {
                ViewBag.error = "Email or password not correct!";
                return View();
            }
            //2. Tim tai khoan trong db
            var useremail = new MapLogin().Detail(email);
            //3. Kiem tra ton tai tai khoan -> ko ton tai => ve trang chu
            if (useremail == null)
            {
                ViewBag.error = "Email or password not correct";
                ViewBag.email = email;
                return View();

            }
            //4. Kiem tra mk => sai => nhap lai
            if (useremail.account_password != f_password)
            {
                ViewBag.error = "Email or password not correct";
                ViewBag.email = email;
                return View();
            }
            //5. Tai khoan dang nhap ok: luu lai session
            Session["user"] = useremail;
            //6. Chuyen huong sang trang chu

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Customers.FirstOrDefault(s => s.customer_email == _user.customer_email);
                if (check == null)
                {
                    // Set customer_role to 1
                    _user.customer_role = 1;
                    _user.account_password = GetMD5(_user.account_password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }

}
