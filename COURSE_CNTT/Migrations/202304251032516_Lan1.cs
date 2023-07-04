namespace COURSE_CNTT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lan1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BLOG",
                c => new
                    {
                        MABLOG = c.Int(nullable: false, identity: true),
                        TIEUDE = c.String(),
                        chitiet = c.String(),
                        ChitietTieuDe = c.String(),
                        Hinh = c.String(),
                        NGAYTAO = c.DateTime(nullable: true),
                        trangthai = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MABLOG)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
               "dbo.HOCPHAN",
               c => new
               {
                   MaHP = c.Int(nullable: false, identity: true),
                   KhoaHoc = c.String(maxLength: 20),
                   Sodo = c.String(maxLength: 20),
                   trangthai = c.Boolean(nullable: false),

               })
               .PrimaryKey(t => t.MaHP);

            //CreateTable(
            //    "dbo.KHACHHANG",
            //    c => new
            //        {
            //            MAKH = c.Int(nullable: false, identity: true),
            //            TENKH = c.String(maxLength: 50),
            //            GIOITINH = c.Boolean(),
            //            SDT = c.String(maxLength: 10),
            //            EMAIL = c.String(maxLength: 30),
            //            TENTK = c.String(maxLength: 30),
            //            MATKHAU = c.String(maxLength: 16),
            //            ANH = c.String(maxLength: 100),
            //            BLOG_MABLOG = c.Int(),
            //        })
            //    .PrimaryKey(t => t.MAKH)
            //    .ForeignKey("dbo.BLOG", t => t.BLOG_MABLOG)
            //    .Index(t => t.BLOG_MABLOG);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Phone = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        ANH = c.String(maxLength: 100),
                        ANHBIA = c.String(maxLength: 100),
                        NGAYSINH = c.DateTime(nullable: false),
                        GIOITINH = c.String(maxLength: 5, nullable: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        hienthi = c.Boolean(nullable: false),
                        khoa = c.String(),
                        Role = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.COURSE",
                c => new
                {
                    MACOURSE = c.String(nullable: false, maxLength: 30),
                    TENCOURSE = c.String(nullable: false, maxLength: 50),
                    ViewCount = c.Int(nullable: false),
                    Mota = c.String(),
                    Chitiet = c.String(),
                    Yeucau = c.String(),
                    video = c.String(),
                    hienthi = c.Boolean(nullable: false),
                    HINH = c.String(maxLength: 50),                 
                    //MALOAI = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.MACOURSE);
                //.ForeignKey("dbo.LOAICOURSE", t => t.MALOAI, cascadeDelete: true)


            //CreateTable(
            //    "dbo.LOAICOURSE",
            //    c => new
            //        {
            //            MALOAI = c.Int(nullable: false, identity: true),
            //            TENLOAI = c.String(maxLength: 20),
            //            ANH = c.String(),
            //        })
            //    .PrimaryKey(t => t.MALOAI);
            
            CreateTable(
                "dbo.VIDEO",
                c => new
                    {
                        MAVIDEO = c.Int(nullable: false, identity: true),
                        LINKVIDEO = c.String(),
                        MACOURSE = c.String(nullable: false, maxLength: 30),
                        TENBAI = c.String(maxLength: 100),
                        TENCHUONG = c.String(maxLength: 100),
                        THOILUONGVIDEO = c.String(maxLength: 10),
                        hienthi = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.MAVIDEO)
                .ForeignKey("dbo.COURSE", t => t.MACOURSE, cascadeDelete: true)
                .Index(t => t.MACOURSE);

            CreateTable(
              "dbo.LOTRINH",
              c => new
              {
                  MAKHOASV = c.Int(nullable: false, identity: true),
                  Hocki = c.Int(),
                  MaHPHoctruoc = c.String(maxLength: 30),
                  Tinchi = c.Int(),
                  MACOURSE = c.String(nullable: false, maxLength: 30),
                  MaHP = c.Int(nullable: false),
              })
              .PrimaryKey(t => t.MAKHOASV )
              .ForeignKey("dbo.COURSE", t => t.MACOURSE, cascadeDelete: true)    
              .ForeignKey("dbo.HOCPHAN", t => t.MaHP, cascadeDelete: true)
              .Index(t => t.MACOURSE)
              .Index(t => t.MaHP);

            CreateTable(
                "dbo.CTQLCOURSE",
                c => new
                    {
                        MACOURSE = c.String(nullable: false, maxLength: 30),
                        MAGH = c.Int(nullable: false),
                        TENCOURSE = c.String(maxLength: 50),
                        chitiet = c.String(),
                        Hinh = c.String(),
                    })
                .PrimaryKey(t => new { t.MACOURSE, t.MAGH })
                .ForeignKey("dbo.COURSE", t => t.MACOURSE, cascadeDelete: true)
                .ForeignKey("dbo.QLCOURSE", t => t.MAGH, cascadeDelete: true)
                .Index(t => t.MACOURSE)
                .Index(t => t.MAGH);
            
            CreateTable(
                "dbo.QLCOURSE",
                c => new
                    {
                        MAGH = c.Int(nullable: false, identity: true),
                        TONGVD = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MAGH)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.THONGKE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false),
                        SoTruyCap = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QLCOURSE", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CTQLCOURSE", "MAGH", "dbo.QLCOURSE");
            DropForeignKey("dbo.CTQLCOURSE", "MACOURSE", "dbo.COURSE");
            DropForeignKey("dbo.VIDEO", "MACOURSE", "dbo.COURSE");
            DropForeignKey("dbo.COURSE", "MALOAI", "dbo.LOAICOURSE");
            DropForeignKey("dbo.BLOG", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KHACHHANG", "BLOG_MABLOG", "dbo.BLOG");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QLCOURSE", new[] { "Id" });
            DropIndex("dbo.CTQLCOURSE", new[] { "MAGH" });
            DropIndex("dbo.CTQLCOURSE", new[] { "MACOURSE" });
            DropIndex("dbo.VIDEO", new[] { "MACOURSE" });
            DropIndex("dbo.COURSE", new[] { "MALOAI" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.KHACHHANG", new[] { "BLOG_MABLOG" });
            DropIndex("dbo.BLOG", new[] { "Id" });
            DropTable("dbo.THONGKE");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NEWS");
            DropTable("dbo.QLCOURSE");
            DropTable("dbo.CTQLCOURSE");
            DropTable("dbo.VIDEO");
            DropTable("dbo.LOAICOURSE");
            DropTable("dbo.COURSE");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.KHACHHANG");
            DropTable("dbo.BLOG");
        }
    }
}
