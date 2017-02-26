namespace Globalmantics.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CatalogItem", "Sku", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.CatalogItem", "Sku", unique: true, name: "IX_U_Sku");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CatalogItem", "IX_U_Sku");
            DropColumn("dbo.CatalogItem", "Sku");
        }
    }
}
