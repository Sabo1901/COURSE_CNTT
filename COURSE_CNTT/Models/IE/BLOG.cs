using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COURSE_CNTT.Models.IE
{
    [Table("BLOG")]
    public partial class BLOG
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MABLOG { get; set; }
        public string TIEUDE { get; set; }

        [AllowHtml]
        public string chitiet { get; set; }
        public string ChitietTieuDe { get; set; }
        public string Hinh { get; set; }
        public DateTime NGAYTAO { get; set; }
        public bool trangthai { get; set; }
        public int ViewCount { get; set; }
        public string Id { get; set; }
        public virtual ApplicationUser USER { get; set; }
        public virtual ICollection<KHACHHANG> KHACHHANGs { get; set; }
    }
}