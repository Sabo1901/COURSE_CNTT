using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using PagedList;

namespace COURSE_CNTT.Areas.Admin.Controllers
{
    public class CTKHOASVsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CTKHOASVs
        public ActionResult Index(int? page, string search)
        {
            if (search == null)
            {
                IEnumerable<CTKHOASV> items = db.CTKHOASVs.Include(c => c.COURSE).Include(c => c.HOCPHAN).OrderBy(x => x.Hocki);
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
                IEnumerable<CTKHOASV> items = db.CTKHOASVs.Where(x => x.HOCPHAN.KhoaHoc.Contains(search.Trim().ToLower()) || x.COURSE.TENCOURSE.Contains(search.Trim().ToLower())).Include(c => c.COURSE).Include(c => c.HOCPHAN).OrderBy(x => x.Hocki);
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

        // GET: Admin/CTKHOASVs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTKHOASV cTKHOASV = db.CTKHOASVs.Find(id);
            if (cTKHOASV == null)
            {
                return HttpNotFound();
            }
            return View(cTKHOASV);
        }

        // GET: Admin/CTKHOASVs/Create
        public ActionResult Create()
        {
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE");
            ViewBag.MAHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc");
            return View();
        }

        // POST: Admin/CTKHOASVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKHOASV,MACOURSE,Tinchi,MaHP,MaHPHoctruoc,Hocki")] CTKHOASV cTKHOASV)
        {
            if (ModelState.IsValid)
            {
                db.CTKHOASVs.Add(cTKHOASV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", cTKHOASV.MACOURSE);
            ViewBag.MAHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc", cTKHOASV.MaHP);
            return View(cTKHOASV);
        }

        // GET: Admin/CTKHOASVs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTKHOASV cTKHOASV = db.CTKHOASVs.Find(id);
            if (cTKHOASV == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", cTKHOASV.MACOURSE);
            ViewBag.MAHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc", cTKHOASV.MaHP);

            //ViewBag.MACOURSE = new SelectList(db.COURSEs.ToList(), "MACOURSE", "TENCOURSE");
            //ViewBag.MAHP = new SelectList(db.HOCPHANs.ToList(), "MaHP", "KhoaHoc");
            return View(cTKHOASV);
        }

        // POST: Admin/CTKHOASVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKHOASV,MACOURSE,Tinchi,MaHP,MaHPHoctruoc,Hocki")] CTKHOASV cTKHOASV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTKHOASV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", cTKHOASV.MACOURSE);
            ViewBag.MAHP = new SelectList(db.HOCPHANs, "MaHP", "KhoaHoc", cTKHOASV.MaHP);
            return View(cTKHOASV);
        }

      
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.CTKHOASVs.Find(id);
            if (item != null)
            {
                db.CTKHOASVs.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
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
    }
}
