using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class Capacitaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_CAPACITACION",
                columns: table => new
                {
                    IN_COD_CAPACITACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ST_TITULO_CAPACITACION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ST_DESCRIPCION_CAPACITACION = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DT_FECHA_CAPACITACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ST_LUGAR_CAPACITACION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ST_RELATOR_CAPACITACION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ST_TOKEN_CAPACITACION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BO_REGISTROABIERTO_CAPACITACION = table.Column<bool>(type: "bit", nullable: false),
                    ST_CREADOPOR_CAPACITACION = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DT_FECHACREACION_CAPACITACION = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAPACITACION", x => x.IN_COD_CAPACITACION);
                });

            migrationBuilder.CreateTable(
                name: "T_CAPACITACIONASISTENCIA",
                columns: table => new
                {
                    IN_COD_CAPACITACIONASISTENCIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IN_COD_CAPACITACION = table.Column<int>(type: "int", nullable: false),
                    ST_NOMBRECOMPLETO_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ST_RUT_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ST_EMAIL_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ST_TELEFONO_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ST_CARGO_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ST_SUCURSAL_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BO_ACEPTAASISTENCIA_CAPACITACIONASISTENCIA = table.Column<bool>(type: "bit", nullable: false),
                    DT_FECHAREGISTRO_CAPACITACIONASISTENCIA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ST_DIRECCIONIP_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ST_USERAGENT_CAPACITACIONASISTENCIA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CAPACITACIONASISTENCIA", x => x.IN_COD_CAPACITACIONASISTENCIA);
                    table.ForeignKey(
                        name: "FK_T_CAPACITACIONASISTENCIA_T_CAPACITACION_IN_COD_CAPACITACION",
                        column: x => x.IN_COD_CAPACITACION,
                        principalTable: "T_CAPACITACION",
                        principalColumn: "IN_COD_CAPACITACION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CAPACITACION_DT_FECHA_CAPACITACION",
                table: "T_CAPACITACION",
                column: "DT_FECHA_CAPACITACION");

            migrationBuilder.CreateIndex(
                name: "IX_T_CAPACITACION_ST_TOKEN_CAPACITACION",
                table: "T_CAPACITACION",
                column: "ST_TOKEN_CAPACITACION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_CAPACITACIONASISTENCIA_IN_COD_CAPACITACION_ST_RUT_CAPACITACIONASISTENCIA",
                table: "T_CAPACITACIONASISTENCIA",
                columns: new[] { "IN_COD_CAPACITACION", "ST_RUT_CAPACITACIONASISTENCIA" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_CAPACITACIONASISTENCIA_ST_RUT_CAPACITACIONASISTENCIA",
                table: "T_CAPACITACIONASISTENCIA",
                column: "ST_RUT_CAPACITACIONASISTENCIA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_CAPACITACIONASISTENCIA");

            migrationBuilder.DropTable(
                name: "T_CAPACITACION");
        }
    }
}
