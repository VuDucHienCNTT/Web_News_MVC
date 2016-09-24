using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        [HttpGet]
        public ActionResult Create()
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                List<Author> lstAuthor = db.Authors.ToList();
                return View(lstAuthor);
            }

        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string authorName = collection["authorname"].ToString();
                string authorAddress = collection["authoraddress"].ToString();

                Author aut = new Author();
                aut.Name = authorName;
                aut.Address = authorAddress;

                db.Authors.Add(aut);
                db.SaveChanges();

                List<Author> lstAuthor = db.Authors.ToList();
                return View(lstAuthor);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Author aut = db.Authors.SingleOrDefault(n => n.Id == id);
                if(aut==null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                return View(aut);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string authorName = collection["authorname"].ToString();
                string authorAddress = collection["authoraddress"].ToString();
                Author aut = db.Authors.SingleOrDefault(n => n.Id == id);
                aut.Name = authorName;
                aut.Address = authorAddress;

                db.SaveChanges();
                return RedirectToAction("Create");
               
            }
        }

        public ActionResult Delete(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                //lay doi tuong can xoa
                Author aut = db.Authors.SingleOrDefault(n => n.Id == id);

                // thuc hien xoa doi tuong
                db.Authors.Remove(aut);

                //Thay doi trong csdl
                db.SaveChanges();
                return RedirectToAction("Create");
            }
        }
    }
}