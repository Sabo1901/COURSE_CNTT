using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("LOTRINH")]
    public partial class CTKHOASV
    {

        //public CTKHOASV()
        //{
        //    COURSEs = new HashSet<COURSE>();
        //    HOCPHANs = new HashSet<HOCPHAN>();

        //}

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAKHOASV { get; set; }
        public int Hocki { get; set; }
        public int Tinchi { get; set; }
        public string MaHPHoctruoc { get; set; }

        //[Key]
        //[Column(Order = 0)]
        [StringLength(30)]
        public string MACOURSE { get; set; }

        //[Key]
        //[Column(Order = 1)]
        public int MaHP { get; set; }

        //public virtual ICollection<COURSE> COURSEs { get; set; }
        //public virtual ICollection<HOCPHAN> HOCPHANs { get; set; }
        public virtual COURSE COURSE { get; set; }
        public virtual HOCPHAN HOCPHAN { get; set; }

    }
}