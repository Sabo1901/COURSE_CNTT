using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using PagedList;

namespace COURSE_CNTT.Areas.Admin.Controllers
{
    public class COURSEsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/COURSEs
        public ActionResult Index(int? page, string search)
        {
            if(search == null)
            {
                IEnumerable<COURSE> items = db.COURSEs.OrderByDescending(x => x.MACOURSE);
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
            else
            {
                IEnumerable<COURSE> items = db.COURSEs.Where(x => x.TENCOURSE.Contains(search.Trim().ToLower()) || x.MACOURSE.Contains(search.Trim().ToLower())).OrderByDescending(x => x.MACOURSE);
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
            
        }

        // GET: Admin/COURSEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // GET: Admin/COURSEs/Create
        public ActionResult Create()
        {
            //ViewBag.MaHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc");
            return View();
        }

        // POST: Admin/COURSEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACOURSE,TENCOURSE,ViewCount,Mota,Chitiet,Yeucau,video,hienthi,HINH")] COURSE cOURSE)
        {
            if (ModelState.IsValid)
            {
                db.COURSEs.Add(cOURSE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MaHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc", cOURSE.MaHP);
            return View(cOURSE);
        }

        // GET: Admin/COURSEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // POST: Admin/COURSEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACOURSE,TENCOURSE,ViewCount,Mota,Chitiet,Yeucau,video,hienthi,HINH")] COURSE cOURSE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOURSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOURSE);
        }

       
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.COURSEs.Find(id);
            if (item != null)
            {
                db.COURSEs.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(string id)
        {
            var item = db.COURSEs.Find(id);
            if (item != null)
            {
                item.hienthi = !item.hienthi;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.hienthi });
            }

            return Json(new { success = false });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

    }
}
