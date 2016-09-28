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
            List<object> mymodel = new List<object>();
            mymodel.Add(db.Categories.Where(n => n.ParentId == null).ToList());
            mymodel.Add(db.News.Take(4).ToList());
            return View(mymodel);
        }
     
        public ActionResult Detail(int id = 0)
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
}