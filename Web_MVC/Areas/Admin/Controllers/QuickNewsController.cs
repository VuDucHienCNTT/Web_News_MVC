using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;
namespace Web_MVC.Areas.Admin.Controllers
{
    public class QuickNewsController : Controller
    {
        // GET: Admin/QuickNews
        public ActionResult Index()
        {
            return View();
        }
        // GET: //QuickNews/LoadQuickNews/
        public JsonResult LoadQuickNews()
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                var lstQuickNews = db.QuickNews.Select(n => new
                {
                    ID = n.Id,
                    Title = n.Title,
                    Summary = n.Summary,
                    Content = n.Content,
                    Poster = n.Poster,
                    Dateposted = n.Dateposted,
                    Author = n.Author
                }).ToList();
                return Json(new { QuickNews1 = lstQuickNews }, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: //QuickNews/AddQuickNews/
        public JsonResult AddQuickNews(string title, string summary, string content, string poster, string dateposted, string author)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                try
                {
                    QuickNew addquicknew = new QuickNew();

                    addquicknew.Title = title;
                    addquicknew.Summary = summary;
                    addquicknew.Content = content;
                    addquicknew.Poster = poster;
                    addquicknew.Dateposted = dateposted;
                    addquicknew.Author = author;
                    db.QuickNews.Add(addquicknew);
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = addquicknew.Id,
                        Title = addquicknew.Title,
                        Summary = addquicknew.Summary,
                        Content = addquicknew.Content,
                        Poster = addquicknew.Poster,
                        Dateposted = addquicknew.Dateposted,
                        Author = addquicknew.Author
                    });
                }
                catch
                {
                    return null;
                }
            }
        }
        // POST: //QuickNews/EditQuickNews/
        public JsonResult EditQuickNews(int id, string title, string summary, string content, string poster, string dateposted, string author)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                try
                {
                    QuickNew editquicknew = db.QuickNews.SingleOrDefault(n => n.Id == id);
                    editquicknew.Title = title;
                    editquicknew.Summary = summary;
                    editquicknew.Content = content;
                    editquicknew.Poster = poster;
                    editquicknew.Dateposted = dateposted;
                    editquicknew.Author = author;
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = editquicknew.Id,
                        Title = editquicknew.Title,
                        Summary = editquicknew.Summary,
                        Content = editquicknew.Content,
                        Poster = editquicknew.Poster,
                        Dateposted = editquicknew.Dateposted,
                        Author = editquicknew.Author
                    });
                }
                catch
                {
                    return null;
                }
            }
        }
        // POST: //QuickNews/DeleteQuickNews/
        public int DelQuickNews(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                QuickNew delquicknew = db.QuickNews.SingleOrDefault(n => n.Id == id);
                if (delquicknew != null)
                {
                    db.QuickNews.Remove(delquicknew);
                    db.SaveChanges();
                    return id;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}