using GMTasker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GMTasker.API.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel>? tb_usuario {get; set;}
        public DbSet<StatusModel>? tb_status {get; set;}
        public DbSet<SprintModel>? tb_sprint {get; set;}
        public DbSet<RequisicaoModel>? tb_requisicao {get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<StatusModel>(entity =>
            {
                entity.HasKey(e => e.id_status);
                entity.Property(e => e.nome).IsRequired();
                entity.HasData(
                    new StatusModel
                    {
                        id_status = 1,
                        nome = "DESENVOLVIMENTO",
                        conta_horas = true
                    });
                entity.HasData(
                    new StatusModel
                    {
                        id_status = 2,
                        nome = "AGUARDANDO",
                        conta_horas = false
                    });
                entity.HasData(
                    new StatusModel
                    {
                        id_status = 3,
                        nome = "CONCLUIDO",
                        conta_horas = false
                    });
            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                // modelBuilder.Entity<UsuarioModel>().Property().HasMaxLength(15);
                
                entity.HasKey(e => e.id_usuario);
                entity.Property(e => e.nome).IsRequired();
                entity.Property(p => p.cpf).HasMaxLength(15);
                entity.HasData(
                    new UsuarioModel
                    {
                        id_usuario = 1,
                        nome = "Rodrigo Ribeiro",
                        cpf = "12345678910",
                        telefone = "11992668225",
                        login = "rodrignucleo",
                        senha = BCrypt.Net.BCrypt.HashPassword("123")
                    });
                entity.HasData(
                    new UsuarioModel
                    {
                        id_usuario = 2,
                        nome = "Patricia Oliveira",
                        cpf = "98765412398",
                        telefone = "9899265826597",
                        login = "patricia.oliveira"
                    });
            });

            modelBuilder.Entity<SprintModel>(entity =>
            {
                entity.HasKey(e => e.id_sprint);
                entity.Property(e => e.nome).IsRequired();
                entity.HasData(
                    new SprintModel
                    {
                        id_sprint = 1,
                        id_status = 2,
                        nome = "JUNHO 1 a 15",
                        data_cadastro = DateTime.Now,
                        data_conclusao = DateTime.Today,
                        id_usuario_criacao = 1
                    });
            });

            modelBuilder.Entity<RequisicaoModel>(entity =>
            {
                entity.HasKey(e => e.id_requisicao);
                entity.Property(e => e.nome).IsRequired();
                entity.HasData(
                    new RequisicaoModel
                    {
                        id_requisicao = 1,
                        id_status = 1,
                        nome = "Desenvolver API",
                        data_cadastro = DateTime.Now,
                        data_prevista_conclusao = DateTime.Now.AddDays(7),
                        id_atual_responsavel = 1,
                        id_usuario_criacao = 2
                    });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseMySQL("server=localhost; userid=root;pwd=1234;port=3306;database=gmtasker_db");
             options.UseMySQL("server=localhost;database=gmtasker_db;user=root;port=3306;password=123123");
        }
        
    }
}