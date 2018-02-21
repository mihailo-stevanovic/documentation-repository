IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [DocumentType] (
    [Id] int NOT NULL IDENTITY,
    [DocumentCategory] int NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [ShortName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DocumentUpdate] (
    [Id] int NOT NULL IDENTITY,
    [DocumentId] int NOT NULL,
    [IsPublished] bit NOT NULL,
    [LatestTopicsUpdated] nvarchar(max) NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_DocumentUpdate] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [ShortName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductVersion] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Release] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ProductVersion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductVersion_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);

GO

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

GO

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

GO

CREATE INDEX [IX_Author_DocumentId] ON [Author] ([DocumentId]);

GO

CREATE INDEX [IX_Document_DocumentTypeId] ON [Document] ([DocumentTypeId]);

GO

CREATE INDEX [IX_Document_LatestUpdateId1] ON [Document] ([LatestUpdateId1]);

GO

CREATE INDEX [IX_Document_ProductVersionId] ON [Document] ([ProductVersionId]);

GO

CREATE INDEX [IX_ProductVersion_ProductId] ON [ProductVersion] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180221222553_InitialCreate', N'2.0.1-rtm-125');

GO

ALTER TABLE [Author] DROP CONSTRAINT [FK_Author_Document_DocumentId];

GO

DROP INDEX [IX_Author_DocumentId] ON [Author];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Author') AND [c].[name] = N'DocumentId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Author] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Author] DROP COLUMN [DocumentId];

GO

CREATE TABLE [DocumentAuthor] (
    [Id] int NOT NULL IDENTITY,
    [AuthorId] int NOT NULL,
    [DocumentId] int NOT NULL,
    CONSTRAINT [PK_DocumentAuthor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DocumentAuthor_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DocumentAuthor_Document_DocumentId] FOREIGN KEY ([DocumentId]) REFERENCES [Document] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_DocumentAuthor_AuthorId] ON [DocumentAuthor] ([AuthorId]);

GO

CREATE INDEX [IX_DocumentAuthor_DocumentId] ON [DocumentAuthor] ([DocumentId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180221224341_AddDocumentAuthor', N'2.0.1-rtm-125');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Document') AND [c].[name] = N'ParentDocumentId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Document] ALTER COLUMN [ParentDocumentId] int NULL;

GO

CREATE INDEX [IX_Document_ParentDocumentId] ON [Document] ([ParentDocumentId]);

GO

ALTER TABLE [Document] ADD CONSTRAINT [FK_Document_Document_ParentDocumentId] FOREIGN KEY ([ParentDocumentId]) REFERENCES [Document] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180221225036_AddParentDocument', N'2.0.1-rtm-125');

GO

