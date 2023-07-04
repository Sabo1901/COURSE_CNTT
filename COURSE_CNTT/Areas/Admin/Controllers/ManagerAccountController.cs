using COURSE_CNTT.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using COURSE_CNTT.Models.IE;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;

namespace COURSE_CNTT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class ManagerAccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ManagerAccountController()
        {
        }

        public ManagerAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Account
        

        public ActionResult Index(int? page, string search)
        {
           
            if (search == null)
            {
                IEnumerable<ApplicationUser> items = db.Users.Where(x => x.Role.Equals("User")).OrderByDescending(x => x.Id);
                var pageSize = 10;

                if (page == null)
                {
                    page = 1;
                }
                var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                items = items.ToPagedList(pageIndex, pageSize);
               // items.Where(x => x.NGAYSINH == DateTime.Now);
                ViewBag.PageSize = pageSize;
                ViewBag.Page = page;
                return View(items);
            }
            else
            {
                IEnumerable<ApplicationUser> items = db.Users.Where(x => x.Role.Equals("User") && x.Email.Contains(search) || x.FullName.Contains(search) || x.khoa.Contains(search)).OrderByDescending(x => x.Id);
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
        [Authorize(Roles = "Admin")]
        public ActionResult ListAdmin(int? pages, string search)
        {
            if (search == null)
            {
                IEnumerable<ApplicationUser> items = db.Users.Where(x => x.Role != "User").OrderBy(x => x.Role);
                var pageSize = 10;

                if (pages == null)
                {
                    pages = 1;
                }
                var pageIndex = pages.HasValue ? Convert.ToInt32(pages) : 1;
                items = items.ToPagedList(pageIndex, pageSize);
                ViewBag.PageSize = pageSize;
                ViewBag.Page = pages;
                return View(items);
            }
            else
            {
                IEnumerable<ApplicationUser> items = db.Users.Where(x => x.Role != "User" && x.Email.Contains(search) || x.FullName.Contains(search) || x.Role.Contains(search)).OrderByDescending(x => x.Role);
                var pageSize = 10;

                if (pages == null)
                {
                    pages = 1;
                }
                var pageIndex = pages.HasValue ? Convert.ToInt32(pages) : 1;
                items = items.ToPagedList(pageIndex, pageSize);
                ViewBag.PageSize = pageSize;
                ViewBag.Page = pages;
                return View(items);
            }


        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    GetIdUserByName(model.Email);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Bạn nhập sai tên đăng nhập hoặc mật khẩu");
                    return View(model);
            }
        }

        public void GetIdUserByName(string name)
        {
            ApplicationUser user = db.Users.Single(u => u.Email.Equals(name));
            // MaKH = user.Id;

            //   int MaGH = db.QLCOUSEs.Where(x => x.Id.Equals(user.Id)).FirstOrDefault().MAGH;

            // lưu id giỏ hàng vào Session
            Session["MaGH"] = db.QLCOUSEs.Where(x => x.Id.Equals(user.Id)).FirstOrDefault().MAGH;
        }


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Phone = model.Phone,
                    Anh = model.Anh,
                    khoa = null,
                    hienthi = true,
                    Role= model.Role,
                    NGAYSINH = model.Ngaysinh,
            };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    QLCOURSE qLCOURSE = new QLCOURSE();
                    qLCOURSE.Id = user.Id;
                    qLCOURSE.TONGVD = 0;
                    Create(qLCOURSE);

                    return RedirectToAction("ListAdmin", "ManagerAccount");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //tạo giỏ hàng
        public void Create([Bind(Include = "MAGH, Id,TONGVD")] QLCOURSE gh)
        {
            //    List<GIOHANG> list = new List<GIOHANG>();
            if (ModelState.IsValid)
            {
                db.QLCOUSEs.AddOrUpdate(gh);
                //model.GIOHANGs.InsertOnSubmit(gh);
                //model.GIOHANGs.InsertOnSubmit(gh);
                //model.SubmitChanges();
                db.SaveChanges();

            }

        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser uSER = db.Users.Find(id);
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(uSER);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ApplicationUser uSER)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser user =  db.Users.Find(uSER.Id);
                user.Role = uSER.Role;
                user.FullName = uSER.FullName;
                user.Anh = uSER.Anh;
                user.Email = uSER.Email;
                user.Phone = uSER.Phone;
                user.NGAYSINH= uSER.NGAYSINH;
                user.hienthi = uSER.hienthi;
                
               db.Users.AddOrUpdate(user);
                db.SaveChanges();
                return RedirectToAction("ListAdmin");
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(uSER);
        }

        public ActionResult Profile()
        {
            var user = db.Users.
                Where(u => u.UserName.Equals(User.Identity.Name)).
                FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(string id, FormCollection collection)
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
                //E_id. = E_gioitinh;
                if (E_ngaysinh != null)
                    E_id.NGAYSINH = DateTime.Parse(E_ngaysinh);

                UpdateModel(E_id);
                db.SaveChanges();
                return RedirectToAction("profile");
            }
            // return this.Edit(id);
            return RedirectToAction("profile");
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.Users.Find(id);
            var GH = db.QLCOUSEs.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (item != null)
            {
                db.QLCOUSEs.Remove(GH);
                db.Users.Remove(item);
               
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult IsActive(string id)
        {
            var item = db.Users.Find(id);
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