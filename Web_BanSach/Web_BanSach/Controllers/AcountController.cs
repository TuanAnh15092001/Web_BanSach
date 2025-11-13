using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web_BanSach.Controllers
{
    public class AcountController : Controller
    {
        QL_SACHEntities ql = new QL_SACHEntities();
        public ActionResult Login()
        {
            ViewBag.Url = !String.IsNullOrEmpty(Request.UrlReferrer.ToString()) ? Request.UrlReferrer.ToString() : "/";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XuLyFormDN(FormCollection f, string duongdan)
        {
            //khai báo 2 biến
            string email = f["email"];
            string pass = f["password"];

            // xử lý tìm kiếm
            var kh = ql.KHACHHANGs.FirstOrDefault(k => k.EMAIL == email && k.MATKHAU == pass);
            if (kh != null)
            {
                FormsAuthentication.SetAuthCookie(kh.TENKHACHHANG, true);
                Session["TENKHACHHANG"] = kh.TENKHACHHANG;
                Session["MAKHACHHANG"] = kh.MAKHACHHANG;
                // return về đường dẫn mà lúc nãy ấn vào, ví dụ: ấn vào view chi tiết, khi ấn đăng nhập vào lại vẫn là viewChitiet
                return Redirect(duongdan);

            }
            else
            {
                return View("Index");
            }
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Sach");
            //có thể dùng cách dưới
            //return Redirect("/");
        }

        // GET: Acount

        //////////


    }
}