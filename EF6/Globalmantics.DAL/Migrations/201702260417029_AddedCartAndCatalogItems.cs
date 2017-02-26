namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCartAndCatalogItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItem",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        CartId = c.Int(nullable: false),
                        CatalogItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.Cart", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.CatalogItem", t => t.CatalogItemId)
                .Index(t => t.CartId)
                .Index(t => t.CatalogItemId);
            
            CreateTable(
                "dbo.CatalogItem",
                c => new
                    {
                        CatalogItemId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CatalogItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItem", "CatalogItemId", "dbo.CatalogItem");
            DropForeignKey("dbo.CartItem", "CartId", "dbo.Cart");
            DropIndex("dbo.CartItem", new[] { "CatalogItemId" });
            DropIndex("dbo.CartItem", new[] { "CartId" });
            DropTable("dbo.CatalogItem");
            DropTable("dbo.CartItem");
        }
    }
}
