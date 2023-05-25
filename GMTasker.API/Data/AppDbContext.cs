using GMTasker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel>? Usuario {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>()
                .Property(p => p.Cpf)
                    .HasMaxLength(15);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.Nome).IsRequired();
                entity.HasData(
                    new UsuarioModel
                    {
                        IdUsuario = 1,
                        Nome = "Rodrigo Ribeiro",
                        Cpf = "12345678910",
                        Telefone = "11992668225",
                        Login = "rodrignucleo"
                    });
            });
                
                //.ToTable("Usuario");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL("server=localhost;database=restaurantedb;user=root;password=1234");
        }
        
    }
}