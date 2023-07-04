using COURSE_CNTT.Models;
using COURSE_CNTT.Models.IE;
using COURSE_CNTT.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            var user = GetUserByUserName();
            //if (user == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_id = db.Users.First(m => m.Id == id);
            var E_hoten = collection["FULLNAME"];
            var E_sdt = collection["PHONE"];
            var E_email = collection["Email"];
            var E_gioitinh = collection["GIOITINH"];
            var E_ngaysinh = String.Format("{0:yyyy/MM/dd}", collection["NGAYSINH"]);
            E_id.Id = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_id.FullName = E_hoten;
                E_id.Phone = E_sdt;
                if (E_ngaysinh != null)
                {
                    E_id.NGAYSINH = DateTime.Parse(E_ngaysinh);
                }
                else
                {
                    E_id.NGAYSINH = DateTime.Now;
                }  
                    

                UpdateModel(E_id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // return this.Edit(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            var bLOG = db.BLOGs.Where(x => x.Id== id).ToList();
            ViewBag.list = bLOG;
            ViewBag.id = user.Id;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
     
       
    }
}