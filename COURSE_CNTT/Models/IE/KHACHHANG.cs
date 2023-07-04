using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        //public KHACHHANG()
        //{
        //    QLCOURSEs = new HashSet<QLCOURSE>();
        //}

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAKH { get; set; }

        [StringLength(50)]
        public string TENKH { get; set; }

        public bool? GIOITINH { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(30)]
        public string EMAIL { get; set; }

        [StringLength(30)]
        public string TENTK { get; set; }

        [StringLength(16)]
        public string MATKHAU { get; set; }

        [StringLength(100)]
        public string ANH { get; set; }

        //public virtual ICollection<QLCOURSE> QLCOURSEs { get; set; }

    }
}