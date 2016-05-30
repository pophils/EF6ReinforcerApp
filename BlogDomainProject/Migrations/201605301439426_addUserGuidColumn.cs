namespace BlogDomainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserGuidColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "user_guid", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.users", "user_guid");
        }
    }
}
