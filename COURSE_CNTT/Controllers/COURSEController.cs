using COURSE_CNTT.Models.IE;
using COURSE_CNTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COURSE_CNTT.repository;
using System.Data.Entity;

namespace COURSE_CNTT.Controllers
{
    public class COURSEController : Controller
    {
        // GET: COURSE
        private ApplicationDbContext db = new ApplicationDbContext();
        public repository.QLCOURSEs Repository = new repository.QLCOURSEs();

        // GET: COURSE
        public ActionResult Index(string id, string tenchuong)
        {
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            //tenchuong = "1. Giới thiệu";
            var bai = cOURSE.VIDEOs.Where(x => x.hienthi).Count();
            var list = cOURSE.VIDEOs.ToList();
            int count = 0 ;
            foreach (var item in list)
            {
                if (item.TENCHUONG != null)
                {
                    if (item.MACOURSE == id)
                    {
                        if (tenchuong != item.TENCHUONG)
                            count++;
                    }
                    tenchuong = item.TENCHUONG;
                }
            }
            ViewBag.count = count;
            ViewBag.video = bai;
            if (cOURSE != null)
            {
                db.COURSEs.Attach(cOURSE);
                cOURSE.ViewCount = cOURSE.ViewCount + 1;
                db.Entry(cOURSE).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
          
            return View(cOURSE);
        }
        public ActionResult Khoahoc(string search)
        {
           
            var items = db.COURSEs.Where(x => x.hienthi).ToList();
            return View(items);
        }

        public ActionResult Details(string id)
        {      
            var sum = db.VIDEOs.Where(x => x.hienthi && x.MACOURSE == id).ToList();
            return View(sum);
        }
        public ActionResult DetailsCourse(string id, string tenchuong, string tenbai)
        {
           
            var sum = db.VIDEOs.Where(x => x.hienthi && x.MACOURSE == id).ToList();
            int count = 0;
            var list = db.VIDEOs.ToList();
            var bai = db.VIDEOs.Where(x => x.TENCHUONG == tenchuong).ToList();
            foreach (var item in list)
            {
                if (item.TENCHUONG != null)
                {
                    if (item.MACOURSE == id)
                    {
                        if(tenchuong == item.TENCHUONG)
                        {
                            if (tenbai != item.TENBAI)
                            {
                                count++;
                            }
                        }
                        else
                        {
                            count = 0;
                            if (tenbai != item.TENBAI)
                            {
                                count++;
                            }
                        }
                    }
                    tenchuong = item.TENCHUONG;
                }
            }
            ViewBag.count = count;
            //  var vIDEO = db.VIDEOs.ToList();
            return PartialView("_DetailsCourse", sum);
        }

        public ActionResult ListMenu(string id)
        {

            var items = db.COURSEs.Where(x => x.ViewCount <= int.MaxValue && x.hienthi.Equals(true)).OrderByDescending(x => x.ViewCount).Take(5).ToList();
            return PartialView(items);
        }

    }
}