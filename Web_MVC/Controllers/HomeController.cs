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
        // GET: Home
        public ActionResult Index()
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                List<Category> lstCategory = db.Categories.Where(n => n.Parent == 0).ToList();
                return View(lstCategory);
            }
        }
    }
}