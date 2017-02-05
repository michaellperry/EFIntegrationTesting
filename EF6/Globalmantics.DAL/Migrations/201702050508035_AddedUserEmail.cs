namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Email", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.User", "Email", unique: true, name: "IX_U_Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", "IX_U_Email");
            DropColumn("dbo.User", "Email");
        }
    }
}
