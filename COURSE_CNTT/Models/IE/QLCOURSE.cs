using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("QLCOURSE")]
    public partial class QLCOURSE
    {
        public QLCOURSE()
        {
            CTQLCOURSEs = new HashSet<CTQLCOURSE>();
        }

        public int TONGVD { get; set; }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAGH { get; set; }

        public string Id { get; set; }

        public virtual ICollection<CTQLCOURSE> CTQLCOURSEs { get; set; }
        public virtual ApplicationUser KHACHHANGs { get; set; }
    }
}