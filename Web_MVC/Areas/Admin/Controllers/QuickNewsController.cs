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
    public class QuickNewsController : Controller
    {
        Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities();
        // GET: Admin/QuickNews
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAuthor()
        {
            //ok, nó không convert được đối tượng Author, mà phải tạo cái mới
            var authors = from a in db.Authors select new { id = a.Id, name = a.Name };
                return Json(authors.ToList(),JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult GetPoster()
        {
            var posters = from a in db.Accounts select new { id = a.Id, name = a.Fullname };
            return Json(posters.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategory()
        {
            var categorys = from a in db.Categories select new { id = a.Id, name = a.Name };
            return Json(categorys.ToList(), JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetAuthors()
        //{
        //    return View(db.Authors.ToList());
        //}
        // GET: //QuickNews/LoadQuickNews/
        public JsonResult LoadQuickNews()
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                //
                //ViewBag.PosterId = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                //ViewBag.AuthorId = new SelectList(db.Authors.ToList(), "Id", "Name");

                var lstQuickNews = db.QuickNews.Select(n => new
                {
                    ID = n.Id,
                    Title = n.Title,
                    Summary = n.Summary,
                    Content = n.Content,
                    PosterId = n.PosterId,
                    Dateposted = n.Dateposted,
                    AuthorId = n.AuthorId,
                    CategoryId=n.CategoryId
                }).ToList();
                return Json(new { QuickNews1 = lstQuickNews }, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: //QuickNews/AddQuickNews/
        public JsonResult AddQuickNews(string title, string summary, string content, int posterid, string dateposted, int authorid,int categoryid)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                try
                {
                    QuickNew addquicknew = new QuickNew();

                    addquicknew.Title = title;
                    addquicknew.Summary = summary;
                    addquicknew.Content = content;
                    addquicknew.PosterId = posterid;
                    addquicknew.Dateposted = dateposted;
                    addquicknew.AuthorId = authorid;
                    addquicknew.CategoryId = categoryid;
                    db.QuickNews.Add(addquicknew);
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = addquicknew.Id,
                        Title = addquicknew.Title,
                        Summary = addquicknew.Summary,
                        Content = addquicknew.Content,
                        PosterId = addquicknew.PosterId,
                        Dateposted = addquicknew.Dateposted,
                        AuthorId = addquicknew.AuthorId,
                        CategoryId = addquicknew.CategoryId
                    });
                }
                catch
                {
                    return null;
                }
            }
        }
        // POST: //QuickNews/EditQuickNews/
        public JsonResult EditQuickNews(int id, string title, string summary, string content, int posterid, string dateposted, int authorid, int categoryid)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                try
                {
                    QuickNew editquicknew = db.QuickNews.SingleOrDefault(n => n.Id == id);
                    editquicknew.Title = title;
                    editquicknew.Summary = summary;
                    editquicknew.Content = content;
                    editquicknew.PosterId = posterid;
                    editquicknew.Dateposted = dateposted;
                    editquicknew.AuthorId = authorid;
                    editquicknew.CategoryId = categoryid;
                    db.SaveChanges();
                    return Json(new
                    {
                        ID = editquicknew.Id,
                        Title = editquicknew.Title,
                        Summary = editquicknew.Summary,
                        Content = editquicknew.Content,
                        PosterId = editquicknew.PosterId,
                        Dateposted = editquicknew.Dateposted,
                        AuthorId = editquicknew.AuthorId,
                        CategoryId = editquicknew.CategoryId
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

        [HttpPost]
        public ActionResult Search(FormCollection collection, int? page)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string tukhoa = collection["txtsearch"].ToString();
                ViewBag.TuKhoa = tukhoa;
                ////
                //ViewBag.Poster = new SelectList(db.Accounts.ToList(), "Id", "Fullname");
                //ViewBag.Author = new SelectList(db.Authors.ToList(), "Id", "Name");

                List<QuickNew> lstResult = db.QuickNews.Where(n => n.Title.Contains(tukhoa) || n.Summary.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 1;
                if (lstResult.Count == 0)
                {
                    ViewBag.searchfalse = "Không tìm thấy";
                }
                ViewBag.search = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Title).ToPagedList(pageNumber, pageSize));
            }
        }
    }
}