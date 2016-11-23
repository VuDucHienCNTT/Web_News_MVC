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
                    TrangThai = n.TrangThai,
                    Quyen = n.Quyen,
                    PassWord = n.Password,
                    ConfPassWord = n.Confirmpassword,
                    Avatar = n.Avatar
                }).ToList();
                return Json(new { Account1 = lstAccount }, JsonRequestBehavior.AllowGet);
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
            string trangthai = "Hoạt động";
            string quyen = "User";

            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Account acc = new Account();
                var checkEmail = db.Accounts.SingleOrDefault(n => n.Email.Equals(email));
                if (checkEmail == null)
                {
                    
                    acc.Fullname = fullname;
                    acc.Email = email;
                    acc.Password = password;
                    acc.Confirmpassword = confpassword;
                    acc.Avatar = avatar;
                    acc.TrangThai = trangthai;
                    acc.Quyen = quyen;
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    ViewBag.tbDangKy = acc.Fullname + "  đăng ký thành công!";                  
                }
                else
                {
                    ViewBag.tbDangKyLoi = email + " đã tồn tại!";
                }
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
                    var ckeckAD = db.Accounts.SingleOrDefault(n => n.Email.Equals(user.Email) && n.Password.Equals(user.Password) && n.Quyen.Equals("Admin"));
                    if (ckeckAD != null)
                    {
                        Session["Id"] = ckeckAD.Id.ToString();
                        Session["Fullname"] = ckeckAD.Fullname.ToString();
                        Session["Email"] = ckeckAD.Email.ToString();
                        Session["Avatar"] = ckeckAD.Avatar.ToString();
                        return RedirectToAction("Create", "Categories");
                    }
                    var checkUS = db.Accounts.SingleOrDefault(n => n.Email.Equals(user.Email) && n.Password.Equals(user.Password) && n.Quyen.Equals("User") && n.TrangThai.Equals("Hoạt động"));
                    if (checkUS != null)
                    {
                        Session["Id"] = checkUS.Id.ToString();
                        Session["Fullname"] = checkUS.Fullname.ToString();
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        ViewBag.tbDangNhap = "Email/Password sai hoặc đã bị khóa";
                        return View();
                    }

                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/Home/Index");
        }
    }
}