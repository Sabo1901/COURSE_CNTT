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
    public class VIDEOsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/VIDEOs
        public ActionResult Index(int? page,string search)
        {
            if(search == null)
            {
                IEnumerable<VIDEO> items = db.VIDEOs.Include(v => v.COURSE).OrderByDescending(x => x.MAVIDEO);

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
                IEnumerable<VIDEO> items = db.VIDEOs.Include(v => v.COURSE).Where(x => x.TENBAI.Contains(search.Trim().ToLower()) || x.TENCHUONG.Contains(search.Trim().ToLower())).OrderByDescending(x => x.MAVIDEO);

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
            


            //var vIDEOs = db.VIDEOs.Include(v => v.COURSE);
            //return View(vIDEOs.OrderByDescending(x => x.MAVIDEO).ToList());
        }

        // GET: Admin/VIDEOs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIDEO vIDEO = db.VIDEOs.Find(id);
            if (vIDEO == null)
            {
                return HttpNotFound();
            }
            return View(vIDEO);
        }

        // GET: Admin/VIDEOs/Create
        public ActionResult Create()
        {
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE");
            return View();
        }

        // POST: Admin/VIDEOs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAVIDEO,LINKVIDEO,MACOURSE,TENBAI,TENCHUONG,THOILUONGVIDEO,hienthi")] VIDEO vIDEO)
        {
            if (ModelState.IsValid)
            {
                db.VIDEOs.Add(vIDEO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", vIDEO.MACOURSE);
            return View(vIDEO);
        }

        // GET: Admin/VIDEOs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIDEO vIDEO = db.VIDEOs.Find(id);
            if (vIDEO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", vIDEO.MACOURSE);
            return View(vIDEO);
        }

        // POST: Admin/VIDEOs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAVIDEO,LINKVIDEO,MACOURSE,TENBAI,TENCHUONG,THOILUONGVIDEO,hienthi")] VIDEO vIDEO)
        {
            if (ModelState.IsValid)
            {
                vIDEO.hienthi = true;
                db.Entry(vIDEO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MACOURSE = new SelectList(db.COURSEs, "MACOURSE", "TENCOURSE", vIDEO.MACOURSE);
            return View(vIDEO);
        }

        // GET: Admin/VIDEOs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.VIDEOs.Find(id);
            if (item != null)
            {
                db.VIDEOs.Remove(item);
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
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.VIDEOs.Find(id);
            if (item != null)
            {
                item.hienthi = !item.hienthi;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.hienthi });
            }

            return Json(new { success = false });
        }

    }
}
