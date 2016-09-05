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
        News_Web_MVCEntities db = new News_Web_MVCEntities();
        // GET: Home
        public ActionResult Index()
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                List<Category> lstCategory = this.db.Categories.Where(n => n.Parent == -1).ToList();
                return View(lstCategory);
            }
        }
    }
}