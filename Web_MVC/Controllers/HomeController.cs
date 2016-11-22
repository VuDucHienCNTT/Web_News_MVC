using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities();

        // GET: Home
        public ActionResult Index()
        {
            List<News> lstNews = db.News.Take(4).ToList();
            return View(lstNews);
        }

        public PartialViewResult Menu()
        {
            var menu = db.Categories.Where(n => n.ParentId == null).ToList();
            return PartialView(menu);
        }
        public PartialViewResult TheLoaiBaiViet(int id = 0)
        {
            var dsTheloaitintuc = db.Categories.ToList();
            return PartialView(dsTheloaitintuc);
        }

        public ActionResult TrangTheLoai(int id)
        {
            var dstintuc = db.News.Where(n => n.CategoryId == id).OrderByDescending(n => n.Id).ToList();
            return View(dstintuc);
        }

        public PartialViewResult TheLoaiDropdown()
        {
            var dsTheloaitintuc = db.Categories.Where(n => !(n.Url == "" && n.ParentId == null)).ToList();
            return PartialView(dsTheloaitintuc);
        }

        public PartialViewResult TinTucCungChuyenMuc(int id)
        {
            var idtl = db.News.Find(id).CategoryId;
            var dstincungCM = db.News.Where(n => n.Id != id && n.CategoryId == idtl && n.TrangThai == "Đã duyệt").OrderByDescending(n => n.Dateposted).Take(10).ToList();
            return PartialView(dstincungCM);
        }


        public PartialViewResult TinTucMoiNhat()
        {
            var dstinmoinhat = db.News.Where(n => n.TrangThai == "Đã duyệt").ToList();
            return PartialView(dstinmoinhat);
        }

        public ActionResult TimKiem(string tukhoa)
        {
            var lstKetqua = db.News.Where(s => s.Title.Contains(tukhoa) || s.Summary.Contains(tukhoa)).ToList();
            if(lstKetqua.Count>0)
            {
                ViewBag.TB = "Tìm được " + lstKetqua.Count + " tin tức";
            }
            return View(lstKetqua);

        }

        public ActionResult Detail(int id = 0)
        {
            News news = db.News.SingleOrDefault(n => n.Id == id);
            news.View = news.View + 1;
            db.SaveChanges();
            if (news == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(news);
        }
    }
}