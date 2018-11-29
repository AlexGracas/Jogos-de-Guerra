namespace JogosDeGuerraModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelJogosDeGuerra : DbContext
    {
        // Your context has been configured to use a 'ModelJogosDeGuerra' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'JogosDeGuerraModel.ModelJogosDeGuerra' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelJogosDeGuerra' 
        // connection string in the application configuration file.
        public ModelJogosDeGuerra()
            : base("name=ModelJogosDeGuerra")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Arqueiro> Arqueiros { get; set; }

        public virtual DbSet<Guerreiro> Guerreiros { get; set; }

        public virtual DbSet<Cavaleiro> Cavalarias { get; set; }

        public virtual DbSet<Exercito> Exercitos { get; set; }


        public virtual DbSet<Batalha> Batalhas { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<ElementoDoExercito> ElementosDoExercitos { get; set; }

        public System.Data.Entity.DbSet<JogosDeGuerraModel.Tabuleiro> Tabuleiroes { get; set; }
    }
        
}