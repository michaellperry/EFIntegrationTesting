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
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true, name: "IX_U_Email");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", "IX_U_Email");
            DropTable("dbo.User");
        }
    }
}
