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
        public ActionResult Create(int? page)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
                if (Session["Id"] != null)
                {
                    int pageSize = 3;
                    int pageNumber = (page ?? 1);

                    ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                    ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name");
                    ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");

                    List<News> lstCategory = db.News.ToList();
                    return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("Signin", "Account");
                }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            string title = collection["title"].ToString();
            string summary = collection["summary"].ToString();
            string content = collection["content"].ToString();
            string dateposted = collection["dateposted"].ToString();
            int authorid = int.Parse(collection["authorid"].ToString());
            int posterid = int.Parse(collection["posterid"].ToString());
            string avatar = collection["avatar"].ToString();
            int categoryid = int.Parse(collection["categoryid"].ToString());

            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name");
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");

                News news = new News();
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Dateposted = dateposted;
                news.AuthorId = authorid;
                news.PosterId = posterid;
                news.Avatar = avatar;
                news.CategoryId = categoryid;

                db.News.Add(news);
                db.SaveChanges();
                return View(db.News.ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);
                if (news == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname", news.PosterId);
                ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name", news.AuthorId);
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", news.CategoryId);
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
            int authorid = int.Parse(collection["authorid"].ToString());
            int posterid = int.Parse(collection["posterid"].ToString());
            string avatar = collection["avatar"].ToString();
            int categoryid = int.Parse(collection["categoryid"].ToString());

            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);
                news.Title = title;
                news.Summary = summary;
                news.Content = content;
                news.Dateposted = dateposted;
                news.AuthorId = authorid;
                news.PosterId = posterid;
                news.Avatar = avatar;
                news.CategoryId = categoryid;

                ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname", news.PosterId);
                ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name", news.AuthorId);
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", news.CategoryId);
                db.SaveChanges();

                return RedirectToAction("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {

                News news = db.News.SingleOrDefault(n => n.Id == id);

                db.News.Remove(news);

                db.SaveChanges();

                return RedirectToAction("Create");
            }
        }

        public string ChangeImage(int id, string avatar)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                if (id == 0)
                {
                    return "Mã không tồn tại";
                }
                News news = db.News.Find(id);
                if (news == null)
                {
                    return "Mã không tồn tại";
                }
                news.Avatar = avatar;
                db.SaveChanges();
                return "";
            }
        }


        [HttpPost]
        public ActionResult Search(FormCollection collection, int? page)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string tukhoa = collection["txtsearch"].ToString();
                ViewBag.TuKhoa = tukhoa;
                //
                ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name");
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");

                List<News> lstResult = db.News.Where(n => n.Title.Contains(tukhoa) || n.Summary.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 1;
                if (lstResult.Count == 0)
                {
                    ViewBag.searchfalse = "Không tìm thấy";
                }
                ViewBag.search = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
            }
        }
        // Viết Hàm Get để khi sang next trang vẫn tìm kiếm đc từ khóa
        [HttpGet]
        public ActionResult Search(int? page, string tukhoa)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                ViewBag.TuKhoa = tukhoa;
                //
                ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name");
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");

                List<News> lstResult = db.News.Where(n => n.Title.Contains(tukhoa) || n.Summary.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 1;
                if (lstResult.Count == 0)
                {
                    ViewBag.searchfalse = "Không tìm thấy ";
                }

                ViewBag.search = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));

            }
        }

    }
}