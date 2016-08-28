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
        public ActionResult Add(int? page)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection collection, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            string title = collection["title"].ToString();
            string summary = collection["summary"].ToString();
            string content = collection["content"].ToString();
            string dateposted = collection["dateposted"].ToString();
            string author = collection["author"].ToString();
            string poster = collection["poster"].ToString();

            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                News news = new News();
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Date_posted = dateposted;
                news.Author = author;
                news.Poster = poster;

                db.News.Add(news);
                db.SaveChanges();
                return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
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

            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {


                News news = db.News.SingleOrDefault(n => n.Id == id);
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Date_posted = dateposted;
                news.Author = author;
                news.Poster = poster;

                db.SaveChanges();

                return RedirectToAction("Add");
            }
        }

        public ActionResult Delete(int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);

                db.News.Remove(news);

                db.SaveChanges();
              
                return RedirectToAction("Add");
            }
        }
    }
}