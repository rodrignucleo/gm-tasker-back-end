using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GMTasker.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_status",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: false),
                    conta_horas = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_status", x => x.id_status);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: false),
                    cpf = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    telefone = table.Column<string>(type: "longtext", nullable: false),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    senha = table.Column<string>(type: "longtext", nullable: false),
                    senha_antiga = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id_usuario);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_sprint",
                columns: table => new
                {
                    id_sprint = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: false),
                    descricao = table.Column<string>(type: "longtext", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_conclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    id_status = table.Column<int>(type: "int", nullable: false),
                    id_usuario_criacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sprint", x => x.id_sprint);
                    table.ForeignKey(
                        name: "FK_tb_sprint_tb_status_id_status",
                        column: x => x.id_status,
                        principalTable: "tb_status",
                        principalColumn: "id_status",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_sprint_tb_usuario_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "tb_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_requisicao",
                columns: table => new
                {
                    id_requisicao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext", nullable: false),
                    descricao = table.Column<string>(type: "longtext", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_prevista_conclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_conclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    id_status = table.Column<int>(type: "int", nullable: false),
                    id_atual_responsavel = table.Column<int>(type: "int", nullable: false),
                    id_usuario_criacao = table.Column<int>(type: "int", nullable: false),
                    id_sprint = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_requisicao", x => x.id_requisicao);
                    table.ForeignKey(
                        name: "FK_tb_requisicao_tb_sprint_id_sprint",
                        column: x => x.id_sprint,
                        principalTable: "tb_sprint",
                        principalColumn: "id_sprint");
                    table.ForeignKey(
                        name: "FK_tb_requisicao_tb_status_id_status",
                        column: x => x.id_status,
                        principalTable: "tb_status",
                        principalColumn: "id_status",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_requisicao_tb_usuario_id_atual_responsavel",
                        column: x => x.id_atual_responsavel,
                        principalTable: "tb_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_requisicao_tb_usuario_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "tb_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tb_status",
                columns: new[] { "id_status", "conta_horas", "nome" },
                values: new object[,]
                {
                    { 1, true, "DESENVOLVIMENTO" },
                    { 2, false, "AGUARDANDO" },
                    { 3, false, "CONCLUIDO" }
                });

            migrationBuilder.InsertData(
                table: "tb_usuario",
                columns: new[] { "id_usuario", "cpf", "email", "nome", "senha", "senha_antiga", "telefone" },
                values: new object[,]
                {
                    { 1, "12345678910", "rodrignucleo@gmtasker.com", "Rodrigo Ribeiro", "$2a$10$98deNIMv75FawNSH/BY9lObUVMGMBzOPFDtnAx/GeK/UKXDX5/hc.", "$2a$10$4LnnDPZ5WRXhwgWQHf4.a.HLrdZGH9H3YQqnamSJUStNjQB6b.ayq", "11992668225" },
                    { 2, "98765412398", "patricia.oliveira@gmtasker.com", "Patricia Oliveira", "$2a$10$.Zi97I79ayAwrWHfQ/X/QORms.cnArruFFFSj7OPMH6b5sKYWa9eq", "$2a$10$3TLZkaUbm.2uuKQlIo5szOFHopbLLQRSiNxhgE.Uym2mfGjyM4I8C", "9899265826597" }
                });

            migrationBuilder.InsertData(
                table: "tb_requisicao",
                columns: new[] { "id_requisicao", "data_cadastro", "data_conclusao", "data_prevista_conclusao", "descricao", "id_atual_responsavel", "id_sprint", "id_status", "id_usuario_criacao", "nome" },
                values: new object[] { 1, new DateTime(2023, 5, 28, 20, 57, 49, 590, DateTimeKind.Local).AddTicks(4302), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 4, 20, 57, 49, 590, DateTimeKind.Local).AddTicks(4303), null, 1, null, 1, 2, "Desenvolver API" });

            migrationBuilder.InsertData(
                table: "tb_sprint",
                columns: new[] { "id_sprint", "data_cadastro", "data_conclusao", "descricao", "id_status", "id_usuario_criacao", "nome" },
                values: new object[] { 1, new DateTime(2023, 5, 28, 20, 57, 49, 590, DateTimeKind.Local).AddTicks(3940), new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Local), null, 2, 1, "JUNHO 1 a 15" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_requisicao_id_atual_responsavel",
                table: "tb_requisicao",
                column: "id_atual_responsavel");

            migrationBuilder.CreateIndex(
                name: "IX_tb_requisicao_id_sprint",
                table: "tb_requisicao",
                column: "id_sprint");

            migrationBuilder.CreateIndex(
                name: "IX_tb_requisicao_id_status",
                table: "tb_requisicao",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_tb_requisicao_id_usuario_criacao",
                table: "tb_requisicao",
                column: "id_usuario_criacao");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sprint_id_status",
                table: "tb_sprint",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sprint_id_usuario_criacao",
                table: "tb_sprint",
                column: "id_usuario_criacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_requisicao");

            migrationBuilder.DropTable(
                name: "tb_sprint");

            migrationBuilder.DropTable(
                name: "tb_status");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
