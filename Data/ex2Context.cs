namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ex2Context : DbContext
    {
        public ex2Context()
            : base("ex2Entities")
        {
        }

        public virtual DbSet<MARCA> MARCA { get; set; }
        public virtual DbSet<PATRIMONIO> PATRIMONIO { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MARCA>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<MARCA>()
                .HasMany(e => e.PATRIMONIO)
                .WithRequired(e => e.MARCA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PATRIMONIO>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<PATRIMONIO>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.senha)
                .IsUnicode(false);
        }
    }
}
