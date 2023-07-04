using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace COURSE_CNTT.Areas.Admin.Controllers
{
    public class ManagerBLOGController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ManagerBLOG
        public ActionResult Index(int? page)
        {

            IEnumerable<BLOG> items = db.BLOGs.OrderByDescending(x => x.MABLOG);
            var pageSize = 10;

            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);

            
        }


        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.BLOGs.Find(id);
            if (item != null)
            {
                item.trangthai = !item.trangthai;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.trangthai });
            }

            return Json(new { success = false });
        }


    }
}