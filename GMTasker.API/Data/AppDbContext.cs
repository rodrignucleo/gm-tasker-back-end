using GMTasker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Data{
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioModel>? Usuario {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=db_restaurante.db;Cache=Shared");
            
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<UsuarioModel>().ToTable("Usuario");
        }
    }
}