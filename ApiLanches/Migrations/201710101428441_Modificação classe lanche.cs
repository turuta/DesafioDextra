namespace ApiLanches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificaçãoclasselanche : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredientes", "Lanche_IdLanche", c => c.Long());
            CreateIndex("dbo.Ingredientes", "Lanche_IdLanche");
            AddForeignKey("dbo.Ingredientes", "Lanche_IdLanche", "dbo.Lanches", "IdLanche");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredientes", "Lanche_IdLanche", "dbo.Lanches");
            DropIndex("dbo.Ingredientes", new[] { "Lanche_IdLanche" });
            DropColumn("dbo.Ingredientes", "Lanche_IdLanche");
        }
    }
}
