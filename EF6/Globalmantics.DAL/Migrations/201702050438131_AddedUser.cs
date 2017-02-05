namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Cart", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cart", "UserId");
            AddForeignKey("dbo.Cart", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cart", "UserId", "dbo.User");
            DropIndex("dbo.Cart", new[] { "UserId" });
            DropColumn("dbo.Cart", "UserId");
            DropTable("dbo.User");
        }
    }
}
