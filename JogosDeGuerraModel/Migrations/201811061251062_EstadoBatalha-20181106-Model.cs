namespace JogosDeGuerraModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstadoBatalha20181106Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batalhas", "Estado", c => c.Int(nullable: false));
            AddColumn("dbo.Batalhas", "Tabuleiro_Id", c => c.Int());
            CreateIndex("dbo.Batalhas", "Tabuleiro_Id");
            AddForeignKey("dbo.Batalhas", "Tabuleiro_Id", "dbo.Tabuleiroes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Batalhas", "Tabuleiro_Id", "dbo.Tabuleiroes");
            DropIndex("dbo.Batalhas", new[] { "Tabuleiro_Id" });
            DropColumn("dbo.Batalhas", "Tabuleiro_Id");
            DropColumn("dbo.Batalhas", "Estado");
        }
    }
}
