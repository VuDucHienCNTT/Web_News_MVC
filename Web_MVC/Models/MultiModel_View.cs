using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_MVC.Models
{
    public class MultiModel_View
    {
        public List<Category> dsTheloai { get; set; }
        public List<News> dsTinTuc { get; set; }
        public MultiModel_View(List<Category> danhsachTheloai, List<News> danhsachTintuc)
        {
            this.dsTheloai = danhsachTheloai;
            this.dsTinTuc = danhsachTintuc;
        }
    }
}