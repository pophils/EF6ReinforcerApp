namespace BlogDomainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.authors",
                c => new
                    {
                        user_id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.blogs",
                c => new
                    {
                        blog_id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 20),
                        body = c.String(nullable: false, unicode: false, storeType: "text"),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                        category_id = c.Int(nullable: false),
                        author_user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.blog_id)
                .ForeignKey("dbo.authors", t => t.author_user_id, cascadeDelete: true)
                .ForeignKey("dbo.categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id)
                .Index(t => t.author_user_id);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        body = c.String(nullable: false, unicode: false, storeType: "text"),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                        blog_id = c.Int(nullable: false),
                        poster_user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.blogs", t => t.blog_id, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.poster_user_id, cascadeDelete: true)
                .Index(t => t.blog_id)
                .Index(t => t.poster_user_id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 20),
                        password = c.String(nullable: false, maxLength: 20),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.roles", t => t.role_id, cascadeDelete: true)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.readers",
                c => new
                    {
                        user_id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.roles",
                c => new
                    {
                        role_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        created_date = c.DateTime(nullable: false),
                        last_modified_date = c.DateTime(nullable: false),
                        created_by = c.String(nullable: false, maxLength: 50),
                        last_modified_by = c.String(maxLength: 50),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.role_id)
                .ForeignKey("dbo.users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.authors", "user_id", "dbo.users");
            DropForeignKey("dbo.comments", "poster_user_id", "dbo.users");
            DropForeignKey("dbo.roles", "User_Id", "dbo.users");
            DropForeignKey("dbo.users", "role_id", "dbo.roles");
            DropForeignKey("dbo.readers", "user_id", "dbo.users");
            DropForeignKey("dbo.comments", "blog_id", "dbo.blogs");
            DropForeignKey("dbo.blogs", "category_id", "dbo.categories");
            DropForeignKey("dbo.blogs", "author_user_id", "dbo.authors");
            DropIndex("dbo.roles", new[] { "User_Id" });
            DropIndex("dbo.readers", new[] { "user_id" });
            DropIndex("dbo.users", new[] { "role_id" });
            DropIndex("dbo.comments", new[] { "poster_user_id" });
            DropIndex("dbo.comments", new[] { "blog_id" });
            DropIndex("dbo.blogs", new[] { "author_user_id" });
            DropIndex("dbo.blogs", new[] { "category_id" });
            DropIndex("dbo.authors", new[] { "user_id" });
            DropTable("dbo.roles");
            DropTable("dbo.readers");
            DropTable("dbo.users");
            DropTable("dbo.comments");
            DropTable("dbo.categories");
            DropTable("dbo.blogs");
            DropTable("dbo.authors");
        }
    }
}
