using COURSE_CNTT.Models.IE;
using COURSE_CNTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace COURSE_CNTT.repository
{
    public class QLCOURSEs
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static List<CTQLCOURSE> dsCOURSEs(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            return db.CTQLCOURSEs.Where(x => x.MAGH == id).ToList();
        }

        public static List<CTQLCOURSE> dsCOURSEsUser(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            return db.CTQLCOURSEs.Where(x => x.QLCOURSE.Id == id).ToList();
        }
        public static List<BLOG> dsBlog(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return db.BLOGs.Where(x => x.Id == id).ToList();
        }
        public string GetUserByUserName(string name)
        {
            ApplicationUser user = db.Users.Single(u => u.Email.Equals(name));
            return user.Id;
        }
        public ApplicationUser GetUserByID(string id)
        {
            ApplicationUser user = db.Users.Single(u => u.Id.Equals(id));
            return user;
        }
    }
}