using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class GestionOperaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_GESTIONOPERACION",
                columns: table => new
                {
                    IN_COD_GESTIONOPERACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IN_CODOPERACIONREGCHEQ_GESTIONOPERACION = table.Column<int>(type: "int", nullable: false),
                    IN_CODIGOOPERACION_GESTIONOPERACION = table.Column<int>(type: "int", nullable: true),
                    ST_RUTCLIENTE_GESTIONOPERACION = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ST_NOMBRECLIENTE_GESTIONOPERACION = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IN_COD_USUARIO = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DT_FECHATOMA_GESTIONOPERACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DT_FECHACREACION_GESTIONOPERACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_FECHACIERRE_GESTIONOPERACION = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GESTIONOPERACION", x => x.IN_COD_GESTIONOPERACION);
                    table.ForeignKey(
                        name: "FK_T_GESTIONOPERACION_T_USUARIO_IN_COD_USUARIO",
                        column: x => x.IN_COD_USUARIO,
                        principalTable: "T_USUARIO",
                        principalColumn: "IN_COD_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_GESTIONOPERACIONNOTA",
                columns: table => new
                {
                    IN_COD_GESTIONOPERACIONNOTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IN_COD_GESTIONOPERACION = table.Column<int>(type: "int", nullable: false),
                    IN_COD_USUARIO = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ST_NOMBREUSUARIO_GESTIONOPERACIONNOTA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ST_TEXTO_GESTIONOPERACIONNOTA = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    BO_SISTEMA_GESTIONOPERACIONNOTA = table.Column<bool>(type: "bit", nullable: false),
                    DT_FECHACREACION_GESTIONOPERACIONNOTA = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GESTIONOPERACIONNOTA", x => x.IN_COD_GESTIONOPERACIONNOTA);
                    table.ForeignKey(
                        name: "FK_T_GESTIONOPERACIONNOTA_T_GESTIONOPERACION_IN_COD_GESTIONOPERACION",
                        column: x => x.IN_COD_GESTIONOPERACION,
                        principalTable: "T_GESTIONOPERACION",
                        principalColumn: "IN_COD_GESTIONOPERACION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_GESTIONOPERACION_DT_FECHACIERRE_GESTIONOPERACION",
                table: "T_GESTIONOPERACION",
                column: "DT_FECHACIERRE_GESTIONOPERACION");

            migrationBuilder.CreateIndex(
                name: "IX_T_GESTIONOPERACION_IN_COD_USUARIO",
                table: "T_GESTIONOPERACION",
                column: "IN_COD_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_T_GESTIONOPERACION_IN_CODOPERACIONREGCHEQ_GESTIONOPERACION",
                table: "T_GESTIONOPERACION",
                column: "IN_CODOPERACIONREGCHEQ_GESTIONOPERACION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_GESTIONOPERACIONNOTA_IN_COD_GESTIONOPERACION",
                table: "T_GESTIONOPERACIONNOTA",
                column: "IN_COD_GESTIONOPERACION");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_GESTIONOPERACIONNOTA");

            migrationBuilder.DropTable(
                name: "T_GESTIONOPERACION");
        }
    }
}
