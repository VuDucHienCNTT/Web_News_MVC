using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Controllers
{
    public class NewsController : Controller
    {
        Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities();
        // GET: News
        public ActionResult Index()
        {
            //List<News> lstNews = db.News.ToList();
            //return View(lstNews);
            List<Category> lstCategory = db.Categories.Where(n => n.ParentId == null).ToList();
            return View(lstCategory);
        }
    }
}