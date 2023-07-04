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
    public class HOCPHANsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/HOCPHANs
        public ActionResult Index(int? page, string search)
        {
            if(search == null)
            {
                IEnumerable<HOCPHAN> items = db.HOCPHANs.OrderByDescending(x => x.KhoaHoc);
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
                IEnumerable<HOCPHAN> items = db.HOCPHANs.Where(x => x.KhoaHoc.Contains(search.Trim().ToLower())).OrderByDescending(x => x.MaHP);
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

        // GET: Admin/HOCPHANs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCPHAN hOCPHAN = db.HOCPHANs.Find(id);
            if (hOCPHAN == null)
            {
                return HttpNotFound();
            }
            return View(hOCPHAN);
        }

        // GET: Admin/HOCPHANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HOCPHANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHP,KhoaHoc,Sodo,trangthai")] HOCPHAN hOCPHAN)
        {
            if (ModelState.IsValid)
            {
                db.HOCPHANs.Add(hOCPHAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOCPHAN);
        }

        // GET: Admin/HOCPHANs/Edit/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCPHAN hOCPHAN = db.HOCPHANs.Find(id);
            if (hOCPHAN == null)
            {
                return HttpNotFound();
            }
            return View(hOCPHAN);
        }

        // POST: Admin/HOCPHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHP,KhoaHoc,Sodo,trangthai")] HOCPHAN hOCPHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOCPHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOCPHAN);
        }

        // GET: Admin/HOCPHANs/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.HOCPHANs.Find(id);
            if (item != null)
            {
                db.HOCPHANs.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.HOCPHANs.Find(id);
            if (item != null)
            {
                item.trangthai = !item.trangthai;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.trangthai });
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
            file.SaveAs(Server.MapPath("~/css/img/" + file.FileName));
            return "/css/img/" + file.FileName;
        }
    }
}
