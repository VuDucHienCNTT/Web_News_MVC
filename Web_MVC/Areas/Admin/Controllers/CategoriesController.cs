using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {

        // GET: Admin/Categories
        [HttpGet]
        public ActionResult Create()
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                if (Session["Id"] != null)
                {
                    ViewBag.ParentId = new SelectList(db.Categories.Where(n => n.ParentId == null), "Id", "Name").ToList();
                    List<Category> lstCategory = db.Categories.ToList();
                    return View(lstCategory);
                }
                else
                {
                    return RedirectToAction("Signin", "Account");
                }
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string nameCategory = collection["namecategory"].ToString();
                string urlCategory = collection["urlcategory"].ToString();
                string parentIdCategory = collection["ParentId"].ToString();
                Category cate = new Category();
                cate.Name = nameCategory;
                if (urlCategory == null)
                {
                    cate.Url = null;
                }
                else
                {
                    cate.Url = urlCategory;
                }

                if (parentIdCategory == "NULL")
                {
                    cate.ParentId = null;
                }
                else
                {
                    cate.ParentId = Convert.ToInt32(parentIdCategory);
                }

                //Them doi tuong vao csdl
                db.Categories.Add(cate);
                //Thuc hien ghi vao csdl
                db.SaveChanges();
                ViewBag.ParentId = new SelectList(db.Categories.Where(n => n.ParentId == null), "Id", "Name").ToList();
                //Lay danh sach chuyen muc trong csdl tra ve view
                List<Category> lstCategory = db.Categories.ToList();
                return View(lstCategory);

            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                Category cate = db.Categories.SingleOrDefault(n => n.Id == id);
                ViewBag.ParentId = new SelectList(db.Categories.Where(n => n.ParentId == null), "Id", "Name", cate.ParentId).ToList();
                if (cate == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cate);

            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {

                string nameCategory = collection["namecategory"].ToString();
                string urlCategory = collection["urlcategory"].ToString();
                string parentIdCategory = collection["ParentId"].ToString();
                Category cate = db.Categories.SingleOrDefault(n => n.Id == id);
                ViewBag.ParentId = new SelectList(db.Categories.Where(n => n.ParentId == null), "Id", "Name", cate.ParentId).ToList();
                cate.Name = nameCategory;

                if (parentIdCategory == "NULL")
                {
                    cate.ParentId = null;
                }
                else
                {
                    cate.ParentId = Convert.ToInt32(parentIdCategory);
                }

                cate.Url = urlCategory;

                db.SaveChanges();
                return RedirectToAction("Create");
            }
        }
        public ActionResult Delete(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                try
                {
                    //Lấy đối tượng cần xóa
                    Category cate = db.Categories.SingleOrDefault(x => x.Id == id);

                    //Thực hiện xóa đối tượng
                    db.Categories.Remove(cate);

                    //Thực hiện thay đổi trong csdl
                    db.SaveChanges();

                    //Lay danh sach chuyen muc trong csdl tra ve view
                    return RedirectToAction("Create");
                }
                catch
                {
                    return RedirectToAction("Create");
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

                List<Category> lstResult = db.Categories.Where(n => n.Name.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 3;
                if (lstResult.Count == 0)
                {
                    ViewBag.khongtimthay = "Không tìm thấy";
                }
                ViewBag.timthay = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Name).ToList());
            }
        }

        // Viết Hàm Get để khi sang next trang vẫn tìm kiếm đc từ khóa đấy 
        [HttpGet]
        public ActionResult Search(int? page, string tukhoa)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                ViewBag.TuKhoa = tukhoa;
                List<Category> lstResult = db.Categories.Where(n => n.Name.Contains(tukhoa)).ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 3;
                if (lstResult.Count == 0)
                {
                    ViewBag.khongtimthay = "Không tìm thấy ";
                }

                ViewBag.timthay = "Đã tìm thấy " + lstResult.Count + "";
                return View(lstResult.OrderBy(n => n.Name).ToList());
            }
        }

    }
}