namespace Fisabrantes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerasituacaoprofissional : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Funcionarios", "SituacaoProfissional");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionarios", "SituacaoProfissional", c => c.String());
        }
    }
}
