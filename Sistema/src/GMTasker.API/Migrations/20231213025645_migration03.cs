using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMTasker.API.Migrations
{
    /// <inheritdoc />
    public partial class migration03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 1,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$c6mmZEsJDx.4hxuuyHSLs.6QwokWpgpszrgA0TeHenc1u3J6nODQO", "$2a$10$DaCtlhGPMdw1bBXWaCIHze5QGVjUtFPOhKxWebsqtyzdn15ZvmAB6" });

            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "id_usuario",
                keyValue: 2,
                columns: new[] { "senha", "senha_antiga" },
                values: new object[] { "$2a$10$Eb4h.XHyvWoWVwaSwmuNceU9o3qNTNfANzp.H4facBTo/CLZIVaWC", "$2a$10$9nKDQ0j/FI3E7rLjTIWDRuHjNK/0WRyh8at8e3jAZ/vgPUoVxh71i" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
