using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class CapacitacionEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "T_EMPRESA",
                columns: table => new
                {
                    IN_COD_EMPRESA = table.Column<int>(type: "int", nullable: false),
                    ST_NOMBRE_EMPRESA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IN_ORDEN_EMPRESA = table.Column<int>(type: "int", nullable: false),
                    BO_ACTIVO_EMPRESA = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPRESA", x => x.IN_COD_EMPRESA);
                });

            migrationBuilder.InsertData(
                table: "T_EMPRESA",
                columns: new[] { "IN_COD_EMPRESA", "BO_ACTIVO_EMPRESA", "ST_NOMBRE_EMPRESA", "IN_ORDEN_EMPRESA" },
                values: new object[,]
                {
                    { 1, true, "Comercial Automotriz Siglo 21", 1 },
                    { 2, true, "Jorge Valenzuela Delarze", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CAPACITACIONASISTENCIA_IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA",
                column: "IN_COD_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPRESA_ST_NOMBRE_EMPRESA",
                table: "T_EMPRESA",
                column: "ST_NOMBRE_EMPRESA",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CAPACITACIONASISTENCIA_T_EMPRESA_IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA",
                column: "IN_COD_EMPRESA",
                principalTable: "T_EMPRESA",
                principalColumn: "IN_COD_EMPRESA",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_CAPACITACIONASISTENCIA_T_EMPRESA_IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA");

            migrationBuilder.DropTable(
                name: "T_EMPRESA");

            migrationBuilder.DropIndex(
                name: "IX_T_CAPACITACIONASISTENCIA_IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA");

            migrationBuilder.DropColumn(
                name: "IN_COD_EMPRESA",
                table: "T_CAPACITACIONASISTENCIA");
        }
    }
}
