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
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {

                var lstQuickNews = db.QuickNews.Select(n => new
                {
                    ID = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    Author = n.Author
                }).ToList();
                return Json(new { QuickNews1 = lstQuickNews }, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: //QuickNews/AddQuickNews/
        public JsonResult AddQuickNews(string title, string content, string author)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                try
                {
                    QuickNew addquicknew = new QuickNew();

                    addquicknew.Title = title;
                    addquicknew.Content = content;
                    addquicknew.Author = author;
                    db.QuickNews.Add(addquicknew);
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = addquicknew.Id,
                        Title = addquicknew.Title,
                        Content = addquicknew.Content,
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
        public JsonResult EditQuickNews(int id, string title, string content, string author)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                try
                {
                    QuickNew editquicknew = db.QuickNews.SingleOrDefault(n=>n.Id==id);
                    editquicknew.Title = title;
                    editquicknew.Content = content;
                    editquicknew.Author = author;
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = editquicknew.Id,
                        Title = editquicknew.Title,
                        Content = editquicknew.Content,
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
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                QuickNew delquicknew = db.QuickNews.SingleOrDefault(n => n.Id == id);
                if(delquicknew !=null)
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