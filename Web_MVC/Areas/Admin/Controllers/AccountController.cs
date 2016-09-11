using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(FormCollection collection)
        {
            string fullname = collection["fullname"].ToString();
            string email = collection["email"].ToString();
            string username = collection["username"].ToString();
            string password = collection["password"].ToString();
            string confpassword = collection["confpassword"].ToString();
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Account acc = new Account();
                acc.Fullname = fullname;
                acc.Email = email;
                acc.Username = username;
                acc.Password = password;
                acc.Confirmpassword = confpassword;
                db.Accounts.Add(acc);
                db.SaveChanges();
                ViewBag.tbDangKy = acc.Fullname + "  đăng ký thành công!";
                return View();
            }
        }

        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(Account user)
        {
            if (ModelState.IsValid)
            {
                using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
                {
                    var u = db.Accounts.SingleOrDefault(n => n.Username.Equals(user.Username) && n.Password.Equals(user.Password));
                    if (u != null)
                    {
                        Session["Id"] = u.Id.ToString();
                        Session["Fullname"] = u.Fullname.ToString();
                        Session["Email"] = u.Email.ToString();
                        return RedirectToAction("Create", "Categories");
                    }
                    else
                    {
                        ViewBag.tbDangNhap = "Username hoặc Password không đúng!";
                        return View();
                    }

                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Signin");
        }
    }
}