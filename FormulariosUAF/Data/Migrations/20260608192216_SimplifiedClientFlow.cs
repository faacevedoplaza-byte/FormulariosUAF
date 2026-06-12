using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulariosUAF.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifiedClientFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignatureDateTime",
                table: "Declarants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureFullName",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SignatureIdNumber",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SignatureIpAddress",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureUserAgent",
                table: "Declarants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeclaredPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipationPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
                    RelationshipTypeOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasMinTenPercentParticipation = table.Column<bool>(type: "bit", nullable: false),
                    IsEffectiveController = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveControlDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandlesCashOrFunds = table.Column<bool>(type: "bit", nullable: false),
                    IsPEP = table.Column<bool>(type: "bit", nullable: false),
                    PepType = table.Column<int>(type: "int", nullable: true),
                    PepTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PepInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PepPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PepRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PepObservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclaredPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeclaredPersons_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxFolderAnalyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtractedRut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedBusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedLegalRep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedIssuedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RawText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasReadable = table.Column<bool>(type: "bit", nullable: false),
                    AnalyzedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFolderAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxFolderAnalyses_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxFolderAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxFolderAnalysisId = table.Column<int>(type: "int", nullable: false),
                    AlertType = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxFolderAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId",
                        column: x => x.TaxFolderAnalysisId,
                        principalTable: "TaxFolderAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeclaredPersons_RequestId",
                table: "DeclaredPersons",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxFolderAlerts_TaxFolderAnalysisId",
                table: "TaxFolderAlerts",
                column: "TaxFolderAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxFolderAnalyses_RequestId",
                table: "TaxFolderAnalyses",
                column: "RequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclaredPersons");

            migrationBuilder.DropTable(
                name: "TaxFolderAlerts");

            migrationBuilder.DropTable(
                name: "TaxFolderAnalyses");

            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "SignatureDateTime",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "SignatureFullName",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "SignatureIdNumber",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "SignatureIpAddress",
                table: "Declarants");

            migrationBuilder.DropColumn(
                name: "SignatureUserAgent",
                table: "Declarants");
        }
    }
}
