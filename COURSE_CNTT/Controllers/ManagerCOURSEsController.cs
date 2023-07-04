using COURSE_CNTT.Models.IE;
using COURSE_CNTT.Models;
using COURSE_CNTT.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace COURSE_CNTT.Controllers
{
    public class ManagerCOURSEsController : Controller
    {
        // GET: ManagerCOURSEs
        private ApplicationDbContext db = new ApplicationDbContext();
        public repository.QLCOURSEs qLCOURSEs = new repository.QLCOURSEs();
        // GET: ManagerCOURSEs
        public ActionResult Index()
        {
            if (Session["MaGH"] == null)
                return RedirectToAction("Login", "Account");
            List<CTQLCOURSE> list = GetListProductInCard((int)Session["MaGH"]);
            ViewBag.list = list;
            return View(list);
        }

        public List<CTQLCOURSE> GetListProductInCard(int MaGH)
        {
            List<CTQLCOURSE> list = new List<CTQLCOURSE>();
            list = db.CTQLCOURSEs.Where(x => x.QLCOURSE.MAGH.Equals(MaGH)).ToList();
            if (list == null)
                list = new List<CTQLCOURSE>();
            return list;
        }

        public List<CTQLCOURSE> dsCOURSEs(int id)
        {
            return db.CTQLCOURSEs.Where(x => x.MAGH == id).ToList();
        }

        [HttpPost]
        public ActionResult AddToCourse(string id, string strURL)
        {
            if (Session["MaGH"] == null)
            {
                Session["success"] = "false";
                return View();
            }
            Session["success"] = "success";
            List<CTQLCOURSE> list = GetListProductInCard((int)Session["MaGH"]);
            CTQLCOURSE sp = list.Find(b => b.MACOURSE.Equals(id));
            if (sp == null)
            {
                sp = new CTQLCOURSE();
                COURSE cOURSE = db.COURSEs.Single(n => n.MACOURSE == id);
                sp.MACOURSE = id;
                sp.Hinh = cOURSE.HINH;
                sp.TENCOURSE = cOURSE.TENCOURSE;
                sp.chitiet = cOURSE.Chitiet;
                sp.MAGH = (int)Session["MaGH"];

                list.Add(sp);
                db.CTQLCOURSEs.AddOrUpdate(sp);

            }
            db.SaveChanges();
            return Redirect(strURL);
        }
    }
}