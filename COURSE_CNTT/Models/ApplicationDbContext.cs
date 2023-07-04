using COURSE_CNTT.Models.IE;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace COURSE_CNTT.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CourseDBContextCNTT", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<THONGKE> THONGKEs { get; set; }
        public virtual DbSet<BLOG> BLOGs { get; set; }
        public virtual DbSet<HOCPHAN> HOCPHANs { get; set; }
        public virtual DbSet<COURSE> COURSEs { get; set; }
        public virtual DbSet<QLCOURSE> QLCOUSEs { get; set; }
        public virtual DbSet<CTQLCOURSE> CTQLCOURSEs { get; set; }
        public virtual DbSet<CTKHOASV> CTKHOASVs { get; set; }
        public virtual DbSet<VIDEO> VIDEOs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}