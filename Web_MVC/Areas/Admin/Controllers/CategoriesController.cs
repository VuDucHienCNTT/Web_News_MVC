﻿using System;
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
        public ActionResult Add()
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                List<Category> lstCategory = db.Categories.ToList();
                return View(lstCategory);
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection collection)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                string cateGoryname = collection["categoryname"].ToString();
                int parentCategory = collection["parentcategory"].ToString() != "" ? Convert.ToInt32(collection["parentcategory"].ToString()) : -1;

                //Them chuyen muc vao csdl
                //Khoi tao doi tuong muon them vao csdl
                Category cate = new Category();
                cate.Name = cateGoryname;
                cate.Parent = parentCategory;

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
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                Category cate = db.Categories.SingleOrDefault(n => n.Id == id);
                if(cate == null)
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
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                string cateGoryname = collection["categoryname"].ToString();
                int parentCategory = collection["parentcategory"].ToString() != "" ? Convert.ToInt32(collection["parentcategory"].ToString()) : -1;
                Category cate = db.Categories.SingleOrDefault(n => n.Id == id);
                cate.Name = cateGoryname;
                cate.Parent = parentCategory;

                db.SaveChanges();
                return RedirectToAction("Add");
            }
        }
        public ActionResult Delete(int id)
        {
            using (News_Web_MVCEntities db = new News_Web_MVCEntities())
            {
                //Lấy đối tượng cần xóa
                Category cate = db.Categories.SingleOrDefault(x => x.Id == id);

                //Thực hiện xóa đối tượng
                db.Categories.Remove(cate);

                //Thực hiện thay đổi trong csdl
                db.SaveChanges();

                //Lay danh sach chuyen muc trong csdl tra ve view
                return RedirectToAction("Add");
            }
        }
    }
}