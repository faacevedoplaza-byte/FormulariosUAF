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
        return QuestDocument.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().AlignCenter().Text("FORMULARIO DE DECLARACIÓN JURADA")
                        .Bold().FontSize(15);
                    col.Item().AlignCenter().Text("CONOCIMIENTO DEL CLIENTE — PERSONA JURÍDICA")
                        .Bold().FontSize(12);
                    col.Item().AlignCenter().Text($"Folio N° {request.RequestNumber} | {request.RequestType.ToDisplayString()}")
                        .FontSize(10).FontColor(Colors.Grey.Darken1);
                    col.Item().LineHorizontal(1).LineColor(Colors.Blue.Darken2);
                    col.Item().Height(4);
                });

                page.Content().PaddingTop(0.3f, Unit.Centimetre).Column(col =>
                {
                    col.Spacing(10);

                    var led = request.LegalEntityDeclaration;

                    // I. Antecedentes persona jurídica
                    col.Item().SectionHeader("I. ANTECEDENTES DE LA PERSONA JURÍDICA");
                    if (led is not null)
                    {
                        col.Item().InfoTable(table =>
                        {
                            AddRow(table, "RUT:", led.RUT);
                            AddRow(table, "Razón Social:", led.BusinessName);
                            AddRow(table, "Tipo de Entidad:", led.EntityType.ToDisplayString());
                            AddRow(table, "Domicilio:", led.Address);
                            AddRow(table, "Ciudad:", led.City);
                            AddRow(table, "País de Constitución:", led.CountryOfIncorporation);
                            AddRow(table, "Teléfono:", led.Phone ?? "—");
                            AddRow(table, "Tipo de Solicitud:", request.RequestType.ToDisplayString());
                        });
                    }

                    // II. Representante legal
                    col.Item().SectionHeader("II. REPRESENTANTE LEGAL");
                    if (led is not null)
                    {
                        col.Item().InfoTable(table =>
                        {
                            AddRow(table, "Nombre:", led.LegalRepresentativeName);
                            AddRow(table, "RUT / N° ID:", led.LegalRepresentativeIdNumber);
                        });
                    }

                    // III. Beneficiarios finales y participación accionaria
                    var beneficiarios = request.DeclaredPersons
                        .Where(p => p.HasMinTenPercentParticipation || p.RelationshipType == Models.Enums.RelationshipType.BeneficiarioFinal
                                    || p.RelationshipType == Models.Enums.RelationshipType.Socio
                                    || p.RelationshipType == Models.Enums.RelationshipType.Accionista)
                        .OrderBy(p => p.SortOrder).ToList();
                    col.Item().SectionHeader("III. BENEFICIARIOS FINALES Y PARTICIPACIÓN ACCIONARIA");
                    if (beneficiarios.Any())
                    {
                        int idx = 1;
                        foreach (var p in beneficiarios)
                        {
                            col.Item().Text($"Beneficiario {idx++}:").Bold().FontSize(9);
                            col.Item().InfoTable(table =>
                            {
                                AddRow(table, "RUT / N° ID:", p.IdNumber);
                                AddRow(table, "Nombre completo:", p.FullName);
                                AddRow(table, "Domicilio:", p.Address ?? "—");
                                AddRow(table, "Ciudad:", p.City ?? "—");
                                AddRow(table, "País:", p.Country);
                                AddRow(table, "Participación:", $"{p.ParticipationPercentage:F2}%");
                                AddRow(table, "Tipo de relación:", p.RelationshipType.ToDisplayString());
                                AddRow(table, "≥10% participación:", p.HasMinTenPercentParticipation ? "Sí" : "No");
                            });
                        }
                    }
                    else
                    {
                        col.Item().Text("No se declararon beneficiarios finales con ≥10% de participación.").Italic().FontColor(Colors.Grey.Medium);
                    }

                    // IV. Controladores efectivos
                    var controladores = request.DeclaredPersons.Where(p => p.IsEffectiveController).OrderBy(p => p.SortOrder).ToList();
                    col.Item().SectionHeader("IV. CONTROLADORES EFECTIVOS");
                    if (controladores.Any())
                    {
                        int idx = 1;
                        foreach (var p in controladores)
                        {
                            col.Item().Text($"Controlador {idx++}:").Bold().FontSize(9);
                            col.Item().InfoTable(table =>
                            {
                                AddRow(table, "RUT / N° ID:", p.IdNumber);
                                AddRow(table, "Nombre completo:", p.FullName);
                                AddRow(table, "Descripción del control:", p.EffectiveControlDescription ?? "—");
                                AddRow(table, "Maneja fondos:", p.HandlesCashOrFunds ? "Sí" : "No");
                            });
                        }
                    }
                    else
                    {
                        col.Item().Text("No se declararon controladores efectivos.").Italic().FontColor(Colors.Grey.Medium);
                    }

                    // V. Declaración PEP
                    var peps = request.DeclaredPersons.Where(p => p.IsPEP).OrderBy(p => p.SortOrder).ToList();
                    col.Item().SectionHeader("V. DECLARACIÓN DE PERSONAS EXPUESTAS POLÍTICAMENTE (PEP)");

                    col.Item().Background(Colors.Grey.Lighten4).Padding(6).Text(
                        "Se consideran PEP quienes desempeñan o han desempeñado funciones públicas destacadas en Chile o en el extranjero, " +
                        "incluyendo jefes de Estado, políticos de alta jerarquía, funcionarios gubernamentales, judiciales o militares, " +
                        "sus parientes hasta 2° grado y personas con pactos de actuación conjunta.")
                        .Italic().FontSize(8.5f);

                    if (peps.Any())
                    {
                        int idx = 1;
                        foreach (var p in peps)
                        {
                            col.Item().Text($"PEP {idx++}: {p.FullName}").Bold().FontSize(9);
                            col.Item().InfoTable(table =>
                            {
                                AddRow(table, "RUT / N° ID:", p.IdNumber);
                                AddRow(table, "Tipo de vínculo PEP:", p.PepType?.ToDisplayString() ?? "—");
                                AddRow(table, "Institución:", p.PepInstitution ?? "—");
                                AddRow(table, "Cargo:", p.PepPosition ?? "—");
                                AddRow(table, "Nombre del PEP:", p.PepTypeName ?? "—");
                                AddRow(table, "Relación:", p.PepRelationship ?? "—");
                                if (!string.IsNullOrEmpty(p.PepObservation))
                                    AddRow(table, "Observación:", p.PepObservation);
                            });
                        }
                    }
                    else
                    {
                        col.Item().Text("☑ Ninguna persona declarada tiene vínculo con PEP.").Bold();
                    }

                    // VI. Personas adicionales con relación
                    var otros = request.DeclaredPersons
                        .Where(p => !p.HasMinTenPercentParticipation && !p.IsEffectiveController
                            && p.RelationshipType != Models.Enums.RelationshipType.BeneficiarioFinal
                            && p.RelationshipType != Models.Enums.RelationshipType.Socio
                            && p.RelationshipType != Models.Enums.RelationshipType.Accionista)
                        .OrderBy(p => p.SortOrder).ToList();
                    if (otros.Any())
                    {
                        col.Item().SectionHeader("VI. OTRAS PERSONAS RELACIONADAS");
                        int idx = 1;
                        foreach (var p in otros)
                        {
                            col.Item().InfoTable(table =>
                            {
                                AddRow(table, $"Persona {idx++}:", p.FullName);
                                AddRow(table, "RUT / N° ID:", p.IdNumber);
                                AddRow(table, "Tipo de relación:", p.RelationshipType.ToDisplayString());
                            });
                        }
                    }

                    // VII. Declarante (firmante)
                    col.Item().SectionHeader("VII. DECLARANTE");
                    if (request.Declarant is { } dec)
                    {
                        col.Item().InfoTable(table =>
                        {
                            AddRow(table, "Nombre completo:", dec.FullName);
                            AddRow(table, "RUT / N° ID:", dec.IdNumber);
                            AddRow(table, "Cargo / Relación:", dec.RelationshipWithLegalEntity);
                            AddRow(table, "Nacionalidad:", dec.NationalityType.ToDisplayString());
                            AddRow(table, "Ciudad:", dec.City);
                            AddRow(table, "Correo electrónico:", dec.Email ?? "—");
                            AddRow(table, "Teléfono:", dec.Phone ?? "—");
                            AddRow(table, "Fecha de declaración:", dec.DeclarationDate.ToString("dd/MM/yyyy"));
                        });
                    }

                    // VIII. Firma electrónica simple
                    col.Item().SectionHeader("VIII. FIRMA ELECTRÓNICA SIMPLE");
                    if (request.Declarant is { SignatureFullName: not null } decFirma)
                    {
                        col.Item().InfoTable(table =>
                        {
                            AddRow(table, "Nombre firmante:", decFirma.SignatureFullName ?? "—");
                            AddRow(table, "RUT firmante:", decFirma.SignatureIdNumber ?? "—");
                            AddRow(table, "Fecha y hora:", decFirma.SignatureDateTime?.ToString("dd/MM/yyyy HH:mm:ss") ?? "—");
                            AddRow(table, "Dirección IP:", decFirma.SignatureIpAddress ?? "—");
                        });
                        col.Item().Text("El declarante manifiesta bajo juramento que la información proporcionada es veraz, fidedigna y completa.")
                            .Italic().FontSize(9);
                    }

                    // IX. Documentos adjuntos
                    col.Item().SectionHeader("IX. DOCUMENTOS ADJUNTOS");
                    if (request.Documents.Any())
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(3); c.RelativeColumn(4); c.RelativeColumn(2); });
                            table.Header(h =>
                            {
                                h.Cell().Background(Colors.Blue.Darken2).Padding(4).Text("Tipo").Bold().FontColor(Colors.White).FontSize(9);
                                h.Cell().Background(Colors.Blue.Darken2).Padding(4).Text("Archivo").Bold().FontColor(Colors.White).FontSize(9);
                                h.Cell().Background(Colors.Blue.Darken2).Padding(4).Text("Fecha").Bold().FontColor(Colors.White).FontSize(9);
                            });
                            foreach (var doc in request.Documents)
                            {
                                table.Cell().BorderBottom(0.5f).Padding(4).Text(doc.DocumentType.ToDisplayString()).FontSize(9);
                                table.Cell().BorderBottom(0.5f).Padding(4).Text(doc.OriginalFileName).FontSize(9);
                                table.Cell().BorderBottom(0.5f).Padding(4).Text(doc.UploadedAt.ToString("dd/MM/yyyy")).FontSize(9);
                            }
                        });
                    }

                    // X. Uso del expediente
                    col.Item().SectionHeader("X. DESTINO Y USO DE LA INFORMACIÓN");
                    col.Item().Background(Colors.Grey.Lighten4).Padding(8).Text(
                        "La información contenida en este formulario es de carácter confidencial y será utilizada exclusivamente " +
                        "para dar cumplimiento a las obligaciones legales de la empresa en materia de Prevención del Lavado de Activos " +
                        "y Financiamiento del Terrorismo (AML/CFT), según lo dispuesto en la Ley N° 19.913 y sus modificaciones, " +
                        "así como las instrucciones emitidas por la Unidad de Análisis Financiero (UAF) de Chile.")
                        .FontSize(8.5f).Italic();
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Documento generado el ").FontSize(8);
                    x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).Bold().FontSize(8);
                    x.Span(" — Folio: ").FontSize(8);
                    x.Span(request.RequestNumber).Bold().FontSize(8);
                    x.Span(" — Pág. ").FontSize(8);
                    x.CurrentPageNumber().FontSize(8);
                    x.Span(" de ").FontSize(8);
                    x.TotalPages().FontSize(8);
                });
            });
        }).GeneratePdf();
    }

    private static void AddTableRow(TableDescriptor table, string label, string value)
    {
        table.Cell().Background(Colors.Grey.Lighten4).Padding(4).Text(label).Bold();
        table.Cell().Padding(4).Text(value);
    }

    // Alias used in GenerateConsolidatedPdf
    private static void AddRow(TableDescriptor table, string label, string value)
        => AddTableRow(table, label, value);
}

internal static class PdfFluentExtensions
{
    internal static void SectionHeader(this IContainer container, string title)
    {
        container.Background(Colors.Blue.Darken2).Padding(6).Text(t =>
        {
            t.Span(title).Bold().FontColor(Colors.White).FontSize(11);
        });
    }

    internal static void InfoTable(this IContainer container, Action<TableDescriptor> configure)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); });
            configure(table);
        });
    }
}
