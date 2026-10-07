-- ============================================================================
-- 02_Validacion_ConvencionT.sql  (solo lectura: SELECTs, no modifica nada)
-- Ejecutar DESPUÉS de 01_Renombrado_ConvencionT.sql. Cada sección debe devolver
-- 0 filas en las consultas marcadas "debe ser vacío".
-- ============================================================================
SET NOCOUNT ON;

DECLARE @Tablas TABLE (Nueva sysname, Original sysname);
INSERT @Tablas VALUES
(N'T_SOLICITUD', N'Requests'),
(N'T_CLIENTE', N'Clients'),
(N'T_USUARIO', N'Usuarios'),
(N'T_ROL', N'Roles'),
(N'T_USUARIOROL', N'UsuariosRoles'),
(N'T_USUARIOCLAIM', N'UsuariosClaims'),
(N'T_USUARIOLOGIN', N'UsuariosLogins'),
(N'T_USUARIOTOKEN', N'UsuariosTokens'),
(N'T_ROLCLAIM', N'RolesClaims'),
(N'T_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations'),
(N'T_PERSONADECLARADA', N'DeclaredPersons'),
(N'T_DECLARANTE', N'Declarants'),
(N'T_DOCUMENTO', N'Documents'),
(N'T_ALMACENAMIENTOARCHIVO', N'FileStorageRecords'),
(N'T_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories'),
(N'T_AUDITORIA', N'AuditLogs'),
(N'T_NOTIFICACION', N'Notifications'),
(N'T_FIRMA', N'SignatureRecords'),
(N'T_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses'),
(N'T_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts'),
(N'T_BENEFICIARIOFINAL', N'BeneficialOwners'),
(N'T_CONTROLADOREFECTIVO', N'EffectiveControllers'),
(N'T_DECLARACIONPEP', N'PepDeclarations');

