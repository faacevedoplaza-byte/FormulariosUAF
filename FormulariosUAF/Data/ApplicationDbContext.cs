using FormulariosUAF.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormulariosUAF.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Módulo core
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<LegalEntityDeclaration> LegalEntityDeclarations => Set<LegalEntityDeclaration>();
    public DbSet<BeneficialOwner> BeneficialOwners => Set<BeneficialOwner>();
    public DbSet<EffectiveController> EffectiveControllers => Set<EffectiveController>();
    public DbSet<PepDeclaration> PepDeclarations => Set<PepDeclaration>();
    public DbSet<Declarant> Declarants => Set<Declarant>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<FileStorageRecord> FileStorageRecords => Set<FileStorageRecord>();
    public DbSet<RequestStatusHistory> RequestStatusHistories => Set<RequestStatusHistory>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    // Flujo simplificado
    public DbSet<DeclaredPerson> DeclaredPersons => Set<DeclaredPerson>();
    public DbSet<TaxFolderAnalysis> TaxFolderAnalyses => Set<TaxFolderAnalysis>();
    public DbSet<TaxFolderAlert> TaxFolderAlerts => Set<TaxFolderAlert>();

    // Fase 2 — Firma + Notificaciones
    public DbSet<SignatureRecord> SignatureRecords => Set<SignatureRecord>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Client>(e =>
        {
            e.HasIndex(c => c.RUT).IsUnique();
            e.Property(c => c.RUT).HasMaxLength(20);
            e.Property(c => c.BusinessName).HasMaxLength(500);
        });

        builder.Entity<Request>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasIndex(r => r.ClientToken).IsUnique();
            e.HasIndex(r => r.RequestNumber).IsUnique();
            e.HasIndex(r => r.Status);
            e.HasIndex(r => r.CreatedAt);
            e.HasQueryFilter(r => !r.IsDeleted);
            e.Property(r => r.RequestNumber).HasMaxLength(50);
            e.Property(r => r.ClientToken).HasMaxLength(200);
            e.HasOne(r => r.Client)
                .WithMany(c => c.Requests)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.VendorUser)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.VendorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LegalEntityDeclaration>(e =>
        {
            e.HasOne(l => l.Request)
                .WithOne(r => r.LegalEntityDeclaration)
                .HasForeignKey<LegalEntityDeclaration>(l => l.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<BeneficialOwner>(e =>
        {
            e.Property(b => b.ParticipationPercentage).HasPrecision(5, 2);
            e.HasOne(b => b.Request)
                .WithMany(r => r.BeneficialOwners)
                .HasForeignKey(b => b.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<EffectiveController>(e =>
        {
            e.Property(ec => ec.ParticipationPercentage).HasPrecision(5, 2);
            e.HasOne(ec => ec.Request)
                .WithMany(r => r.EffectiveControllers)
                .HasForeignKey(ec => ec.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PepDeclaration>(e =>
        {
            e.HasOne(p => p.Request)
                .WithOne(r => r.PepDeclaration)
                .HasForeignKey<PepDeclaration>(p => p.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Declarant>(e =>
        {
            e.HasOne(d => d.Request)
                .WithOne(r => r.Declarant)
                .HasForeignKey<Declarant>(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Document>(e =>
        {
            e.HasOne(d => d.Request)
                .WithMany(r => r.Documents)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FileStorageRecord>(e =>
        {
            e.HasOne(f => f.Document)
                .WithMany(d => d.StorageRecords)
                .HasForeignKey(f => f.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RequestStatusHistory>(e =>
        {
            e.HasOne(h => h.Request)
                .WithMany(r => r.StatusHistory)
                .HasForeignKey(h => h.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<AuditLog>(e =>
        {
            e.HasIndex(a => a.CreatedAt);
            e.HasIndex(a => a.RequestId);
            e.HasOne(a => a.Request)
                .WithMany(r => r.AuditLogs)
                .HasForeignKey(a => a.RequestId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<SignatureRecord>(e =>
        {
            e.HasIndex(s => s.RequestId);
            e.HasOne(s => s.Request)
                .WithMany()
                .HasForeignKey(s => s.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Notification>(e =>
        {
            e.HasIndex(n => n.UserId);
            e.HasIndex(n => new { n.UserId, n.IsRead });
            e.HasIndex(n => n.CreatedAt);
        });

        builder.Entity<DeclaredPerson>(e =>
        {
            e.Property(d => d.ParticipationPercentage).HasPrecision(5, 2);
            e.HasIndex(d => d.RequestId);
            e.HasOne(d => d.Request)
                .WithMany(r => r.DeclaredPersons)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TaxFolderAnalysis>(e =>
        {
            e.HasIndex(t => t.RequestId).IsUnique();
            e.HasOne(t => t.Request)
                .WithOne(r => r.TaxFolderAnalysis)
                .HasForeignKey<TaxFolderAnalysis>(t => t.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<TaxFolderAlert>(e =>
        {
            e.HasOne(a => a.TaxFolderAnalysis)
                .WithMany(t => t.Alerts)
                .HasForeignKey(a => a.TaxFolderAnalysisId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Renombrar tablas Identity a nombres amigables
        builder.Entity<ApplicationUser>().ToTable("Usuarios");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("Roles");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("UsuariosRoles");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("UsuariosClaims");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("UsuariosLogins");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("RolesClaims");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("UsuariosTokens");
    }
}
