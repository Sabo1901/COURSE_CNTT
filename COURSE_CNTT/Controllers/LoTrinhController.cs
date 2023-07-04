using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Controllers
{
    
    public class LoTrinhController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: LoTrinh
        public ActionResult Index()
        {
            if (Session["MaGH"] == null)
                return RedirectToAction("Login", "Account");
            var user = GetUserByUserName();
            var hOCPHAN = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Equals(user.khoa)).OrderBy(x => x.Hocki).ToList();
            var bai = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Equals(user.khoa)).Count();
            var list = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Equals(user.khoa)).ToList();
            var sodo = db.HOCPHANs.Where(x => x.KhoaHoc.Equals(user.khoa)).FirstOrDefault().Sodo;
            var khoa = db.HOCPHANs.Where(x => x.KhoaHoc.Equals(user.khoa)).FirstOrDefault().KhoaHoc;
            int count = 0;
            foreach (var item in list)
            {
                if (item.Tinchi != 0)
                {
                    count = count + item.Tinchi;
                }
            }
            ViewBag.sodo = sodo;
            ViewBag.count = count;
            ViewBag.hocphan = bai;
            ViewBag.list = list;      
            ViewBag.khoa = khoa;
                return View(hOCPHAN);
        }

        public ActionResult SearchMaHP(string mahp)
        {
            var user = GetUserByUserName();
            var list = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Equals(user.khoa) && x.MaHPHoctruoc.Equals(mahp)).ToList();
            ViewBag.list = list;
            ViewBag.mahp = mahp;
            
                var items = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Equals(user.khoa) && x.MACOURSE.Equals(mahp)).ToList();
                return PartialView("_SearchMaHP", items);
            


        }
        public ApplicationUser GetUserByUserName()
        {
            var user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            return user;
        }

    }
}