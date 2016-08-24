using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        [HttpGet]
        public ActionResult Index()
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                List<News> lstNews = db.News.ToList();
                return View(lstNews);
            }
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string title = collection["title"].ToString();
            string summary = collection["summary"].ToString();
            string content = collection["content"].ToString();
            string dateposted = collection["dateposted"].ToString();
            string author = collection["author"].ToString();
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                News news = new News();
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Date_posted = dateposted;
                news.Author = author;

                db.News.Add(news);
                db.SaveChanges();

                List<News> lstNews = db.News.ToList();
                return View(lstNews);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                News news = db.News.SingleOrDefault(n => n.Id == id);
                return View(news);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                string title = collection["title"].ToString();
                string summary = collection["summary"].ToString();
                string content = collection["content"].ToString();
                string dateposted = collection["dateposted"].ToString();
                string author = collection["author"].ToString();

                News news = db.News.SingleOrDefault(n => n.Id == id);

                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Date_posted = dateposted;
                news.Author = author;

                db.SaveChanges();

                List<News> lstNews = db.News.ToList();
                return RedirectToAction("Index", lstNews);
            }
        }

        public ActionResult Delete(int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);

                db.News.Remove(news);

                db.SaveChanges();

                List<News> lstNews = db.News.ToList();
                return RedirectToAction("Index", lstNews);
            }
        }
    }
}