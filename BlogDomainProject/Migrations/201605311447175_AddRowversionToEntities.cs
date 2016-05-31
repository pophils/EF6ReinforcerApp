namespace BlogDomainProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowversionToEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.authors", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.blogs", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.categories", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.comments", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.users", "created_date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.users", "last_modified_date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.users", "created_by", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.users", "last_modified_by", c => c.String(maxLength: 50));
            AddColumn("dbo.users", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.readers", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.roles", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.roles", "RowVersion");
            DropColumn("dbo.readers", "RowVersion");
            DropColumn("dbo.users", "RowVersion");
            DropColumn("dbo.users", "last_modified_by");
            DropColumn("dbo.users", "created_by");
            DropColumn("dbo.users", "last_modified_date");
            DropColumn("dbo.users", "created_date");
            DropColumn("dbo.comments", "RowVersion");
            DropColumn("dbo.categories", "RowVersion");
            DropColumn("dbo.blogs", "RowVersion");
            DropColumn("dbo.authors", "RowVersion");
        }
    }
}
