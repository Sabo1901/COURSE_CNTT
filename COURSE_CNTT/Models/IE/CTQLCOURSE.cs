using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("CTQLCOURSE")]
    public partial class CTQLCOURSE
    {
        [StringLength(50)]
        public string TENCOURSE { get; set; }
        public string Hinh { get; set; }
        public string chitiet { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string MACOURSE { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MAGH { get; set; }

        public virtual COURSE COURSE { get; set; }

        public virtual QLCOURSE QLCOURSE { get; set; }
    }
}