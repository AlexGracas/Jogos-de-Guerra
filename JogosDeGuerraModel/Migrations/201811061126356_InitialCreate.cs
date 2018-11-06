namespace JogosDeGuerraModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElementoDoExercitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Saude = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Exercito_Id = c.Int(),
                        Tabuleiro_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercitoes", t => t.Exercito_Id)
                .ForeignKey("dbo.Tabuleiroes", t => t.Tabuleiro_Id)
                .Index(t => t.Exercito_Id)
                .Index(t => t.Tabuleiro_Id);
            
            CreateTable(
                "dbo.Exercitoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nacao = c.Int(nullable: false),
                        Batalha_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batalhas", t => t.Batalha_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.Batalha_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Batalhas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExercitoBranco_Id = c.Int(),
                        ExercitoPreto_Id = c.Int(),
                        Turno_Id = c.Int(),
                        Vencedor_Id = c.Int(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercitoes", t => t.ExercitoBranco_Id)
                .ForeignKey("dbo.Exercitoes", t => t.ExercitoPreto_Id)
                .ForeignKey("dbo.Exercitoes", t => t.Turno_Id)
                .ForeignKey("dbo.Exercitoes", t => t.Vencedor_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id)
                .Index(t => t.ExercitoBranco_Id)
                .Index(t => t.ExercitoPreto_Id)
                .Index(t => t.Turno_Id)
                .Index(t => t.Vencedor_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Tabuleiroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Largura = c.Int(nullable: false),
                        Altura = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercitoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Batalhas", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.ElementoDoExercitoes", "Tabuleiro_Id", "dbo.Tabuleiroes");
            DropForeignKey("dbo.ElementoDoExercitoes", "Exercito_Id", "dbo.Exercitoes");
            DropForeignKey("dbo.Exercitoes", "Batalha_Id", "dbo.Batalhas");
            DropForeignKey("dbo.Batalhas", "Vencedor_Id", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "Turno_Id", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "ExercitoPreto_Id", "dbo.Exercitoes");
            DropForeignKey("dbo.Batalhas", "ExercitoBranco_Id", "dbo.Exercitoes");
            DropIndex("dbo.Batalhas", new[] { "Usuario_Id" });
            DropIndex("dbo.Batalhas", new[] { "Vencedor_Id" });
            DropIndex("dbo.Batalhas", new[] { "Turno_Id" });
            DropIndex("dbo.Batalhas", new[] { "ExercitoPreto_Id" });
            DropIndex("dbo.Batalhas", new[] { "ExercitoBranco_Id" });
            DropIndex("dbo.Exercitoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Exercitoes", new[] { "Batalha_Id" });
            DropIndex("dbo.ElementoDoExercitoes", new[] { "Tabuleiro_Id" });
            DropIndex("dbo.ElementoDoExercitoes", new[] { "Exercito_Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Tabuleiroes");
            DropTable("dbo.Batalhas");
            DropTable("dbo.Exercitoes");
            DropTable("dbo.ElementoDoExercitoes");
        }
    }
}
