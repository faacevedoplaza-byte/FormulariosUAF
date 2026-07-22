using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRutYNombreUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidoMaterno",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApellidoPaterno",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rut",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            // 'NetcarBusinessNumber' (Requests) fue agregada fuera de migración por la
            // feature Netcar previa: en QA ya existe, pero en producción puede faltar.
            // Se agrega SOLO si no existe (idempotente) para no romper ni un lado ni el otro.
            migrationBuilder.Sql(
                "IF COL_LENGTH('dbo.Requests', 'NetcarBusinessNumber') IS NULL " +
                "ALTER TABLE [dbo].[Requests] ADD [NetcarBusinessNumber] nvarchar(max) NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoMaterno",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ApellidoPaterno",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rut",
                table: "Usuarios");
        }
    }
}
