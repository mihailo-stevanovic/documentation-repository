IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [DocumentType] (
        [Id] int NOT NULL IDENTITY,
        [DocumentCategory] int NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [ShortName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_DocumentType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [DocumentUpdate] (
        [Id] int NOT NULL IDENTITY,
        [DocumentId] int NOT NULL,
        [IsPublished] bit NOT NULL,
        [LatestTopicsUpdated] nvarchar(max) NULL,
        [Timestamp] datetime2 NOT NULL,
        CONSTRAINT [PK_DocumentUpdate] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [Product] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [ShortName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [ProductVersion] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [Release] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ProductVersion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductVersion_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [Document] (
        [Id] int NOT NULL IDENTITY,
        [AitId] int NOT NULL,
        [ClientCatalog] nvarchar(max) NULL,
        [DocumentTypeId] int NOT NULL,
        [FitForClients] bit NOT NULL,
        [HtmlLink] nvarchar(max) NULL,
        [LatestUpdateId] int NOT NULL,
        [LatestUpdateId1] int NULL,
        [ParentDocumentId] int NOT NULL,
        [PdfLink] nvarchar(max) NULL,
        [ProductVersionId] int NOT NULL,
        [ShortDescription] nvarchar(max) NULL,
        [Title] nvarchar(max) NOT NULL,
        [WordLink] nvarchar(max) NULL,
        CONSTRAINT [PK_Document] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Document_DocumentType_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentType] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Document_DocumentUpdate_LatestUpdateId1] FOREIGN KEY ([LatestUpdateId1]) REFERENCES [DocumentUpdate] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Document_ProductVersion_ProductVersionId] FOREIGN KEY ([ProductVersionId]) REFERENCES [ProductVersion] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE TABLE [Author] (
        [Id] int NOT NULL IDENTITY,
        [Alias] nvarchar(max) NOT NULL,
        [DocumentId] int NULL,
        [Email] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Author] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Author_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Document] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE INDEX [IX_Author_DocumentId] ON [Author] ([DocumentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE INDEX [IX_Document_DocumentTypeId] ON [Document] ([DocumentTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE INDEX [IX_Document_LatestUpdateId1] ON [Document] ([LatestUpdateId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE INDEX [IX_Document_ProductVersionId] ON [Document] ([ProductVersionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    CREATE INDEX [IX_ProductVersion_ProductId] ON [ProductVersion] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221222553_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180221222553_InitialCreate', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    ALTER TABLE [Author] DROP CONSTRAINT [FK_Author_Document_DocumentId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    DROP INDEX [IX_Author_DocumentId] ON [Author];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Author') AND [c].[name] = N'DocumentId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Author] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Author] DROP COLUMN [DocumentId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    CREATE TABLE [DocumentAuthor] (
        [Id] int NOT NULL IDENTITY,
        [AuthorId] int NOT NULL,
        [DocumentId] int NOT NULL,
        CONSTRAINT [PK_DocumentAuthor] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DocumentAuthor_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DocumentAuthor_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Document] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    CREATE UNIQUE INDEX [IX_DocumentAuthor_AuthorId] ON [DocumentAuthor] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    CREATE INDEX [IX_DocumentAuthor_DocumentId] ON [DocumentAuthor] ([DocumentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221224341_AddDocumentAuthor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180221224341_AddDocumentAuthor', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221225036_AddParentDocument')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'ParentDocumentId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Document] ALTER COLUMN [ParentDocumentId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221225036_AddParentDocument')
BEGIN
    CREATE INDEX [IX_Document_ParentDocumentId] ON [Document] ([ParentDocumentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221225036_AddParentDocument')
BEGIN
    ALTER TABLE [Document] ADD CONSTRAINT [FK_Document_Document_ParentDocumentId] FOREIGN KEY ([ParentDocumentId]) REFERENCES [Document] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221225036_AddParentDocument')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180221225036_AddParentDocument', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221230334_AddDocOtherLink')
BEGIN
    ALTER TABLE [Document] ADD [OtherLink] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180221230334_AddDocOtherLink')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180221230334_AddDocOtherLink', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    ALTER TABLE [Document] DROP CONSTRAINT [FK_Document_DocumentUpdate_LatestUpdateId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DROP INDEX [IX_Document_LatestUpdateId1] ON [Document];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'LatestUpdateId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Document] DROP COLUMN [LatestUpdateId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'LatestUpdateId1');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Document] DROP COLUMN [LatestUpdateId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    EXEC sp_rename N'Document.FitForClients', N'IsFitForClients', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'ProductVersion') AND [c].[name] = N'Release');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ProductVersion] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [ProductVersion] ALTER COLUMN [Release] nvarchar(10) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Product') AND [c].[name] = N'ShortName');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Product] ALTER COLUMN [ShortName] nvarchar(7) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'DocumentType') AND [c].[name] = N'ShortName');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [DocumentType] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [DocumentType] ALTER COLUMN [ShortName] nvarchar(5) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'AitId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Document] ALTER COLUMN [AitId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Author') AND [c].[name] = N'LastName');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Author] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Author] ALTER COLUMN [LastName] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Author') AND [c].[name] = N'FirstName');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Author] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Author] ALTER COLUMN [FirstName] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Author') AND [c].[name] = N'Alias');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Author] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Author] ALTER COLUMN [Alias] nvarchar(7) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    ALTER TABLE [Author] ADD [IsFormerAuthor] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224182537_AnnotationCleanup')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180224182537_AnnotationCleanup', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    DROP INDEX [IX_DocumentAuthor_AuthorId] ON [DocumentAuthor];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    ALTER TABLE [DocumentUpdate] ADD [RowVersion] rowversion NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    ALTER TABLE [Document] ADD [RowVersion] rowversion NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    ALTER TABLE [Author] ADD [AitName] nvarchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    CREATE INDEX [IX_DocumentAuthor_AuthorId] ON [DocumentAuthor] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180224190029_AddRowVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180224190029_AddRowVersion', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180303115534_ProdVersionEndOfSupport')
BEGIN
    ALTER TABLE [ProductVersion] ADD [EndOfSupport] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180303115534_ProdVersionEndOfSupport')
BEGIN
    ALTER TABLE [Product] ADD [Alias] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180303115534_ProdVersionEndOfSupport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180303115534_ProdVersionEndOfSupport', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'ClientCatalog');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Document] DROP COLUMN [ClientCatalog];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    CREATE TABLE [ClientCatalog] (
        [Id] int NOT NULL IDENTITY,
        [InternalId] nvarchar(max) NULL,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_ClientCatalog] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    CREATE TABLE [DocumentCatalog] (
        [Id] int NOT NULL IDENTITY,
        [CatalogId] int NOT NULL,
        [DocumentId] int NOT NULL,
        CONSTRAINT [PK_DocumentCatalog] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DocumentCatalog_ClientCatalog_CatalogId] FOREIGN KEY ([CatalogId]) REFERENCES [ClientCatalog] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DocumentCatalog_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Document] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    CREATE INDEX [IX_DocumentCatalog_CatalogId] ON [DocumentCatalog] ([CatalogId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    CREATE INDEX [IX_DocumentCatalog_DocumentId] ON [DocumentCatalog] ([DocumentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180318192011_ClientCatalogSeparateTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180318192011_ClientCatalogSeparateTable', N'2.0.1-rtm-125');
END;

GO

