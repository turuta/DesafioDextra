namespace ApiLanches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bancodedadoiniciald : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lanches", "ValorTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lanches", "ValorTotal");
        }
    }
}
