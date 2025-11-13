using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Web_BanSach.Controllers
{
    public class SachController : Controller
    {
        QL_SACHEntities sach = new QL_SACHEntities();
        // GET: Sach
        public ActionResult Index()
        {
            //làm theo cách bình thường
            //List<BANGSACH> dsSach = sach.BANGSACHes.ToList();
            //lấy cả tên loại sách và nhà xuất bản
            List<BANGSACH> dsSachs = sach.BANGSACHes.Include(s => s.LOAISACH).Include(s => s.NHAXUATBAN).ToList();
                return View(dsSachs);
        }
    }
}