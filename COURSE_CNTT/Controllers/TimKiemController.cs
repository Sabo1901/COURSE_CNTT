using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Controllers
{
    public class TimKiemController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTimKiem(string keyword)
        {
            var list = db.COURSEs.Where(x => x.TENCOURSE.Contains(keyword));
            var listblog = db.BLOGs.Where(x => x.TIEUDE.Contains(keyword));
            return View(list.OrderBy(x => x.TENCOURSE));
        }
        [HttpPost]

        public ActionResult LayTuKhoaTimKiem(string keyword)
        {

            return RedirectToAction("KQTimKiem", new {@keyword = keyword});
        }

       
    }
}