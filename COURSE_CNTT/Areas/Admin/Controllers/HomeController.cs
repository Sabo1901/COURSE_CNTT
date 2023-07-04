using COURSE_CNTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            Session["acc"] = db.Users.FirstOrDefault(c => c.UserName.Equals(User.Identity.Name));
            var taikhoan = db.Users.Count();
            var khoahoc = db.COURSEs.Count();
            var baiviet = db.BLOGs.Count();
            var video = db.VIDEOs.Count();
            ViewBag.taikhoan = taikhoan;
            ViewBag.khoahoc = khoahoc;
            ViewBag.baiviet = baiviet;
            ViewBag.video = video;
            return View();
        }
    }
}