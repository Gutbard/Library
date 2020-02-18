namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        MiddleName = c.String(maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId)
                .Index(t => t.FirstName)
                .Index(t => new { t.FirstName, t.LastName }, unique: true)
                .Index(t => t.LastName);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Summary = c.String(),
                        Year = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        Author_AuthorId = c.Int(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.Author_AuthorId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Title, unique: true)
                .Index(t => t.Genre)
                .Index(t => t.Author_AuthorId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ClientId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.BorrowedHistories",
                c => new
                    {
                        BorrowedHistoryId = c.Int(nullable: false, identity: true),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Book_BookId = c.Guid(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.BorrowedHistoryId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Book_BookId)
                .Index(t => t.Client_ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.BorrowedHistories", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.BorrowedHistories", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.BorrowedHistories", new[] { "Client_ClientId" });
            DropIndex("dbo.BorrowedHistories", new[] { "Book_BookId" });
            DropIndex("dbo.Clients", new[] { "Name" });
            DropIndex("dbo.Books", new[] { "Client_ClientId" });
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            DropIndex("dbo.Books", new[] { "Genre" });
            DropIndex("dbo.Books", new[] { "Title" });
            DropIndex("dbo.Authors", new[] { "LastName" });
            DropIndex("dbo.Authors", new[] { "FirstName", "LastName" });
            DropIndex("dbo.Authors", new[] { "FirstName" });
            DropTable("dbo.BorrowedHistories");
            DropTable("dbo.Clients");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
