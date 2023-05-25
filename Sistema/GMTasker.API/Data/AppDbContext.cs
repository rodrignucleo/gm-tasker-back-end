using GMTasker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel>? tb_usuario {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>()
                .Property(p => p.cpf)
                    .HasMaxLength(15);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.Property(p => p.cpf).HasMaxLength(15);
                entity.HasKey(e => e.id_usuario);
                entity.Property(e => e.nome).IsRequired();
                entity.HasData(
                    new UsuarioModel
                    {
                        id_usuario = 1,
                        nome = "Rodrigo Ribeiro",
                        cpf = "12345678910",
                        telefone = "11992668225",
                        login = "rodrignucleo"
                    });
            });
                
                //.ToTable("Usuario");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL("server=localhost;database=gmtasker_db;user=root;password=1234");
        }
        
    }
}