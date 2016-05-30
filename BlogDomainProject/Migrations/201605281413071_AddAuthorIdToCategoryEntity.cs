namespace BlogDomainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorIdToCategoryEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.categories", "author_user_id", c => c.Int(nullable: false));
            CreateIndex("dbo.categories", "author_user_id");
            AddForeignKey("dbo.categories", "author_user_id", "dbo.authors", "user_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.categories", "author_user_id", "dbo.authors");
            DropIndex("dbo.categories", new[] { "author_user_id" });
            DropColumn("dbo.categories", "author_user_id");
        }
    }
}
