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
            List<Category> lstCategory = db.Categories.Where(n => n.ParentId == null).ToList();
            return View(lstCategory);
        }
    }
}