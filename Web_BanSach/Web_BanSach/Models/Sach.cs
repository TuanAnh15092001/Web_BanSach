using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BanSach.Models
{
    public class Sach
    {
        public virtual LOAISACH LOAISACH { get; set; }
        public virtual NHAXUATBAN NHAXUATBAN { get; set; }

    }

}