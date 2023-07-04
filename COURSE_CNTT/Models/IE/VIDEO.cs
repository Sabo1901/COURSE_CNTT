using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("VIDEO")]
    public partial class VIDEO
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MAVIDEO { get; set; }

        public string LINKVIDEO { get; set; }
        [StringLength(30)]
        public string MACOURSE { get; set; }
        [StringLength(100)]
        public string TENBAI { get; set; }
        [StringLength(100)]
        public string TENCHUONG { get; set; }
        [StringLength(10)]
        public string THOILUONGVIDEO { get; set; }
        public bool hienthi { get; set; }

        public virtual COURSE COURSE { get; set; }
    }
}