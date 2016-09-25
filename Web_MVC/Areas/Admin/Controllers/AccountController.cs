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

        //GET: //Account/LoadAccount
        public JsonResult LoadAccount()
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                var lstAccount = db.Accounts.Select(n => new
                {
                    ID = n.Id,
                    FullName = n.Fullname,
                    Email = n.Email,
                    PassWord = n.Password,
                    ConfPassWord =n.Confirmpassword,
                    Avatar= n.Avatar
                }).ToList();
                return Json(new { Account1 = lstAccount}, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: //Account/DelAccount/
        public int DelAccount(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Account delaccount = db.Accounts.SingleOrDefault(n => n.Id == id);
                if (delaccount != null)
                {
                    db.Accounts.Remove(delaccount);
                    db.SaveChanges();
                    return id;
                }
                else
                {
                    return -1;
                }
            }
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
            string password = collection["password"].ToString();
            string confpassword = collection["confpassword"].ToString();
            string avatar = collection["avatar"].ToString();
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Account acc = new Account();
                acc.Fullname = fullname;
                acc.Email = email;
                acc.Password = password;
                acc.Confirmpassword = confpassword;
                acc.Avatar = avatar;
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
                    var u = db.Accounts.SingleOrDefault(n => n.Email.Equals(user.Email) && n.Password.Equals(user.Password));
                    if (u != null)
                    {
                        Session["Id"] = u.Id.ToString();
                        Session["Fullname"] = u.Fullname.ToString();
                        Session["Email"] = u.Email.ToString();
                        Session["Avatar"] = u.Avatar.ToString();
                        return RedirectToAction("Create", "Categories");
                    }
                    else
                    {
                        ViewBag.tbDangNhap = "Email và/hoặc Password không đúng!";
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