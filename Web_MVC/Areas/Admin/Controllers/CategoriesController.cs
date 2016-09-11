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
                if (Session["Id"] != null)
                {
                    List<Category> lstCategory = db.Categories.ToList();
                    return View(lstCategory);

                }
                else
                {
                    return RedirectToAction("Signin", "Account");
                }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
            {
                string cateGoryname = collection["categoryname"].ToString();
                int parentCategory = collection["parentcategory"].ToString() != "" ? Convert.ToInt32(collection["parentcategory"].ToString()) : -1;

                //Them chuyen muc vao csdl
                //Khoi tao doi tuong muon them vao csdl
                Category cate = new Category();
                cate.Name = cateGoryname;
                cate.ParentId = parentCategory;

                //Them doi tuong vao csdl
                db.Categories.Add(cate);
                //Thuc hien ghi vao csdl
                db.SaveChanges();

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
                string cateGoryname = collection["categoryname"].ToString();
                int parentCategory = collection["parentcategory"].ToString() != "" ? Convert.ToInt32(collection["parentcategory"].ToString()) : -1;
                Category cate = db.Categories.SingleOrDefault(n => n.Id == id);
                cate.Name = cateGoryname;
                cate.ParentId = parentCategory;

                db.SaveChanges();
                return RedirectToAction("Create");
            }
        }
        public ActionResult Delete(int id)
        {
            using (Web_NEWS_MVCEntities db = new Web_NEWS_MVCEntities())
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