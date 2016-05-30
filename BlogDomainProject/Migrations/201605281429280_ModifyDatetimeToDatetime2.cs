namespace BlogDomainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDatetimeToDatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.roles", "created_date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.roles", "last_modified_date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.roles", "last_modified_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.roles", "created_date", c => c.DateTime(nullable: false));
        }
    }
}
