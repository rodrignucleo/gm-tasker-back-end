using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMTasker.API.Migrations
{
    /// <inheritdoc />
    public partial class migration02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "tb_ponto",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 1,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$ThFGPi2ZAeJAjElINTJoLe8TxnEkyWuNP207MEBxfIpOm.vapFqyW", "$2a$10$JCCwrRHy88NZp6WLaX60/ekUuNZB96oOU0ZpyLjJJ28uLo4B1HKMa" });

            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 2,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$fpxJRJjZBdSe03bDCOQMAesw.pva6gTYd/mnx5gF7eX0Uy2OEyC5m", "$2a$10$sabN5j0dxtOajvmqaR.JfOc6m8GNcZFoj1NGNfWGDyr2y8H8dOC8C" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "tb_ponto");

            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 1,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$6VMRh4gx8NBBN7mVeLSJte5VfW/xafA9QbNORGgfs2k94eopNWY1K", "$2a$10$oqAdQrKQFZPTNUK0CZEOguSGYsYVQhtpJC7NaSB5YGfklnuzI7wje" });

            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 2,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$ECTQtVUO/UX.xRTO/TbHZ./cg3/fkHeTeENzA8RWIiQTk5GlNcW52", "$2a$10$nZMSxl6QxQUN669y/eoxjeg4SuHh1FjoStYrtZ7rWFuvXWGXye/rq" });
        }
    }
}
