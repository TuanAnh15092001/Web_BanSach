using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_BanSach.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc
        public ActionResult _DanhMuc()
        {
            return PartialView();
        }
    }
}