using FormulariosUAF.Extensions;
using FormulariosUAF.Models.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestDocument = QuestPDF.Fluent.Document;

namespace FormulariosUAF.Services;

public class PdfService : IPdfService
{
    public byte[] GenerateBeneficialOwnerDeclarationPdf(Request request)
    {
        return QuestDocument.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("DECLARACIÓN DE BENEFICIARIO FINAL")
                        .Bold().FontSize(14);
                    col.Item().AlignCenter().Text($"Folio: {request.RequestNumber}")
                        .FontSize(11);
                    col.Item().LineHorizontal(1);
                });

                page.Content().PaddingTop(0.5f, Unit.Centimetre).Column(col =>
                {
                    col.Spacing(8);

                    col.Item().Text("La información declarada será utilizada para procesos de debida diligencia y conocimiento del cliente.")
                        .Italic().FontColor(Colors.Grey.Medium);

                    // Datos empresa
                    if (request.LegalEntityDeclaration is { } led)
                    {
                        col.Item().Text("I. ANTECEDENTES PERSONA JURÍDICA").Bold().FontSize(11);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                            AddTableRow(table, "RUT:", led.RUT);
                            AddTableRow(table, "Razón Social:", led.BusinessName);
                            AddTableRow(table, "Domicilio:", led.Address);
                            AddTableRow(table, "Ciudad:", led.City);
                            AddTableRow(table, "País:", led.CountryOfIncorporation);
                            AddTableRow(table, "Tipo de Entidad:", led.EntityType.ToDisplayString());
                            AddTableRow(table, "Representante Legal:", led.LegalRepresentativeName);
                            AddTableRow(table, "RUT Representante:", led.LegalRepresentativeIdNumber);
                        });
                    }

                    // Beneficiarios
                    if (request.BeneficialOwners.Any())
                    {
                        col.Item().Text("II. BENEFICIARIOS FINALES").Bold().FontSize(11);
                        int i = 1;
                        foreach (var bo in request.BeneficialOwners.OrderBy(b => b.SortOrder))
                        {
                            col.Item().Text($"Beneficiario {i++}:").Bold();
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                                AddTableRow(table, "RUT/ID:", bo.IdNumber);
                                AddTableRow(table, "Nombre:", bo.FullName);
                                AddTableRow(table, "País:", bo.Country);
                                AddTableRow(table, "Participación %:", bo.ParticipationPercentage.ToString("F2") + "%");
                                AddTableRow(table, "Es PEP:", bo.IsPEP ? "Sí" : "No");
                                if (bo.IsPEP && !string.IsNullOrEmpty(bo.PepDetail))
                                    AddTableRow(table, "Detalle PEP:", bo.PepDetail);
                            });
                        }
                    }

                    // Declarante
                    if (request.Declarant is { } dec)
                    {
                        col.Item().Text("III. DECLARANTE").Bold().FontSize(11);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                            AddTableRow(table, "Nombre:", dec.FullName);
                            AddTableRow(table, "RUT/ID:", dec.IdNumber);
                            AddTableRow(table, "Cargo/Relación:", dec.RelationshipWithLegalEntity);
                            AddTableRow(table, "Ciudad:", dec.City);
                            AddTableRow(table, "Fecha:", dec.DeclarationDate.ToString("dd/MM/yyyy"));
                        });
                    }

                    // Firma
                    if (request.PepDeclaration is { } pep)
                    {
                        col.Item().PaddingTop(1, Unit.Centimetre).Column(sig =>
                        {
                            sig.Item().Text("FIRMA ELECTRÓNICA SIMPLE").Bold();
                            sig.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                                AddTableRow(table, "Nombre:", pep.SignatureFullName);
                                AddTableRow(table, "RUT:", pep.SignatureIdNumber);
                                AddTableRow(table, "Fecha/Hora:", pep.SignatureDateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                                AddTableRow(table, "IP:", pep.SignatureIpAddress);
                            });
                        });
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Documento generado el ");
                    x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).Bold();
                    x.Span(" — Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        }).GeneratePdf();
    }

    public byte[] GeneratePepDeclarationPdf(Request request)
    {
        return QuestDocument.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("DECLARACIÓN DE VÍNCULO CON PERSONAS EXPUESTAS POLÍTICAMENTE (PEP)")
                        .Bold().FontSize(13);
                    col.Item().AlignCenter().Text($"Folio: {request.RequestNumber}").FontSize(11);
                    col.Item().LineHorizontal(1);
                });

                page.Content().PaddingTop(0.5f, Unit.Centimetre).Column(col =>
                {
                    col.Spacing(8);

                    col.Item().Background(Colors.Grey.Lighten3).Padding(8).Text(
                        "Se entenderá como PEP a chilenos o extranjeros que desempeñen o hayan desempeñado funciones públicas " +
                        "destacadas, incluyendo jefes de Estado o de gobierno, políticos de alta jerarquía, funcionarios " +
                        "gubernamentales, judiciales o militares, altos ejecutivos de empresas estatales, así como cónyuge, " +
                        "conviviente civil, parientes hasta segundo grado de consanguinidad o afinidad y personas naturales " +
                        "con pacto de actuación conjunta.")
                        .Italic().FontSize(9);

                    if (request.PepDeclaration is { } pep)
                    {
                        col.Item().Text("I. DATOS DEL DECLARANTE").Bold().FontSize(11);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                            AddTableRow(table, "Nombre:", pep.DeclarantName);
                            AddTableRow(table, "RUT/ID:", pep.IdNumber);
                            AddTableRow(table, "Nacionalidad:", pep.Nationality);
                        });

                        col.Item().Text("II. DECLARACIÓN PEP").Bold().FontSize(11);
                        col.Item().Text(pep.DeclaresPEP
                            ? "☑ DECLARA SER O TENER VÍNCULO CON PERSONA EXPUESTA POLÍTICAMENTE"
                            : "☑ DECLARA NO SER PERSONA EXPUESTA POLÍTICAMENTE")
                            .Bold();

                        if (pep.DeclaresPEP)
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                                AddTableRow(table, "Nombre PEP:", pep.PepName ?? "");
                                AddTableRow(table, "Institución:", pep.Institution ?? "");
                                AddTableRow(table, "Motivo PEP:", pep.PepReasonType?.ToDisplayString() ?? "");
                                AddTableRow(table, "Tipo de Vínculo:", pep.VinculoType ?? "");
                            });
                        }

                        col.Item().Text("III. FIRMA ELECTRÓNICA SIMPLE").Bold().FontSize(11);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
                            AddTableRow(table, "Nombre:", pep.SignatureFullName);
                            AddTableRow(table, "RUT:", pep.SignatureIdNumber);
                            AddTableRow(table, "Fecha/Hora:", pep.SignatureDateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                            AddTableRow(table, "Dirección IP:", pep.SignatureIpAddress);
                        });
                        col.Item().Text("Declaro bajo juramento que la información proporcionada es veraz y fidedigna.")
                            .Italic();
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Generado el ");
                    x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).Bold();
                    x.Span(" — Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        }).GeneratePdf();
    }

    public byte[] GenerateConsolidatedPdf(Request request)
    {
        var doc1 = GenerateBeneficialOwnerDeclarationPdf(request);
        var doc2 = GeneratePepDeclarationPdf(request);

        // Concatenate both PDFs using QuestPDF merge
        return QuestDocument.Merge(
            QuestDocument.Create(c => c.Page(p =>
            {
                p.Size(PageSizes.A4);
                p.Margin(2, Unit.Centimetre);
                p.Content().Text("Expediente Consolidado").Bold().FontSize(16);
            }))
        ).GeneratePdf();
    }

    private static void AddTableRow(TableDescriptor table, string label, string value)
    {
        table.Cell().Background(Colors.Grey.Lighten4).Padding(4).Text(label).Bold();
        table.Cell().Padding(4).Text(value);
    }
}
