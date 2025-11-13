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

        public ActionResult ChiTietSach(int masach)
        {
            var sachs = sach.BANGSACHes.FirstOrDefault(s => s.MASACH == masach);

            //View sách liên quan
            List<BANGSACH> lienquan = sach.BANGSACHes.Where(i => i.MALOAI == sachs.MALOAI && i.MASACH != masach).ToList();

            if (lienquan == null || !lienquan.Any())
            {
                ViewBag.Message = "Không có sách cùng loại.";
            }
            ViewBag.LQ = lienquan;
            //////View cùng nhà sản xuất
            //List<BANGSACH> nxb = sach.BANGSACHes.Where(i => i.NHAXUATBAN == sachs.NHAXUATBAN && i.MASACH != masach).ToList();
            //ViewBag.NXB = nxb;
            return View(sachs);
        }
    }
}