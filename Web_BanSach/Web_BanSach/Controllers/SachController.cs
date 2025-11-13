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

            //View cùng nhà sản xuất
            List<BANGSACH> nxb = sach.BANGSACHes.Where(i => i.MANXB == sachs.MANXB && i.MASACH != masach).ToList();

            if (nxb== null || !nxb.Any())
            {
                ViewBag.Message = "Không có bản thảo chung của nhà xuất bản.";
            }
            ViewBag.NXB = nxb;
            return View(sachs);
        }


        /// <summary>
        /// lọc sản phẩm theo thể loại hoặc nxb
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">1: lọc theo thể loại, 2: lọc theo tên nhà xuất bản</param>
        /// <returns></returns>
        public ActionResult LocSP(int id, int type)
        {
            List<BANGSACH> loc = new List<BANGSACH>();

            if (type == 1) loc = sach.BANGSACHes.Where(i => i.MALOAI== id).ToList();
            else if (type == 2) loc = sach.BANGSACHes.Where(i => i.MANXB == id).ToList();
            return View("Index", loc);
        }

        public ActionResult TimKiemSPNangCao(string kw, int? chude, string[] gia)
        {
            List<BANGSACH> lstsachs = new List<BANGSACH>();
            //Kiểm tra nếu chuỗi không null thì tìm theo từ khóa
            if (!String.IsNullOrEmpty(kw))
            {
                //tìm kiếm theo từ khóa khi tên sách bằng mới chuỗi Keyword(kw) khi nhập vào
                lstsachs = sach.BANGSACHes.Where(s => s.TENSACH.Contains(kw.ToLower())).ToList();
            }
            if (chude != null)
            {
                //tìm kiếm theo chủ đề (Loại sách) nếu thể loại tích đúng thì hiển thị lst
                lstsachs = sach.BANGSACHes.Where(s => s.MALOAI == chude).ToList();
            }
            if (gia != null && gia.Length > 0)
            {
                var kq = new List<BANGSACH>();
                foreach (var g in gia)
                {
                    if (g.Contains('-'))
                    {
                        var arr = g.Split('-');
                        int min = int.Parse(arr[0]);
                        int max = int.Parse(arr[1]);
                        kq.AddRange(lstsachs.Where(s => s.GIABAN >= min && s.GIABAN <= max));
                    }
                    if (g.Contains('>'))
                    {
                        int min = int.Parse(g.Replace(">", ""));
                        kq.AddRange(lstsachs.Where(s => s.GIABAN > min));
                    }
                }
                lstsachs = kq.Distinct().ToList();
            }

            return View("Index", lstsachs);
        }


    }
}