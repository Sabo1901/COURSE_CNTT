using COURSE_CNTT.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("HOCPHAN")]
    public partial class HOCPHAN
    {

        public HOCPHAN()
        {
            CTKHOASVs = new HashSet<CTKHOASV>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MaHP { get; set; }
        [StringLength(20)]
        public string KhoaHoc { get; set; }

        [StringLength(20)]
        public string Sodo { get; set; }
        public bool trangthai { get; set; }

       
        public virtual ICollection<CTKHOASV> CTKHOASVs { get; set; }

    }
}