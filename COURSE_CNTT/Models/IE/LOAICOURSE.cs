using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COURSE_CNTT.Models.IE
{
    [Table("LOAICOURSE")]
    public class LOAICOURSE
    {
        public LOAICOURSE()
        {
            COURSEs = new HashSet<COURSE>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MALOAI { get; set; }

        [StringLength(20)]
        public string TENLOAI { get; set; }
        public string ANH { get; set; }
        public virtual ICollection<COURSE> COURSEs { get; set; }
    }
}