DECLARE @Columnas TABLE (Tabla sysname, Nueva sysname, TablaOriginal sysname, Original sysname);
INSERT @Columnas VALUES
(N'T_AUDITORIA', N'IN_COD_AUDITORIA', N'AuditLogs', N'Id'),
(N'T_AUDITORIA', N'IN_COD_SOLICITUD', N'AuditLogs', N'RequestId'),
(N'T_AUDITORIA', N'ST_ACCION_AUDITORIA', N'AuditLogs', N'Action'),
(N'T_AUDITORIA', N'ST_TIPOENTIDAD_AUDITORIA', N'AuditLogs', N'EntityType'),
(N'T_AUDITORIA', N'ST_CODENTIDAD_AUDITORIA', N'AuditLogs', N'EntityId'),
(N'T_AUDITORIA', N'ST_VALORESANTERIORES_AUDITORIA', N'AuditLogs', N'OldValues'),
(N'T_AUDITORIA', N'ST_VALORESNUEVOS_AUDITORIA', N'AuditLogs', N'NewValues'),
(N'T_AUDITORIA', N'IN_COD_USUARIO', N'AuditLogs', N'UserId'),
(N'T_AUDITORIA', N'ST_NOMBREUSUARIO_AUDITORIA', N'AuditLogs', N'UserName'),
(N'T_AUDITORIA', N'ST_DIRECCIONIP_AUDITORIA', N'AuditLogs', N'IpAddress'),
(N'T_AUDITORIA', N'ST_USERAGENT_AUDITORIA', N'AuditLogs', N'UserAgent'),
(N'T_AUDITORIA', N'DT_FECHACREACION_AUDITORIA', N'AuditLogs', N'CreatedAt'),
(N'T_BENEFICIARIOFINAL', N'IN_COD_BENEFICIARIOFINAL', N'BeneficialOwners', N'Id'),
(N'T_BENEFICIARIOFINAL', N'IN_COD_SOLICITUD', N'BeneficialOwners', N'RequestId'),
(N'T_BENEFICIARIOFINAL', N'ST_NUMEROIDENTIFICACION_BENEFICIARIOFINAL', N'BeneficialOwners', N'IdNumber'),
(N'T_BENEFICIARIOFINAL', N'ST_NOMBRECOMPLETO_BENEFICIARIOFINAL', N'BeneficialOwners', N'FullName'),
(N'T_BENEFICIARIOFINAL', N'ST_DIRECCION_BENEFICIARIOFINAL', N'BeneficialOwners', N'Address'),
(N'T_BENEFICIARIOFINAL', N'ST_CIUDAD_BENEFICIARIOFINAL', N'BeneficialOwners', N'City'),
(N'T_BENEFICIARIOFINAL', N'ST_PAIS_BENEFICIARIOFINAL', N'BeneficialOwners', N'Country'),
(N'T_BENEFICIARIOFINAL', N'NU_PORCENTAJEPARTICIPACION_BENEFICIARIOFINAL', N'BeneficialOwners', N'ParticipationPercentage'),
(N'T_BENEFICIARIOFINAL', N'BO_ESBENEFICIARIOFINAL_BENEFICIARIOFINAL', N'BeneficialOwners', N'IsBeneficialOwner'),
(N'T_BENEFICIARIOFINAL', N'BO_ESCONTROLEFECTIVO_BENEFICIARIOFINAL', N'BeneficialOwners', N'IsEffectiveControl'),
(N'T_BENEFICIARIOFINAL', N'BO_ESPEP_BENEFICIARIOFINAL', N'BeneficialOwners', N'IsPEP'),
(N'T_BENEFICIARIOFINAL', N'ST_DETALLEPEP_BENEFICIARIOFINAL', N'BeneficialOwners', N'PepDetail'),
(N'T_BENEFICIARIOFINAL', N'IN_ORDEN_BENEFICIARIOFINAL', N'BeneficialOwners', N'SortOrder'),
(N'T_BENEFICIARIOFINAL', N'DT_FECHACREACION_BENEFICIARIOFINAL', N'BeneficialOwners', N'CreatedAt'),
(N'T_BENEFICIARIOFINAL', N'DT_FECHAMODIFICACION_BENEFICIARIOFINAL', N'BeneficialOwners', N'UpdatedAt'),
(N'T_CLIENTE', N'IN_COD_CLIENTE', N'Clients', N'Id'),
(N'T_CLIENTE', N'ST_RUT_CLIENTE', N'Clients', N'RUT'),
(N'T_CLIENTE', N'ST_RAZONSOCIAL_CLIENTE', N'Clients', N'BusinessName'),
(N'T_CLIENTE', N'BO_ACTIVO_CLIENTE', N'Clients', N'IsActive'),
(N'T_CLIENTE', N'DT_FECHACREACION_CLIENTE', N'Clients', N'CreatedAt'),
(N'T_CLIENTE', N'ST_CREADOPOR_CLIENTE', N'Clients', N'CreatedBy'),
(N'T_CLIENTE', N'DT_FECHAMODIFICACION_CLIENTE', N'Clients', N'UpdatedAt'),
(N'T_CLIENTE', N'ST_MODIFICADOPOR_CLIENTE', N'Clients', N'UpdatedBy'),
(N'T_DECLARANTE', N'IN_COD_DECLARANTE', N'Declarants', N'Id'),
(N'T_DECLARANTE', N'IN_COD_SOLICITUD', N'Declarants', N'RequestId'),
(N'T_DECLARANTE', N'IN_TIPONACIONALIDAD_DECLARANTE', N'Declarants', N'NationalityType'),
(N'T_DECLARANTE', N'ST_NUMEROIDENTIFICACION_DECLARANTE', N'Declarants', N'IdNumber'),
(N'T_DECLARANTE', N'ST_NOMBRE_DECLARANTE', N'Declarants', N'FirstName'),
(N'T_DECLARANTE', N'ST_APELLIDOPATERNO_DECLARANTE', N'Declarants', N'LastName1'),
(N'T_DECLARANTE', N'ST_APELLIDOMATERNO_DECLARANTE', N'Declarants', N'LastName2'),
(N'T_DECLARANTE', N'ST_LUGARORIGEN_DECLARANTE', N'Declarants', N'PlaceOfOrigin'),
(N'T_DECLARANTE', N'ST_RELACIONPERSONAJURIDICA_DECLARANTE', N'Declarants', N'RelationshipWithLegalEntity'),
(N'T_DECLARANTE', N'BO_DECLARABAJOJURAMENTO_DECLARANTE', N'Declarants', N'DeclaresUnderOath'),
(N'T_DECLARANTE', N'ST_CIUDAD_DECLARANTE', N'Declarants', N'City'),
(N'T_DECLARANTE', N'DT_FECHADECLARACION_DECLARANTE', N'Declarants', N'DeclarationDate'),
(N'T_DECLARANTE', N'DT_FECHACREACION_DECLARANTE', N'Declarants', N'CreatedAt'),
(N'T_DECLARANTE', N'DT_FECHAMODIFICACION_DECLARANTE', N'Declarants', N'UpdatedAt'),
(N'T_DECLARANTE', N'ST_EMAIL_DECLARANTE', N'Declarants', N'Email'),
(N'T_DECLARANTE', N'ST_TELEFONO_DECLARANTE', N'Declarants', N'Phone'),
(N'T_DECLARANTE', N'DT_FECHAFIRMA_DECLARANTE', N'Declarants', N'SignatureDateTime'),
(N'T_DECLARANTE', N'ST_NOMBRECOMPLETOFIRMA_DECLARANTE', N'Declarants', N'SignatureFullName'),
(N'T_DECLARANTE', N'ST_NUMEROIDENTIFICACIONFIRMA_DECLARANTE', N'Declarants', N'SignatureIdNumber'),
(N'T_DECLARANTE', N'ST_DIRECCIONIPFIRMA_DECLARANTE', N'Declarants', N'SignatureIpAddress'),
(N'T_DECLARANTE', N'ST_USERAGENTFIRMA_DECLARANTE', N'Declarants', N'SignatureUserAgent'),
(N'T_PERSONADECLARADA', N'IN_COD_PERSONADECLARADA', N'DeclaredPersons', N'Id'),
(N'T_PERSONADECLARADA', N'IN_COD_SOLICITUD', N'DeclaredPersons', N'RequestId'),
(N'T_PERSONADECLARADA', N'ST_NUMEROIDENTIFICACION_PERSONADECLARADA', N'DeclaredPersons', N'IdNumber'),
(N'T_PERSONADECLARADA', N'ST_NOMBRECOMPLETO_PERSONADECLARADA', N'DeclaredPersons', N'FullName'),
(N'T_PERSONADECLARADA', N'ST_DIRECCION_PERSONADECLARADA', N'DeclaredPersons', N'Address'),
(N'T_PERSONADECLARADA', N'ST_CIUDAD_PERSONADECLARADA', N'DeclaredPersons', N'City'),
(N'T_PERSONADECLARADA', N'ST_PAIS_PERSONADECLARADA', N'DeclaredPersons', N'Country'),
(N'T_PERSONADECLARADA', N'NU_PORCENTAJEPARTICIPACION_PERSONADECLARADA', N'DeclaredPersons', N'ParticipationPercentage'),
(N'T_PERSONADECLARADA', N'IN_TIPORELACION_PERSONADECLARADA', N'DeclaredPersons', N'RelationshipType'),
(N'T_PERSONADECLARADA', N'ST_TIPORELACIONOTRO_PERSONADECLARADA', N'DeclaredPersons', N'RelationshipTypeOther'),
(N'T_PERSONADECLARADA', N'BO_PARTICIPACIONMINDIEZPORCIENTO_PERSONADECLARADA', N'DeclaredPersons', N'HasMinTenPercentParticipation'),
(N'T_PERSONADECLARADA', N'BO_ESCONTROLADOREFECTIVO_PERSONADECLARADA', N'DeclaredPersons', N'IsEffectiveController'),
(N'T_PERSONADECLARADA', N'ST_DESCRIPCIONCONTROLEFECTIVO_PERSONADECLARADA', N'DeclaredPersons', N'EffectiveControlDescription'),
(N'T_PERSONADECLARADA', N'BO_MANEJAEFECTIVOFONDOS_PERSONADECLARADA', N'DeclaredPersons', N'HandlesCashOrFunds'),
(N'T_PERSONADECLARADA', N'BO_ESPEP_PERSONADECLARADA', N'DeclaredPersons', N'IsPEP'),
(N'T_PERSONADECLARADA', N'IN_TIPOPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepType'),
(N'T_PERSONADECLARADA', N'ST_NOMBRETIPOPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepTypeName'),
(N'T_PERSONADECLARADA', N'ST_INSTITUCIONPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepInstitution'),
(N'T_PERSONADECLARADA', N'ST_CARGOPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepPosition'),
(N'T_PERSONADECLARADA', N'ST_RELACIONPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepRelationship'),
(N'T_PERSONADECLARADA', N'ST_OBSERVACIONPEP_PERSONADECLARADA', N'DeclaredPersons', N'PepObservation'),
(N'T_PERSONADECLARADA', N'IN_ORDEN_PERSONADECLARADA', N'DeclaredPersons', N'SortOrder'),
(N'T_PERSONADECLARADA', N'DT_FECHACREACION_PERSONADECLARADA', N'DeclaredPersons', N'CreatedAt'),
(N'T_PERSONADECLARADA', N'DT_FECHAMODIFICACION_PERSONADECLARADA', N'DeclaredPersons', N'UpdatedAt'),
(N'T_DOCUMENTO', N'IN_COD_DOCUMENTO', N'Documents', N'Id'),
(N'T_DOCUMENTO', N'IN_COD_SOLICITUD', N'Documents', N'RequestId'),
(N'T_DOCUMENTO', N'IN_TIPO_DOCUMENTO', N'Documents', N'DocumentType'),
(N'T_DOCUMENTO', N'ST_TIPOOTRO_DOCUMENTO', N'Documents', N'DocumentTypeOther'),
(N'T_DOCUMENTO', N'ST_NOMBREARCHIVOORIGINAL_DOCUMENTO', N'Documents', N'OriginalFileName'),
(N'T_DOCUMENTO', N'ST_NOMBREARCHIVOALMACENADO_DOCUMENTO', N'Documents', N'StoredFileName'),
(N'T_DOCUMENTO', N'ST_RUTAALMACENAMIENTO_DOCUMENTO', N'Documents', N'StoragePath'),
(N'T_DOCUMENTO', N'ST_TIPOMIME_DOCUMENTO', N'Documents', N'MimeType'),
(N'T_DOCUMENTO', N'IN_TAMANOBYTES_DOCUMENTO', N'Documents', N'FileSizeBytes'),
(N'T_DOCUMENTO', N'BO_ACTIVO_DOCUMENTO', N'Documents', N'IsActive'),
(N'T_DOCUMENTO', N'IN_VERSION_DOCUMENTO', N'Documents', N'Version'),
(N'T_DOCUMENTO', N'IN_PROVEEDORALMACENAMIENTO_DOCUMENTO', N'Documents', N'StorageProvider'),
(N'T_DOCUMENTO', N'DT_FECHACARGA_DOCUMENTO', N'Documents', N'UploadedAt'),
(N'T_DOCUMENTO', N'ST_IPCARGA_DOCUMENTO', N'Documents', N'UploadedByIp'),
(N'T_CONTROLADOREFECTIVO', N'IN_COD_CONTROLADOREFECTIVO', N'EffectiveControllers', N'Id'),
(N'T_CONTROLADOREFECTIVO', N'IN_COD_SOLICITUD', N'EffectiveControllers', N'RequestId'),
(N'T_CONTROLADOREFECTIVO', N'ST_NUMEROIDENTIFICACION_CONTROLADOREFECTIVO', N'EffectiveControllers', N'IdNumber'),
(N'T_CONTROLADOREFECTIVO', N'ST_NOMBRECOMPLETO_CONTROLADOREFECTIVO', N'EffectiveControllers', N'FullName'),
(N'T_CONTROLADOREFECTIVO', N'ST_DIRECCION_CONTROLADOREFECTIVO', N'EffectiveControllers', N'Address'),
(N'T_CONTROLADOREFECTIVO', N'ST_CIUDAD_CONTROLADOREFECTIVO', N'EffectiveControllers', N'City'),
(N'T_CONTROLADOREFECTIVO', N'ST_PAIS_CONTROLADOREFECTIVO', N'EffectiveControllers', N'Country'),
(N'T_CONTROLADOREFECTIVO', N'NU_PORCENTAJEPARTICIPACION_CONTROLADOREFECTIVO', N'EffectiveControllers', N'ParticipationPercentage'),
(N'T_CONTROLADOREFECTIVO', N'BO_ESPEP_CONTROLADOREFECTIVO', N'EffectiveControllers', N'IsPEP'),
(N'T_CONTROLADOREFECTIVO', N'ST_DETALLEPEP_CONTROLADOREFECTIVO', N'EffectiveControllers', N'PepDetail'),
(N'T_CONTROLADOREFECTIVO', N'ST_DESCRIPCIONCONTROL_CONTROLADOREFECTIVO', N'EffectiveControllers', N'ControlDescription'),
(N'T_CONTROLADOREFECTIVO', N'IN_ORDEN_CONTROLADOREFECTIVO', N'EffectiveControllers', N'SortOrder'),
(N'T_CONTROLADOREFECTIVO', N'DT_FECHACREACION_CONTROLADOREFECTIVO', N'EffectiveControllers', N'CreatedAt'),
(N'T_CONTROLADOREFECTIVO', N'DT_FECHAMODIFICACION_CONTROLADOREFECTIVO', N'EffectiveControllers', N'UpdatedAt'),
(N'T_ALMACENAMIENTOARCHIVO', N'IN_COD_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'Id'),
(N'T_ALMACENAMIENTOARCHIVO', N'IN_COD_DOCUMENTO', N'FileStorageRecords', N'DocumentId'),
(N'T_ALMACENAMIENTOARCHIVO', N'IN_PROVEEDORALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'StorageProvider'),
(N'T_ALMACENAMIENTOARCHIVO', N'ST_RUTAALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'StoragePath'),
(N'T_ALMACENAMIENTOARCHIVO', N'ST_CLAVEALMACENAMIENTO_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'StorageKey'),
(N'T_ALMACENAMIENTOARCHIVO', N'DT_FECHACARGA_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'UploadedAt'),
(N'T_ALMACENAMIENTOARCHIVO', N'ST_METADATOS_ALMACENAMIENTOARCHIVO', N'FileStorageRecords', N'Metadata'),
(N'T_DECLARACIONPERSONAJURIDICA', N'IN_COD_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'Id'),
(N'T_DECLARACIONPERSONAJURIDICA', N'IN_COD_SOLICITUD', N'LegalEntityDeclarations', N'RequestId'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_RUT_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'RUT'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_RAZONSOCIAL_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'BusinessName'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_DIRECCION_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'Address'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_CIUDAD_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'City'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_PAISCONSTITUCION_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'CountryOfIncorporation'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_TELEFONO_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'Phone'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_NUMEROIDENTIFICACIONREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'LegalRepresentativeIdNumber'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_NOMBREREPRESENTANTELEGAL_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'LegalRepresentativeName'),
(N'T_DECLARACIONPERSONAJURIDICA', N'IN_TIPOENTIDAD_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'EntityType'),
(N'T_DECLARACIONPERSONAJURIDICA', N'ST_TIPOENTIDADOTRO_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'EntityTypeOther'),
(N'T_DECLARACIONPERSONAJURIDICA', N'DT_FECHACREACION_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'CreatedAt'),
(N'T_DECLARACIONPERSONAJURIDICA', N'DT_FECHAMODIFICACION_DECLARACIONPERSONAJURIDICA', N'LegalEntityDeclarations', N'UpdatedAt'),
(N'T_NOTIFICACION', N'IN_COD_NOTIFICACION', N'Notifications', N'Id'),
(N'T_NOTIFICACION', N'IN_COD_USUARIO', N'Notifications', N'UserId'),
(N'T_NOTIFICACION', N'IN_TIPO_NOTIFICACION', N'Notifications', N'Type'),
(N'T_NOTIFICACION', N'ST_MENSAJE_NOTIFICACION', N'Notifications', N'Message'),
(N'T_NOTIFICACION', N'IN_COD_SOLICITUD', N'Notifications', N'RequestId'),
(N'T_NOTIFICACION', N'BO_LEIDA_NOTIFICACION', N'Notifications', N'IsRead'),
(N'T_NOTIFICACION', N'DT_FECHALECTURA_NOTIFICACION', N'Notifications', N'ReadAt'),
(N'T_NOTIFICACION', N'DT_FECHACREACION_NOTIFICACION', N'Notifications', N'CreatedAt'),
(N'T_DECLARACIONPEP', N'IN_COD_DECLARACIONPEP', N'PepDeclarations', N'Id'),
(N'T_DECLARACIONPEP', N'IN_COD_SOLICITUD', N'PepDeclarations', N'RequestId'),
(N'T_DECLARACIONPEP', N'ST_NOMBREDECLARANTE_DECLARACIONPEP', N'PepDeclarations', N'DeclarantName'),
(N'T_DECLARACIONPEP', N'ST_NUMEROIDENTIFICACION_DECLARACIONPEP', N'PepDeclarations', N'IdNumber'),
(N'T_DECLARACIONPEP', N'ST_NACIONALIDAD_DECLARACIONPEP', N'PepDeclarations', N'Nationality'),
(N'T_DECLARACIONPEP', N'BO_DECLARAPEP_DECLARACIONPEP', N'PepDeclarations', N'DeclaresPEP'),
(N'T_DECLARACIONPEP', N'ST_NOMBREPEP_DECLARACIONPEP', N'PepDeclarations', N'PepName'),
(N'T_DECLARACIONPEP', N'ST_INSTITUCION_DECLARACIONPEP', N'PepDeclarations', N'Institution'),
(N'T_DECLARACIONPEP', N'IN_MOTIVOPEP_DECLARACIONPEP', N'PepDeclarations', N'PepReasonType'),
(N'T_DECLARACIONPEP', N'ST_MOTIVOPEPOTRO_DECLARACIONPEP', N'PepDeclarations', N'PepReasonOther'),
(N'T_DECLARACIONPEP', N'ST_TIPOVINCULO_DECLARACIONPEP', N'PepDeclarations', N'VinculoType'),
(N'T_DECLARACIONPEP', N'DT_FECHADECLARACION_DECLARACIONPEP', N'PepDeclarations', N'DeclarationDate'),
(N'T_DECLARACIONPEP', N'BO_ACEPTABAJOJURAMENTO_DECLARACIONPEP', N'PepDeclarations', N'AcceptsUnderOath'),
(N'T_DECLARACIONPEP', N'ST_NOMBRECOMPLETOFIRMA_DECLARACIONPEP', N'PepDeclarations', N'SignatureFullName'),
(N'T_DECLARACIONPEP', N'ST_NUMEROIDENTIFICACIONFIRMA_DECLARACIONPEP', N'PepDeclarations', N'SignatureIdNumber'),
(N'T_DECLARACIONPEP', N'DT_FECHAFIRMA_DECLARACIONPEP', N'PepDeclarations', N'SignatureDateTime'),
(N'T_DECLARACIONPEP', N'ST_DIRECCIONIPFIRMA_DECLARACIONPEP', N'PepDeclarations', N'SignatureIpAddress'),
(N'T_DECLARACIONPEP', N'ST_USERAGENTFIRMA_DECLARACIONPEP', N'PepDeclarations', N'SignatureUserAgent'),
(N'T_DECLARACIONPEP', N'DT_FECHACREACION_DECLARACIONPEP', N'PepDeclarations', N'CreatedAt'),
(N'T_DECLARACIONPEP', N'DT_FECHAMODIFICACION_DECLARACIONPEP', N'PepDeclarations', N'UpdatedAt'),
(N'T_SOLICITUD', N'IN_COD_SOLICITUD', N'Requests', N'Id'),
(N'T_SOLICITUD', N'ST_NUMERO_SOLICITUD', N'Requests', N'RequestNumber'),
(N'T_SOLICITUD', N'IN_COD_CLIENTE', N'Requests', N'ClientId'),
(N'T_SOLICITUD', N'IN_COD_USUARIO', N'Requests', N'VendorUserId'),
(N'T_SOLICITUD', N'IN_TIPO_SOLICITUD', N'Requests', N'RequestType'),
(N'T_SOLICITUD', N'IN_ESTADO_SOLICITUD', N'Requests', N'Status'),
(N'T_SOLICITUD', N'ST_TOKENCLIENTE_SOLICITUD', N'Requests', N'ClientToken'),
(N'T_SOLICITUD', N'DT_FECHAVENCIMIENTOTOKEN_SOLICITUD', N'Requests', N'TokenExpiry'),
(N'T_SOLICITUD', N'ST_NOTASINTERNAS_SOLICITUD', N'Requests', N'InternalNotes'),
(N'T_SOLICITUD', N'IN_PASOACTUAL_SOLICITUD', N'Requests', N'CurrentStep'),
(N'T_SOLICITUD', N'DT_FECHACREACION_SOLICITUD', N'Requests', N'CreatedAt'),
(N'T_SOLICITUD', N'ST_CREADOPOR_SOLICITUD', N'Requests', N'CreatedBy'),
(N'T_SOLICITUD', N'DT_FECHAMODIFICACION_SOLICITUD', N'Requests', N'UpdatedAt'),
(N'T_SOLICITUD', N'ST_MODIFICADOPOR_SOLICITUD', N'Requests', N'UpdatedBy'),
(N'T_SOLICITUD', N'DT_FECHAENVIO_SOLICITUD', N'Requests', N'SentAt'),
(N'T_SOLICITUD', N'DT_FECHAAPERTURA_SOLICITUD', N'Requests', N'OpenedAt'),
(N'T_SOLICITUD', N'DT_FECHACOMPLETADA_SOLICITUD', N'Requests', N'CompletedAt'),
(N'T_SOLICITUD', N'DT_FECHAVENCIMIENTO_SOLICITUD', N'Requests', N'DueDate'),
(N'T_SOLICITUD', N'BO_ELIMINADO_SOLICITUD', N'Requests', N'IsDeleted'),
(N'T_SOLICITUD', N'ST_EMAILCLIENTE_SOLICITUD', N'Requests', N'ClientEmail'),
(N'T_SOLICITUD', N'ST_TELEFONOCLIENTE_SOLICITUD', N'Requests', N'ClientPhone'),
(N'T_SOLICITUD', N'ST_NUMERONEGOCIONETCAR_SOLICITUD', N'Requests', N'NetcarBusinessNumber'),
(N'T_HISTORIALESTADOSOLICITUD', N'IN_COD_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'Id'),
(N'T_HISTORIALESTADOSOLICITUD', N'IN_COD_SOLICITUD', N'RequestStatusHistories', N'RequestId'),
(N'T_HISTORIALESTADOSOLICITUD', N'IN_ESTADOANTERIOR_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'OldStatus'),
(N'T_HISTORIALESTADOSOLICITUD', N'IN_ESTADONUEVO_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'NewStatus'),
(N'T_HISTORIALESTADOSOLICITUD', N'ST_CAMBIADOPOR_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'ChangedBy'),
(N'T_HISTORIALESTADOSOLICITUD', N'ST_NOTAS_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'Notes'),
(N'T_HISTORIALESTADOSOLICITUD', N'DT_FECHACAMBIO_HISTORIALESTADOSOLICITUD', N'RequestStatusHistories', N'ChangedAt'),
(N'T_ROL', N'IN_COD_ROL', N'Roles', N'Id'),
(N'T_ROL', N'ST_NOMBRE_ROL', N'Roles', N'Name'),
(N'T_ROL', N'ST_NOMBRENORMALIZADO_ROL', N'Roles', N'NormalizedName'),
(N'T_ROL', N'ST_SELLOCONCURRENCIA_ROL', N'Roles', N'ConcurrencyStamp'),
(N'T_ROLCLAIM', N'IN_COD_ROLCLAIM', N'RolesClaims', N'Id'),
(N'T_ROLCLAIM', N'IN_COD_ROL', N'RolesClaims', N'RoleId'),
(N'T_ROLCLAIM', N'ST_TIPOCLAIM_ROLCLAIM', N'RolesClaims', N'ClaimType'),
(N'T_ROLCLAIM', N'ST_VALORCLAIM_ROLCLAIM', N'RolesClaims', N'ClaimValue'),
(N'T_FIRMA', N'IN_COD_FIRMA', N'SignatureRecords', N'Id'),
(N'T_FIRMA', N'IN_COD_SOLICITUD', N'SignatureRecords', N'RequestId'),
(N'T_FIRMA', N'IN_TIPO_FIRMA', N'SignatureRecords', N'Type'),
(N'T_FIRMA', N'IN_ESTADO_FIRMA', N'SignatureRecords', N'Status'),
(N'T_FIRMA', N'ST_NOMBREFIRMANTE_FIRMA', N'SignatureRecords', N'SignerName'),
(N'T_FIRMA', N'ST_NUMEROIDENTIFICACIONFIRMANTE_FIRMA', N'SignatureRecords', N'SignerIdNumber'),
(N'T_FIRMA', N'DT_FECHAFIRMA_FIRMA', N'SignatureRecords', N'SignedAt'),
(N'T_FIRMA', N'ST_DIRECCIONIP_FIRMA', N'SignatureRecords', N'IpAddress'),
(N'T_FIRMA', N'ST_USERAGENT_FIRMA', N'SignatureRecords', N'UserAgent'),
(N'T_FIRMA', N'ST_HASHDOCUMENTO_FIRMA', N'SignatureRecords', N'DocumentHash'),
(N'T_FIRMA', N'ST_NUMEROFOLIO_FIRMA', N'SignatureRecords', N'FolioNumber'),
(N'T_FIRMA', N'ST_CODTRANSACCIONPROVEEDOR_FIRMA', N'SignatureRecords', N'ProviderTransactionId'),
(N'T_FIRMA', N'ST_URL_FIRMA', N'SignatureRecords', N'SignatureUrl'),
(N'T_FIRMA', N'ST_CERTIFICADO_FIRMA', N'SignatureRecords', N'Certificate'),
(N'T_FIRMA', N'ST_RUTAPDFFIRMADO_FIRMA', N'SignatureRecords', N'SignedPdfPath'),
(N'T_FIRMA', N'DT_FECHACREACION_FIRMA', N'SignatureRecords', N'CreatedAt'),
(N'T_FIRMA', N'DT_FECHAMODIFICACION_FIRMA', N'SignatureRecords', N'UpdatedAt'),
(N'T_ALERTACARPETATRIBUTARIA', N'IN_COD_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts', N'Id'),
(N'T_ALERTACARPETATRIBUTARIA', N'IN_COD_ANALISISCARPETATRIBUTARIA', N'TaxFolderAlerts', N'TaxFolderAnalysisId'),
(N'T_ALERTACARPETATRIBUTARIA', N'IN_TIPO_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts', N'AlertType'),
(N'T_ALERTACARPETATRIBUTARIA', N'ST_MENSAJE_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts', N'Message'),
(N'T_ALERTACARPETATRIBUTARIA', N'ST_SEVERIDAD_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts', N'Severity'),
(N'T_ALERTACARPETATRIBUTARIA', N'BO_RESUELTA_ALERTACARPETATRIBUTARIA', N'TaxFolderAlerts', N'IsResolved'),
(N'T_ANALISISCARPETATRIBUTARIA', N'IN_COD_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'Id'),
(N'T_ANALISISCARPETATRIBUTARIA', N'IN_COD_SOLICITUD', N'TaxFolderAnalyses', N'RequestId'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_RUTEXTRAIDO_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedRut'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_RAZONSOCIALEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedBusinessName'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_DIRECCIONEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedAddress'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_ACTIVIDADEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedActivity'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_REPRESENTANTELEGALEXTRAIDO_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedLegalRep'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_FECHAEMISIONEXTRAIDA_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'ExtractedIssuedDate'),
(N'T_ANALISISCARPETATRIBUTARIA', N'ST_TEXTOBRUTO_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'RawText'),
(N'T_ANALISISCARPETATRIBUTARIA', N'BO_LEGIBLE_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'WasReadable'),
(N'T_ANALISISCARPETATRIBUTARIA', N'DT_FECHAANALISIS_ANALISISCARPETATRIBUTARIA', N'TaxFolderAnalyses', N'AnalyzedAt'),
(N'T_USUARIO', N'IN_COD_USUARIO', N'Usuarios', N'Id'),
(N'T_USUARIO', N'ST_NOMBRECOMPLETO_USUARIO', N'Usuarios', N'FullName'),
(N'T_USUARIO', N'ST_CODVENDEDOR_USUARIO', N'Usuarios', N'VendorCode'),
(N'T_USUARIO', N'BO_ACTIVO_USUARIO', N'Usuarios', N'IsActive'),
(N'T_USUARIO', N'DT_FECHACREACION_USUARIO', N'Usuarios', N'CreatedAt'),
(N'T_USUARIO', N'DT_FECHAULTIMOLOGIN_USUARIO', N'Usuarios', N'LastLoginAt'),
(N'T_USUARIO', N'ST_NOMBREUSUARIO_USUARIO', N'Usuarios', N'UserName'),
(N'T_USUARIO', N'ST_NOMBREUSUARIONORMALIZADO_USUARIO', N'Usuarios', N'NormalizedUserName'),
(N'T_USUARIO', N'ST_EMAIL_USUARIO', N'Usuarios', N'Email'),
(N'T_USUARIO', N'ST_EMAILNORMALIZADO_USUARIO', N'Usuarios', N'NormalizedEmail'),
(N'T_USUARIO', N'BO_EMAILCONFIRMADO_USUARIO', N'Usuarios', N'EmailConfirmed'),
(N'T_USUARIO', N'ST_HASHCONTRASENA_USUARIO', N'Usuarios', N'PasswordHash'),
(N'T_USUARIO', N'ST_SELLOSEGURIDAD_USUARIO', N'Usuarios', N'SecurityStamp'),
(N'T_USUARIO', N'ST_SELLOCONCURRENCIA_USUARIO', N'Usuarios', N'ConcurrencyStamp'),
(N'T_USUARIO', N'ST_TELEFONO_USUARIO', N'Usuarios', N'PhoneNumber'),
(N'T_USUARIO', N'BO_TELEFONOCONFIRMADO_USUARIO', N'Usuarios', N'PhoneNumberConfirmed'),
(N'T_USUARIO', N'BO_DOBLEFACTORHABILITADO_USUARIO', N'Usuarios', N'TwoFactorEnabled'),
(N'T_USUARIO', N'DT_FECHAFINBLOQUEO_USUARIO', N'Usuarios', N'LockoutEnd'),
(N'T_USUARIO', N'BO_BLOQUEOHABILITADO_USUARIO', N'Usuarios', N'LockoutEnabled'),
(N'T_USUARIO', N'IN_CANTIDADACCESOSFALLIDOS_USUARIO', N'Usuarios', N'AccessFailedCount'),
(N'T_USUARIO', N'ST_APELLIDOMATERNO_USUARIO', N'Usuarios', N'ApellidoMaterno'),
(N'T_USUARIO', N'ST_APELLIDOPATERNO_USUARIO', N'Usuarios', N'ApellidoPaterno'),
(N'T_USUARIO', N'ST_NOMBRE_USUARIO', N'Usuarios', N'Nombre'),
(N'T_USUARIO', N'ST_RUT_USUARIO', N'Usuarios', N'Rut'),
(N'T_USUARIOCLAIM', N'IN_COD_USUARIOCLAIM', N'UsuariosClaims', N'Id'),
(N'T_USUARIOCLAIM', N'IN_COD_USUARIO', N'UsuariosClaims', N'UserId'),
(N'T_USUARIOCLAIM', N'ST_TIPOCLAIM_USUARIOCLAIM', N'UsuariosClaims', N'ClaimType'),
(N'T_USUARIOCLAIM', N'ST_VALORCLAIM_USUARIOCLAIM', N'UsuariosClaims', N'ClaimValue'),
(N'T_USUARIOLOGIN', N'ST_PROVEEDORLOGIN_USUARIOLOGIN', N'UsuariosLogins', N'LoginProvider'),
(N'T_USUARIOLOGIN', N'ST_CLAVEPROVEEDOR_USUARIOLOGIN', N'UsuariosLogins', N'ProviderKey'),
(N'T_USUARIOLOGIN', N'ST_NOMBREVISIBLEPROVEEDOR_USUARIOLOGIN', N'UsuariosLogins', N'ProviderDisplayName'),
(N'T_USUARIOLOGIN', N'IN_COD_USUARIO', N'UsuariosLogins', N'UserId'),
(N'T_USUARIOROL', N'IN_COD_USUARIO', N'UsuariosRoles', N'UserId'),
(N'T_USUARIOROL', N'IN_COD_ROL', N'UsuariosRoles', N'RoleId'),
(N'T_USUARIOTOKEN', N'IN_COD_USUARIO', N'UsuariosTokens', N'UserId'),
(N'T_USUARIOTOKEN', N'ST_PROVEEDORLOGIN_USUARIOTOKEN', N'UsuariosTokens', N'LoginProvider'),
(N'T_USUARIOTOKEN', N'ST_NOMBRE_USUARIOTOKEN', N'UsuariosTokens', N'Name'),
(N'T_USUARIOTOKEN', N'ST_VALOR_USUARIOTOKEN', N'UsuariosTokens', N'Value');

DECLARE @PK TABLE (Nombre sysname);
INSERT @PK VALUES
(N'PK_T_USUARIOTOKEN'),
(N'PK_T_USUARIOROL'),
(N'PK_T_USUARIOLOGIN'),
(N'PK_T_USUARIOCLAIM'),
(N'PK_T_USUARIO'),
(N'PK_T_ANALISISCARPETATRIBUTARIA'),
(N'PK_T_ALERTACARPETATRIBUTARIA'),
(N'PK_T_FIRMA'),
(N'PK_T_ROLCLAIM'),
(N'PK_T_ROL'),
(N'PK_T_HISTORIALESTADOSOLICITUD'),
(N'PK_T_SOLICITUD'),
(N'PK_T_DECLARACIONPEP'),
(N'PK_T_NOTIFICACION'),
(N'PK_T_DECLARACIONPERSONAJURIDICA'),
(N'PK_T_ALMACENAMIENTOARCHIVO'),
(N'PK_T_CONTROLADOREFECTIVO'),
(N'PK_T_DOCUMENTO'),
(N'PK_T_PERSONADECLARADA'),
(N'PK_T_DECLARANTE'),
(N'PK_T_CLIENTE'),
(N'PK_T_BENEFICIARIOFINAL'),
(N'PK_T_AUDITORIA');

DECLARE @FK TABLE (Nombre sysname);
INSERT @FK VALUES
(N'FK_T_AUDITORIA_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_BENEFICIARIOFINAL_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_DECLARANTE_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_PERSONADECLARADA_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_DOCUMENTO_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_CONTROLADOREFECTIVO_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_ALMACENAMIENTOARCHIVO_T_DOCUMENTO_IN_COD_DOCUMENTO'),
(N'FK_T_DECLARACIONPERSONAJURIDICA_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_DECLARACIONPEP_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_SOLICITUD_T_CLIENTE_IN_COD_CLIENTE'),
(N'FK_T_SOLICITUD_T_USUARIO_IN_COD_USUARIO'),
(N'FK_T_HISTORIALESTADOSOLICITUD_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_ROLCLAIM_T_ROL_IN_COD_ROL'),
(N'FK_T_FIRMA_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_ALERTACARPETATRIBUTARIA_T_ANALISISCARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA'),
(N'FK_T_ANALISISCARPETATRIBUTARIA_T_SOLICITUD_IN_COD_SOLICITUD'),
(N'FK_T_USUARIOCLAIM_T_USUARIO_IN_COD_USUARIO'),
(N'FK_T_USUARIOLOGIN_T_USUARIO_IN_COD_USUARIO'),
(N'FK_T_USUARIOROL_T_USUARIO_IN_COD_USUARIO'),
(N'FK_T_USUARIOROL_T_ROL_IN_COD_ROL'),
(N'FK_T_USUARIOTOKEN_T_USUARIO_IN_COD_USUARIO');

DECLARE @IX TABLE (Nombre sysname);
INSERT @IX VALUES
(N'IX_T_USUARIOROL_IN_COD_ROL'),
(N'IX_T_USUARIOLOGIN_IN_COD_USUARIO'),
(N'IX_T_USUARIOCLAIM_IN_COD_USUARIO'),
(N'IX_T_ANALISISCARPETATRIBUTARIA_IN_COD_SOLICITUD'),
(N'IX_T_ALERTACARPETATRIBUTARIA_IN_COD_ANALISISCARPETATRIBUTARIA'),
(N'IX_T_FIRMA_IN_COD_SOLICITUD'),
(N'IX_T_ROLCLAIM_IN_COD_ROL'),
(N'IX_T_HISTORIALESTADOSOLICITUD_IN_COD_SOLICITUD'),
(N'IX_T_SOLICITUD_IN_COD_USUARIO'),
(N'IX_T_SOLICITUD_IN_ESTADO_SOLICITUD'),
(N'IX_T_SOLICITUD_ST_NUMERO_SOLICITUD'),
(N'IX_T_SOLICITUD_DT_FECHACREACION_SOLICITUD'),
(N'IX_T_SOLICITUD_ST_TOKENCLIENTE_SOLICITUD'),
(N'IX_T_SOLICITUD_IN_COD_CLIENTE'),
(N'IX_T_DECLARACIONPEP_IN_COD_SOLICITUD'),
(N'IX_T_NOTIFICACION_IN_COD_USUARIO_BO_LEIDA_NOTIFICACION'),
(N'IX_T_NOTIFICACION_IN_COD_USUARIO'),
(N'IX_T_NOTIFICACION_DT_FECHACREACION_NOTIFICACION'),
(N'IX_T_DECLARACIONPERSONAJURIDICA_IN_COD_SOLICITUD'),
(N'IX_T_ALMACENAMIENTOARCHIVO_IN_COD_DOCUMENTO'),
(N'IX_T_CONTROLADOREFECTIVO_IN_COD_SOLICITUD'),
(N'IX_T_DOCUMENTO_IN_COD_SOLICITUD'),
(N'IX_T_PERSONADECLARADA_IN_COD_SOLICITUD'),
(N'IX_T_DECLARANTE_IN_COD_SOLICITUD'),
(N'IX_T_CLIENTE_ST_RUT_CLIENTE'),
(N'IX_T_BENEFICIARIOFINAL_IN_COD_SOLICITUD'),
(N'IX_T_AUDITORIA_IN_COD_SOLICITUD'),
(N'IX_T_AUDITORIA_DT_FECHACREACION_AUDITORIA'),
(N'UserNameIndex'),
(N'RoleNameIndex'),
(N'EmailIndex');

-- 0. Migración registrada en EF
SELECT '0. Migración registrada' AS Chequeo, COUNT(*) AS Encontrada FROM [__EFMigrationsHistory] WHERE MigrationId = N'20261001130256_ConvencionNombresT';

-- 1. Tablas nuevas que faltan (debe ser vacío)
SELECT '1. Tabla faltante' AS Chequeo, t.Nueva FROM @Tablas t WHERE OBJECT_ID(N'[dbo].' + QUOTENAME(t.Nueva), N'U') IS NULL;

-- 2. Tablas con nombre antiguo que siguen existiendo (debe ser vacío)
SELECT '2. Tabla antigua presente' AS Chequeo, t.Original FROM @Tablas t WHERE OBJECT_ID(N'[dbo].' + QUOTENAME(t.Original), N'U') IS NOT NULL;

-- 3. Columnas nuevas que faltan (debe ser vacío)
SELECT '3. Columna faltante' AS Chequeo, c.Tabla, c.Nueva FROM @Columnas c WHERE COL_LENGTH(N'[dbo].' + QUOTENAME(c.Tabla), c.Nueva) IS NULL;

-- 4. Columnas sobrantes: existen en las tablas T_ pero no están en la propuesta (debe ser vacío)
SELECT '4. Columna no prevista' AS Chequeo, t.name AS Tabla, col.name AS Columna
FROM sys.tables t JOIN sys.columns col ON col.object_id = t.object_id
WHERE t.name IN (SELECT Nueva FROM @Tablas)
  AND NOT EXISTS (SELECT 1 FROM @Columnas c WHERE c.Tabla = t.name AND c.Nueva = col.name);

-- 5. Primary Keys esperadas que faltan (debe ser vacío) y tablas T_ sin PK (debe ser vacío)
SELECT '5a. PK faltante' AS Chequeo, p.Nombre FROM @PK p WHERE NOT EXISTS (SELECT 1 FROM sys.key_constraints k WHERE k.type = 'PK' AND k.name = p.Nombre);
SELECT '5b. Tabla sin PK' AS Chequeo, t.Nueva FROM @Tablas t WHERE OBJECTPROPERTY(OBJECT_ID(N'[dbo].' + QUOTENAME(t.Nueva)), 'TableHasPrimaryKey') = 0;

-- 6. Foreign Keys esperadas que faltan, deshabilitadas o no confiables (debe ser vacío)
SELECT '6. FK con problema' AS Chequeo, f.Nombre,
       CASE WHEN fk.object_id IS NULL THEN 'FALTA' WHEN fk.is_disabled = 1 THEN 'DESHABILITADA' ELSE 'NO CONFIABLE' END AS Problema
FROM @FK f LEFT JOIN sys.foreign_keys fk ON fk.name = f.Nombre
WHERE fk.object_id IS NULL OR fk.is_disabled = 1 OR fk.is_not_trusted = 1;

-- 7. Índices esperados que faltan o están deshabilitados (debe ser vacío)
SELECT '7. Índice con problema' AS Chequeo, i.Nombre FROM @IX i
WHERE NOT EXISTS (SELECT 1 FROM sys.indexes x JOIN sys.tables t ON t.object_id = x.object_id WHERE x.name = i.Nombre AND x.is_disabled = 0 AND t.name IN (SELECT Nueva FROM @Tablas));

-- 8. Resumen de objetos (esperado: 23 PK, 21 FK, 54 índices, 2 DEFAULT, 0 CHECK)
SELECT '8. Resumen' AS Chequeo,
  (SELECT COUNT(*) FROM sys.key_constraints k JOIN @Tablas t ON OBJECT_NAME(k.parent_object_id) = t.Nueva WHERE k.type = 'PK') AS PKs,
  (SELECT COUNT(*) FROM sys.foreign_keys f JOIN @Tablas t ON OBJECT_NAME(f.parent_object_id) = t.Nueva) AS FKs,
  (SELECT COUNT(*) FROM sys.indexes i JOIN @Tablas t ON OBJECT_NAME(i.object_id) = t.Nueva WHERE i.type > 0) AS Indices,
  (SELECT COUNT(*) FROM sys.default_constraints d JOIN @Tablas t ON OBJECT_NAME(d.parent_object_id) = t.Nueva) AS Defaults,
  (SELECT COUNT(*) FROM sys.check_constraints c JOIN @Tablas t ON OBJECT_NAME(c.parent_object_id) = t.Nueva) AS Checks;

-- 9. Vistas, procedimientos, funciones y triggers con referencias rotas (debe ser vacío)
--    Toda referencia a un objeto inexistente aparece con referenced_id NULL.
SELECT '9a. Referencia rota' AS Chequeo, OBJECT_SCHEMA_NAME(d.referencing_id) + '.' + OBJECT_NAME(d.referencing_id) AS Objeto, o.type_desc,
       d.referenced_entity_name AS Referencia
FROM sys.sql_expression_dependencies d JOIN sys.objects o ON o.object_id = d.referencing_id
WHERE d.referenced_id IS NULL AND d.referenced_database_name IS NULL AND d.is_ambiguous = 0
  AND d.referenced_entity_name NOT IN (SELECT name FROM sys.types);

--    Revalidar cada módulo (vista/proc/función/trigger) sin ejecutarlo. Si alguno queda roto se lista.
DECLARE @rotos TABLE (Objeto nvarchar(300), Error nvarchar(4000));
DECLARE @obj nvarchar(300);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR
  SELECT QUOTENAME(OBJECT_SCHEMA_NAME(m.object_id)) + '.' + QUOTENAME(OBJECT_NAME(m.object_id))
  FROM sys.sql_modules m JOIN sys.objects o ON o.object_id = m.object_id
  WHERE m.is_schema_bound = 0 AND o.is_ms_shipped = 0 AND o.type IN ('V','P','FN','IF','TF','TR');
OPEN c; FETCH NEXT FROM c INTO @obj;
WHILE @@FETCH_STATUS = 0
BEGIN
  BEGIN TRY EXEC sys.sp_refreshsqlmodule @obj; END TRY
  BEGIN CATCH INSERT @rotos VALUES (@obj, ERROR_MESSAGE()); END CATCH
  FETCH NEXT FROM c INTO @obj;
END
CLOSE c; DEALLOCATE c;
SELECT '9b. Módulo que no compila' AS Chequeo, * FROM @rotos;

-- 10. Cumplimiento de la convención (debe ser vacío)
--     Tablas: T_ + MAYÚSCULAS/dígitos, sin más "_".
SELECT '10a. Tabla fuera de convención' AS Chequeo, t.name
FROM sys.tables t
WHERE t.is_ms_shipped = 0 AND t.name NOT IN (N'__EFMigrationsHistory', N'sysdiagrams')
  AND (t.name NOT LIKE 'T[_]%' COLLATE Latin1_General_BIN
       OR SUBSTRING(t.name, 3, 128) LIKE '%[^A-Z0-9]%' COLLATE Latin1_General_BIN
       OR LEN(t.name) < 3);

--     Columnas: PREFIJO_DESCRIPCION_ENTIDAD (entidad = tabla sin "T_"), prefijo acorde al tipo,
--     o bien FK con el mismo nombre que la PK de la tabla referenciada (IN_COD_ENTIDAD).
;WITH col AS (
  SELECT t.name AS Tabla, SUBSTRING(t.name, 3, 128) AS Entidad, c.name AS Columna, ty.name AS Tipo,
         LEFT(c.name, CHARINDEX('_', c.name + '_') - 1) AS Prefijo,
         LEN(c.name) - LEN(REPLACE(c.name, '_', '')) AS Guiones,
         CASE WHEN EXISTS (SELECT 1 FROM sys.foreign_key_columns fkc
                           JOIN sys.columns rc ON rc.object_id = fkc.referenced_object_id AND rc.column_id = fkc.referenced_column_id
                           WHERE fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id AND rc.name = c.name)
              THEN 1 ELSE 0 END AS EsFkMismoNombre,
         CASE WHEN c.name LIKE 'IN[_]COD[_]%' AND OBJECT_ID(N'[dbo].' + QUOTENAME('T_' + SUBSTRING(c.name, 8, 128)), N'U') IS NOT NULL
              THEN 1 ELSE 0 END AS EsReferenciaACodTabla
  FROM sys.tables t
  JOIN sys.columns c ON c.object_id = t.object_id
  JOIN sys.types ty ON ty.user_type_id = c.user_type_id
  WHERE t.name LIKE 'T[_]%'
)
SELECT '10b. Columna fuera de convención' AS Chequeo, Tabla, Columna, Tipo,
  CASE
    WHEN Columna LIKE '%[^A-Z0-9_]%' COLLATE Latin1_General_BIN THEN 'Caracteres no permitidos / minúsculas'
    WHEN Prefijo NOT IN ('IN','ST','DT','NU','BO','BL','CH') THEN 'Prefijo desconocido'
    WHEN Columna LIKE 'IN[_]COD[_]%' AND (EsFkMismoNombre = 1 OR EsReferenciaACodTabla = 1 OR Columna = 'IN_COD_' + Entidad) THEN NULL
    WHEN Guiones <> 2 THEN 'Debe tener exactamente 2 "_" (PREFIJO_DESCRIPCION_ENTIDAD)'
    WHEN RIGHT(Columna, LEN(Entidad) + 1) <> '_' + Entidad THEN 'No termina en _' + Entidad
    WHEN Prefijo IN ('BO','BL') AND Tipo <> 'bit' THEN 'BO_/BL_ solo para bit'
    WHEN Prefijo = 'DT' AND Tipo NOT IN ('date','datetime','datetime2','smalldatetime','datetimeoffset','time') THEN 'DT_ solo para fechas'
    WHEN Prefijo = 'NU' AND Tipo NOT IN ('decimal','numeric','money','smallmoney','float','real') THEN 'NU_ solo para decimales'
    WHEN Prefijo = 'ST' AND Tipo NOT IN ('varchar','nvarchar','text','ntext','sysname') THEN 'ST_ solo para texto variable'
    WHEN Prefijo = 'CH' AND Tipo NOT IN ('char','nchar') THEN 'CH_ solo para char'
    WHEN Prefijo = 'IN' AND Tipo NOT IN ('int','bigint','smallint','tinyint') AND Columna NOT LIKE 'IN[_]COD[_]%' THEN 'IN_ solo para enteros o identificadores'
  END AS Problema
FROM col
WHERE CASE
    WHEN Columna LIKE '%[^A-Z0-9_]%' COLLATE Latin1_General_BIN THEN 1
    WHEN Prefijo NOT IN ('IN','ST','DT','NU','BO','BL','CH') THEN 1
    WHEN Columna LIKE 'IN[_]COD[_]%' AND (EsFkMismoNombre = 1 OR EsReferenciaACodTabla = 1 OR Columna = 'IN_COD_' + Entidad) THEN 0
    WHEN Guiones <> 2 THEN 1
    WHEN RIGHT(Columna, LEN(Entidad) + 1) <> '_' + Entidad THEN 1
    WHEN Prefijo IN ('BO','BL') AND Tipo <> 'bit' THEN 1
    WHEN Prefijo = 'DT' AND Tipo NOT IN ('date','datetime','datetime2','smalldatetime','datetimeoffset','time') THEN 1
    WHEN Prefijo = 'NU' AND Tipo NOT IN ('decimal','numeric','money','smallmoney','float','real') THEN 1
    WHEN Prefijo = 'ST' AND Tipo NOT IN ('varchar','nvarchar','text','ntext','sysname') THEN 1
    WHEN Prefijo = 'CH' AND Tipo NOT IN ('char','nchar') THEN 1
    WHEN Prefijo = 'IN' AND Tipo NOT IN ('int','bigint','smallint','tinyint') AND Columna NOT LIKE 'IN[_]COD[_]%' THEN 1
    ELSE 0 END = 1;

-- 11. Filas y huella de datos por tabla. Ejecute 00_ConteoPrevio_ConvencionT.sql ANTES del renombrado
--     y compare: Filas y HuellaDatos deben ser idénticas (el renombrado no toca datos).
SELECT N'Requests' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_SOLICITUD]
UNION ALL SELECT N'Clients' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_CLIENTE]
UNION ALL SELECT N'Usuarios' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_USUARIO]
UNION ALL SELECT N'Roles' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_ROL]
UNION ALL SELECT N'UsuariosRoles' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_USUARIOROL]
UNION ALL SELECT N'UsuariosClaims' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_USUARIOCLAIM]
UNION ALL SELECT N'UsuariosLogins' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_USUARIOLOGIN]
UNION ALL SELECT N'UsuariosTokens' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_USUARIOTOKEN]
UNION ALL SELECT N'RolesClaims' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_ROLCLAIM]
UNION ALL SELECT N'LegalEntityDeclarations' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_DECLARACIONPERSONAJURIDICA]
UNION ALL SELECT N'DeclaredPersons' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_PERSONADECLARADA]
UNION ALL SELECT N'Declarants' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_DECLARANTE]
UNION ALL SELECT N'Documents' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_DOCUMENTO]
UNION ALL SELECT N'FileStorageRecords' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_ALMACENAMIENTOARCHIVO]
UNION ALL SELECT N'RequestStatusHistories' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_HISTORIALESTADOSOLICITUD]
UNION ALL SELECT N'AuditLogs' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_AUDITORIA]
UNION ALL SELECT N'Notifications' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_NOTIFICACION]
UNION ALL SELECT N'SignatureRecords' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_FIRMA]
UNION ALL SELECT N'TaxFolderAnalyses' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_ANALISISCARPETATRIBUTARIA]
UNION ALL SELECT N'TaxFolderAlerts' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_ALERTACARPETATRIBUTARIA]
UNION ALL SELECT N'BeneficialOwners' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_BENEFICIARIOFINAL]
UNION ALL SELECT N'EffectiveControllers' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_CONTROLADOREFECTIVO]
UNION ALL SELECT N'PepDeclarations' AS TablaOriginal, COUNT_BIG(*) AS Filas, CHECKSUM_AGG(BINARY_CHECKSUM(*)) AS HuellaDatos FROM [dbo].[T_DECLARACIONPEP];