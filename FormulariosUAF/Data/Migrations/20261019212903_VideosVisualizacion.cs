using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class VideosVisualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_VIDEOVISUALIZACION",
                columns: table => new
                {
                    IN_COD_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IN_CODSESION_VIDEOVISUALIZACION = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ST_VIDEO_VIDEOVISUALIZACION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IN_COD_USUARIO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DT_FECHAINICIO_VIDEOVISUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_FECHAULTIMAACTIVIDAD_VIDEOVISUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BO_REPRODUCIDO_VIDEOVISUALIZACION = table.Column<bool>(type: "bit", nullable: false),
                    IN_TOTALESCENAS_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    IN_ESCENAMAXIMA_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    ST_ESCENASVISTAS_VIDEOVISUALIZACION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IN_PAUSAS_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    IN_ADELANTOS_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    IN_RETROCESOS_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    IN_SEGUNDOS_VIDEOVISUALIZACION = table.Column<int>(type: "int", nullable: false),
                    BO_COMPLETO_VIDEOVISUALIZACION = table.Column<bool>(type: "bit", nullable: false),
                    BO_CONVOZ_VIDEOVISUALIZACION = table.Column<bool>(type: "bit", nullable: false),
                    BO_PANTALLACOMPLETA_VIDEOVISUALIZACION = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_VIDEOVISUALIZACION", x => x.IN_COD_VIDEOVISUALIZACION);
                    table.ForeignKey(
                        name: "FK_T_VIDEOVISUALIZACION_T_USUARIO_IN_COD_USUARIO",
                        column: x => x.IN_COD_USUARIO,
                        principalTable: "T_USUARIO",
                        principalColumn: "IN_COD_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_VIDEOVISUALIZACION_DT_FECHAINICIO_VIDEOVISUALIZACION",
                table: "T_VIDEOVISUALIZACION",
                column: "DT_FECHAINICIO_VIDEOVISUALIZACION");

            migrationBuilder.CreateIndex(
                name: "IX_T_VIDEOVISUALIZACION_IN_COD_USUARIO",
                table: "T_VIDEOVISUALIZACION",
                column: "IN_COD_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_T_VIDEOVISUALIZACION_IN_CODSESION_VIDEOVISUALIZACION",
                table: "T_VIDEOVISUALIZACION",
                column: "IN_CODSESION_VIDEOVISUALIZACION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_VIDEOVISUALIZACION_ST_VIDEO_VIDEOVISUALIZACION_IN_COD_USUARIO",
                table: "T_VIDEOVISUALIZACION",
                columns: new[] { "ST_VIDEO_VIDEOVISUALIZACION", "IN_COD_USUARIO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_VIDEOVISUALIZACION");
        }
    }
}
