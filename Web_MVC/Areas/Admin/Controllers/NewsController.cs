using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace Web_MVC.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        [HttpGet]
        public ActionResult Create(int? page)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            string title = collection["title"].ToString();
            string summary = collection["summary"].ToString();
            string content = collection["content"].ToString();
            string dateposted = collection["dateposted"].ToString();
            string author = collection["author"].ToString();
            string poster = collection["poster"].ToString();
            string avatar = collection["avatar"].ToString();

            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                News news = new News();
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Dateposted = dateposted;
                news.Author = author;
                news.Poster = poster;
                news.Avatar = avatar;

                db.News.Add(news);
                db.SaveChanges();
                return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                News news = db.News.SingleOrDefault(n => n.Id == id);
                if (news == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(news);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection collection, int id)
        {
            string title = collection["title"].ToString();
            string summary = collection["summary"].ToString();
            string content = collection["content"].ToString();
            string dateposted = collection["dateposted"].ToString();
            string author = collection["author"].ToString();
            string poster = collection["poster"].ToString();
            string avatar = collection["avatar"].ToString();

            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                News news = db.News.SingleOrDefault(n => n.Id == id);
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Dateposted = dateposted;
                news.Author = author;
                news.Poster = poster;
                news.Avatar = avatar;

                db.SaveChanges();

                return RedirectToAction("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);

                db.News.Remove(news);

                db.SaveChanges();

                return RedirectToAction("Create");
            }
        }



        public string ChangeImage(int id, string avatar)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                if (id == null)
                {
                    return "Mã không tồn tại";
                }
                News news = db.News.Find(id);
                if (news == null)
                {
                    return "Mã không tồn tại";
                }
                news.Avatar = avatar;
                db.SaveChanges();
                return "";
            }
        }


        [HttpPost]
        public ActionResult Search(FormCollection collection, int? page)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string tukhoa = collection["txtsearch"].ToString();
                ViewBag.TuKhoa = tukhoa;
                List<News> lstResult = db.News.Where(n => n.Title.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 1;
                if (lstResult.Count == 0)
                {
                    ViewBag.khongtimthay = "Không tìm thấy";

                }
                ViewBag.timthay = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Title).ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult Search(int? page, string tukhoa)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                ViewBag.TuKhoa = tukhoa;
                List<News> lstResult = db.News.Where(n => n.Title.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 1;
                if (lstResult.Count == 0)
                {
                    ViewBag.khongtimthay = "Không tìm thấy ";
                }

                ViewBag.timthay = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Title).ToPagedList(pageNumber, pageSize));
            }
        }

    }
}