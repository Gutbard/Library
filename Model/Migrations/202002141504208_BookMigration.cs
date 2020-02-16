namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Books", new[] { "Title" });
            CreateIndex("dbo.Books", "Title", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Books", new[] { "Title" });
            CreateIndex("dbo.Books", "Title");
        }
    }
}
