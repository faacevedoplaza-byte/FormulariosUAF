IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Clients] (
        [Id] int NOT NULL IDENTITY,
        [RUT] nvarchar(20) NOT NULL,
        [BusinessName] nvarchar(500) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] nvarchar(450) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [VendorCode] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [LastLoginAt] datetime2 NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [RolesClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RolesClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RolesClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Requests] (
        [Id] uniqueidentifier NOT NULL,
        [RequestNumber] nvarchar(50) NOT NULL,
        [ClientId] int NOT NULL,
        [VendorUserId] nvarchar(450) NOT NULL,
        [RequestType] int NOT NULL,
        [Status] int NOT NULL,
        [ClientToken] nvarchar(200) NOT NULL,
        [TokenExpiry] datetime2 NOT NULL,
        [InternalNotes] nvarchar(max) NULL,
        [CurrentStep] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] nvarchar(max) NULL,
        [SentAt] datetime2 NULL,
        [OpenedAt] datetime2 NULL,
        [CompletedAt] datetime2 NULL,
        [DueDate] datetime2 NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Requests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Requests_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Requests_Usuarios_VendorUserId] FOREIGN KEY ([VendorUserId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [UsuariosClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UsuariosClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UsuariosClaims_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [UsuariosLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UsuariosLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UsuariosLogins_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [UsuariosRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UsuariosRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsuariosRoles_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [UsuariosTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UsuariosTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UsuariosTokens_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [AuditLogs] (
        [Id] bigint NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NULL,
        [Action] nvarchar(max) NOT NULL,
        [EntityType] nvarchar(max) NOT NULL,
        [EntityId] nvarchar(max) NULL,
        [OldValues] nvarchar(max) NULL,
        [NewValues] nvarchar(max) NULL,
        [UserId] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [IpAddress] nvarchar(max) NOT NULL,
        [UserAgent] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AuditLogs_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE SET NULL
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [BeneficialOwners] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [IdNumber] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Country] nvarchar(max) NOT NULL,
        [ParticipationPercentage] decimal(5,2) NOT NULL,
        [IsBeneficialOwner] bit NOT NULL,
        [IsEffectiveControl] bit NOT NULL,
        [IsPEP] bit NOT NULL,
        [PepDetail] nvarchar(max) NULL,
        [SortOrder] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_BeneficialOwners] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BeneficialOwners_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Declarants] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [NationalityType] int NOT NULL,
        [IdNumber] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName1] nvarchar(max) NOT NULL,
        [LastName2] nvarchar(max) NULL,
        [PlaceOfOrigin] nvarchar(max) NULL,
        [RelationshipWithLegalEntity] nvarchar(max) NOT NULL,
        [DeclaresUnderOath] bit NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [DeclarationDate] datetime2 NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Declarants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Declarants_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [Documents] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [DocumentType] int NOT NULL,
        [DocumentTypeOther] nvarchar(max) NULL,
        [OriginalFileName] nvarchar(max) NOT NULL,
        [StoredFileName] nvarchar(max) NOT NULL,
        [StoragePath] nvarchar(max) NOT NULL,
        [MimeType] nvarchar(max) NOT NULL,
        [FileSizeBytes] bigint NOT NULL,
        [IsActive] bit NOT NULL,
        [Version] int NOT NULL,
        [StorageProvider] int NOT NULL,
        [UploadedAt] datetime2 NOT NULL,
        [UploadedByIp] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Documents] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Documents_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [EffectiveControllers] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [IdNumber] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Country] nvarchar(max) NOT NULL,
        [ParticipationPercentage] decimal(5,2) NULL,
        [IsPEP] bit NOT NULL,
        [PepDetail] nvarchar(max) NULL,
        [ControlDescription] nvarchar(max) NOT NULL,
        [SortOrder] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_EffectiveControllers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EffectiveControllers_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [LegalEntityDeclarations] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [RUT] nvarchar(max) NOT NULL,
        [BusinessName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [CountryOfIncorporation] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NULL,
        [LegalRepresentativeIdNumber] nvarchar(max) NOT NULL,
        [LegalRepresentativeName] nvarchar(max) NOT NULL,
        [EntityType] int NOT NULL,
        [EntityTypeOther] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_LegalEntityDeclarations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LegalEntityDeclarations_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [PepDeclarations] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [DeclarantName] nvarchar(max) NOT NULL,
        [IdNumber] nvarchar(max) NOT NULL,
        [Nationality] nvarchar(max) NOT NULL,
        [DeclaresPEP] bit NOT NULL,
        [PepName] nvarchar(max) NULL,
        [Institution] nvarchar(max) NULL,
        [PepReasonType] int NULL,
        [PepReasonOther] nvarchar(max) NULL,
        [VinculoType] nvarchar(max) NULL,
        [DeclarationDate] datetime2 NOT NULL,
        [AcceptsUnderOath] bit NOT NULL,
        [SignatureFullName] nvarchar(max) NOT NULL,
        [SignatureIdNumber] nvarchar(max) NOT NULL,
        [SignatureDateTime] datetime2 NOT NULL,
        [SignatureIpAddress] nvarchar(max) NOT NULL,
        [SignatureUserAgent] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_PepDeclarations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PepDeclarations_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [RequestStatusHistories] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [OldStatus] int NOT NULL,
        [NewStatus] int NOT NULL,
        [ChangedBy] nvarchar(max) NOT NULL,
        [Notes] nvarchar(max) NULL,
        [ChangedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_RequestStatusHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RequestStatusHistories_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE TABLE [FileStorageRecords] (
        [Id] int NOT NULL IDENTITY,
        [DocumentId] int NOT NULL,
        [StorageProvider] int NOT NULL,
        [StoragePath] nvarchar(max) NOT NULL,
        [StorageKey] nvarchar(max) NOT NULL,
        [UploadedAt] datetime2 NOT NULL,
        [Metadata] nvarchar(max) NULL,
        CONSTRAINT [PK_FileStorageRecords] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FileStorageRecords_Documents_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Documents] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_CreatedAt] ON [AuditLogs] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AuditLogs_RequestId] ON [AuditLogs] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_BeneficialOwners_RequestId] ON [BeneficialOwners] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Clients_RUT] ON [Clients] ([RUT]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Declarants_RequestId] ON [Declarants] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Documents_RequestId] ON [Documents] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_EffectiveControllers_RequestId] ON [EffectiveControllers] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_FileStorageRecords_DocumentId] ON [FileStorageRecords] ([DocumentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_LegalEntityDeclarations_RequestId] ON [LegalEntityDeclarations] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_PepDeclarations_RequestId] ON [PepDeclarations] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Requests_ClientId] ON [Requests] ([ClientId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Requests_ClientToken] ON [Requests] ([ClientToken]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Requests_CreatedAt] ON [Requests] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Requests_RequestNumber] ON [Requests] ([RequestNumber]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Requests_Status] ON [Requests] ([Status]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Requests_VendorUserId] ON [Requests] ([VendorUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_RequestStatusHistories_RequestId] ON [RequestStatusHistories] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_RolesClaims_RoleId] ON [RolesClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Usuarios] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Usuarios] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UsuariosClaims_UserId] ON [UsuariosClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UsuariosLogins_UserId] ON [UsuariosLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UsuariosRoles_RoleId] ON [UsuariosRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260604221419_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260604221419_InitialCreate', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [Type] int NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [RequestId] uniqueidentifier NULL,
        [IsRead] bit NOT NULL,
        [ReadAt] datetime2 NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE TABLE [SignatureRecords] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [Type] int NOT NULL,
        [Status] int NOT NULL,
        [SignerName] nvarchar(max) NOT NULL,
        [SignerIdNumber] nvarchar(max) NOT NULL,
        [SignedAt] datetime2 NULL,
        [IpAddress] nvarchar(max) NULL,
        [UserAgent] nvarchar(max) NULL,
        [DocumentHash] nvarchar(max) NULL,
        [FolioNumber] nvarchar(max) NOT NULL,
        [ProviderTransactionId] nvarchar(max) NULL,
        [SignatureUrl] nvarchar(max) NULL,
        [Certificate] nvarchar(max) NULL,
        [SignedPdfPath] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_SignatureRecords] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SignatureRecords_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE INDEX [IX_Notifications_CreatedAt] ON [Notifications] ([CreatedAt]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE INDEX [IX_Notifications_UserId_IsRead] ON [Notifications] ([UserId], [IsRead]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    CREATE INDEX [IX_SignatureRecords_RequestId] ON [SignatureRecords] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260605215032_AddNotificationsAndSignatures'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260605215032_AddNotificationsAndSignatures', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Requests] ADD [ClientEmail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Requests] ADD [ClientPhone] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [Email] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [Phone] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [SignatureDateTime] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [SignatureFullName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [SignatureIdNumber] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [SignatureIpAddress] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    ALTER TABLE [Declarants] ADD [SignatureUserAgent] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE TABLE [DeclaredPersons] (
        [Id] uniqueidentifier NOT NULL,
        [RequestId] uniqueidentifier NOT NULL,
        [IdNumber] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Country] nvarchar(max) NOT NULL,
        [ParticipationPercentage] decimal(5,2) NOT NULL,
        [RelationshipType] int NOT NULL,
        [RelationshipTypeOther] nvarchar(max) NULL,
        [HasMinTenPercentParticipation] bit NOT NULL,
        [IsEffectiveController] bit NOT NULL,
        [EffectiveControlDescription] nvarchar(max) NULL,
        [HandlesCashOrFunds] bit NOT NULL,
        [IsPEP] bit NOT NULL,
        [PepType] int NULL,
        [PepTypeName] nvarchar(max) NULL,
        [PepInstitution] nvarchar(max) NULL,
        [PepPosition] nvarchar(max) NULL,
        [PepRelationship] nvarchar(max) NULL,
        [PepObservation] nvarchar(max) NULL,
        [SortOrder] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_DeclaredPersons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DeclaredPersons_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE TABLE [TaxFolderAnalyses] (
        [Id] int NOT NULL IDENTITY,
        [RequestId] uniqueidentifier NOT NULL,
        [ExtractedRut] nvarchar(max) NULL,
        [ExtractedBusinessName] nvarchar(max) NULL,
        [ExtractedAddress] nvarchar(max) NULL,
        [ExtractedActivity] nvarchar(max) NULL,
        [ExtractedLegalRep] nvarchar(max) NULL,
        [ExtractedIssuedDate] nvarchar(max) NULL,
        [RawText] nvarchar(max) NULL,
        [WasReadable] bit NOT NULL,
        [AnalyzedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_TaxFolderAnalyses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaxFolderAnalyses_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE TABLE [TaxFolderAlerts] (
        [Id] int NOT NULL IDENTITY,
        [TaxFolderAnalysisId] int NOT NULL,
        [AlertType] int NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [Severity] nvarchar(max) NOT NULL,
        [IsResolved] bit NOT NULL,
        CONSTRAINT [PK_TaxFolderAlerts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TaxFolderAlerts_TaxFolderAnalyses_TaxFolderAnalysisId] FOREIGN KEY ([TaxFolderAnalysisId]) REFERENCES [TaxFolderAnalyses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE INDEX [IX_DeclaredPersons_RequestId] ON [DeclaredPersons] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE INDEX [IX_TaxFolderAlerts_TaxFolderAnalysisId] ON [TaxFolderAlerts] ([TaxFolderAnalysisId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    CREATE UNIQUE INDEX [IX_TaxFolderAnalyses_RequestId] ON [TaxFolderAnalyses] ([RequestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260608192216_SimplifiedClientFlow'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260608192216_SimplifiedClientFlow', N'8.0.10');
END;
GO

COMMIT;
GO

