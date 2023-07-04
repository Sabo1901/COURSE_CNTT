using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Models.IE
{
    [Table("COURSE")]
    public partial class COURSE
    {
        public COURSE()
        {
            VIDEOs = new HashSet<VIDEO>();

        }

        [Key]
        [StringLength(30)]
     
        public string MACOURSE { get; set; }
        [Required(ErrorMessage = "Tên khóa học không để trống")]
        
        [StringLength(50)]
        public string TENCOURSE { get; set; }
     

        public int ViewCount { get; set; }
      
        [AllowHtml]
        public string Mota { get; set; }
        public string Chitiet { get; set; }
        [AllowHtml]
        public string Yeucau { get; set; }
        public string video { get; set; }
        public bool hienthi { get; set; }
        [StringLength(50)]
        public string HINH { get; set; }
        [StringLength(30)]

        

        //public virtual HOCPHAN HOCPHAN { get; set; }
        public virtual ICollection<CTKHOASV> CTKHOASV { get; set; }
        public virtual ICollection<VIDEO> VIDEOs { get; set; }
    }
}