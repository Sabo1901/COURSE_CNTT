using COURSE_CNTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.repository
{
    public class CourseDao
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public CourseDao() {
            ApplicationDbContext daaa = new ApplicationDbContext();
        }
        public List<string> ListName(string keyword)
        {
            return db.COURSEs.Where(x => x.TENCOURSE.Contains(keyword)).Select(x => x.TENCOURSE).ToList();
        }
    }
}