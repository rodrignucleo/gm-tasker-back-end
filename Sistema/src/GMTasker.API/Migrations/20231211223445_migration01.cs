﻿using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GMTasker.API.Migrations
{
    /// <inheritdoc />
    public partial class migration01 : Migration
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
                name: "tb_ponto",
                columns: table => new
                {
                    id_ponto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    data_ponto = table.Column<string>(type: "longtext", nullable: false),
                    hora_ponto = table.Column<string>(type: "longtext", nullable: false),
                    id_usuario_criacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ponto", x => x.id_ponto);
                    table.ForeignKey(
                        name: "FK_tb_ponto_tb_usuario_id_usuario_criacao",
                        column: x => x.id_usuario_criacao,
                        principalTable: "tb_usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
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
                    data_cadastro = table.Column<string>(type: "longtext", nullable: true),
                    data_conclusao = table.Column<string>(type: "longtext", nullable: true),
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
                    data_cadastro = table.Column<string>(type: "longtext", nullable: false),
                    data_conclusao = table.Column<string>(type: "longtext", nullable: false),
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
                    { 1, "12345678910", "rodrignucleo@gmtasker.com", "Rodrigo Ribeiro", "$2a$10$6VMRh4gx8NBBN7mVeLSJte5VfW/xafA9QbNORGgfs2k94eopNWY1K", "$2a$10$oqAdQrKQFZPTNUK0CZEOguSGYsYVQhtpJC7NaSB5YGfklnuzI7wje", "11992668225" },
                    { 2, "98765412398", "patricia.oliveira@gmtasker.com", "Patricia Oliveira", "$2a$10$ECTQtVUO/UX.xRTO/TbHZ./cg3/fkHeTeENzA8RWIiQTk5GlNcW52", "$2a$10$nZMSxl6QxQUN669y/eoxjeg4SuHh1FjoStYrtZ7rWFuvXWGXye/rq", "9899265826597" }
                });

            migrationBuilder.InsertData(
                table: "tb_requisicao",
                columns: new[] { "id_requisicao", "data_cadastro", "data_conclusao", "descricao", "id_atual_responsavel", "id_sprint", "id_status", "id_usuario_criacao", "nome" },
                values: new object[] { 1, "01/06/2023", "15/06/2023", null, 1, null, 1, 2, "Desenvolver API" });

            migrationBuilder.InsertData(
                table: "tb_sprint",
                columns: new[] { "id_sprint", "data_cadastro", "data_conclusao", "descricao", "id_status", "id_usuario_criacao", "nome" },
                values: new object[] { 1, "01/06/2023", "15/06/2023", null, 2, 1, "JUNHO 1 a 15" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_ponto_id_usuario_criacao",
                table: "tb_ponto",
                column: "id_usuario_criacao");

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
                name: "tb_ponto");

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
