namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCartAndCartLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartLine",
                c => new
                    {
                        CartLineId = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CartLineId)
                .ForeignKey("dbo.Cart", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CreatedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartLine", "CartId", "dbo.Cart");
            DropIndex("dbo.CartLine", new[] { "CartId" });
            DropTable("dbo.Cart");
            DropTable("dbo.CartLine");
        }
    }
}
