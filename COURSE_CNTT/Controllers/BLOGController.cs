using COURSE_CNTT.Models.IE;
using COURSE_CNTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Net;
using System.Xml.Linq;
using System.Data.Entity;
using PagedList;

namespace COURSE_CNTT.Controllers
{
    public class BLOGController : Controller
    {
        // GET: BLOG
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BLOG
        public ActionResult Index(int? page, int? ma)
        {
            IEnumerable<BLOG> items = db.BLOGs.Where(x => x.trangthai).OrderByDescending(x => x.MABLOG);
            var list = db.BLOGs.Where(x => x.MABLOG == ma).ToList();

            var user = GetUserByUserName();
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOGs.Find(id);
            TimeSpan ngay = DateTime.Now - bLOG.NGAYTAO;
            ViewBag.ngay = ngay;
            if (bLOG != null)
            {
                db.BLOGs.Attach(bLOG);
                bLOG.ViewCount = bLOG.ViewCount + 1;
                db.Entry(bLOG).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            return View(bLOG);
        }
        public ActionResult Create()
        {
            if (Session["MaGH"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MABLOG,TIEUDE,chitiet,ChitietTieuDe,Hinh,Id")] BLOG bLOG)
        {
            var user = GetUserByUserName();
            if (ModelState.IsValid)
            {
                bLOG.Id = user.Id;
                bLOG.NGAYTAO = DateTime.Now;
                bLOG.trangthai = true;
                db.BLOGs.Add(bLOG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bLOG);
        }

        public ActionResult Edit(int? id)
        {
            var user = GetUserByUserName();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOGs.Find(id);
            bLOG.Id = user.Id;

            if (bLOG == null)
            {
                return HttpNotFound();
            }
            return View(bLOG);
        }

        // POST: Admin/COURSEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MABLOG,TIEUDE,chitiet,ChitietTieuDe,Hinh,Id")] BLOG bLOG)
        {
            var user = GetUserByUserName();
            if (ModelState.IsValid)
            {
                bLOG.Id = user.Id;
                bLOG.NGAYTAO = DateTime.Now;
                bLOG.trangthai = true;
                db.Entry(bLOG).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bLOG);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = GetUserByUserName();
            var item = db.BLOGs.Find(id);
            if (item != null)
            {
                item.Id = user.Id;
                db.BLOGs.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        public ActionResult BlogUser(string id)
        {
            var user = GetUserByUserName();
            var list = db.BLOGs.Where(x => x.Id== user.Id).ToList();
            return View(list);
        }


        public ApplicationUser GetUserByUserName()
        {
            var user = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            return user;
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
        public ActionResult BlogView(int? id)
        {
            var bai = db.BLOGs.Find(id);
            var ketqua = db.BLOGs.Where(x => x.ViewCount <= int.MaxValue && x.trangthai.Equals(true)).OrderByDescending(x => x.ViewCount).Take(8).ToList();
            return PartialView(ketqua);


        }

    }
    }