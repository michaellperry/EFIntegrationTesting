namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoyaltyCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoyaltyCard",
                c => new
                    {
                        LoyaltyCardId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CardNumber = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.LoyaltyCardId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CardNumber, unique: true, name: "IX_U_CardNumber");
            
            AddColumn("dbo.CartItem", "ItemTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoyaltyCard", "UserId", "dbo.User");
            DropIndex("dbo.LoyaltyCard", "IX_U_CardNumber");
            DropIndex("dbo.LoyaltyCard", new[] { "UserId" });
            DropColumn("dbo.CartItem", "ItemTotal");
            DropTable("dbo.LoyaltyCard");
        }
    }
}
