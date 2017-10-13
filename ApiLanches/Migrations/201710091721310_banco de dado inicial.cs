namespace ApiLanches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bancodedadoinicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredientes",
                c => new
                    {
                        IdIngrediente = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Valor = c.Double(nullable: false),
                        Qtd = c.Int(nullable: false),
                        SomaTotal = c.Double(nullable: false),
                        Lanche_IdLanche = c.Long(),
                    })
                .PrimaryKey(t => t.IdIngrediente)
                .ForeignKey("dbo.Lanches", t => t.Lanche_IdLanche)
                .Index(t => t.Lanche_IdLanche);
            
            CreateTable(
                "dbo.Lanches",
                c => new
                    {
                        IdLanche = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.IdLanche);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredientes", "Lanche_IdLanche", "dbo.Lanches");
            DropIndex("dbo.Ingredientes", new[] { "Lanche_IdLanche" });
            DropTable("dbo.Lanches");
            DropTable("dbo.Ingredientes");
        }
    }
}
