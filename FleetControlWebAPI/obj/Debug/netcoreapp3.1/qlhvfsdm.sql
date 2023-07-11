IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [tbl_motor] (
    [motor_id] varchar(50) NOT NULL,
    [nome] varchar(150) NOT NULL,
    [cnh] varchar(10) NOT NULL,
    [validadeCNH] datetime NOT NULL,
    [ativo] bit NOT NULL,
    CONSTRAINT [PK_tbl_motor] PRIMARY KEY ([motor_id])
);

GO

CREATE TABLE [tbl_veicl] (
    [veicl_id] varchar(50) NOT NULL,
    [modelo] varchar(100) NULL,
    [placa] varchar(20) NULL,
    [ano] int NOT NULL,
    CONSTRAINT [PK_tbl_veicl] PRIMARY KEY ([veicl_id])
);

GO

CREATE TABLE [tbl_motr_veicl] (
    [motor_id] varchar(50) NOT NULL,
    [veicl_id] varchar(50) NOT NULL,
    CONSTRAINT [PK_tbl_motr_veicl] PRIMARY KEY ([veicl_id], [motor_id]),
    CONSTRAINT [FK_tbl_motr_veicl_tbl_motor_motor_id] FOREIGN KEY ([motor_id]) REFERENCES [tbl_motor] ([motor_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_tbl_motr_veicl_tbl_veicl_veicl_id] FOREIGN KEY ([veicl_id]) REFERENCES [tbl_veicl] ([veicl_id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_tbl_motr_veicl_motor_id] ON [tbl_motr_veicl] ([motor_id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230403020618_CriacaoDoBanco', N'3.1.19');

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] ON;
INSERT INTO [tbl_motor] ([motor_id], [ativo], [cnh], [nome], [validadeCNH])
VALUES ('dd761061-f588-4cbc-a06e-d88f7559c4f0', CAST(1 AS bit), '1234567891', 'Samuel Almeida', '2024-02-23T00:00:00.000'),
('24ee7c1d-8969-4094-8ff4-92d4c3a30c1a', CAST(1 AS bit), '9876549283', 'João da Silva', '2025-07-25T00:00:00.000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] ON;
INSERT INTO [tbl_veicl] ([veicl_id], [ano], [modelo], [placa])
VALUES ('fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100', 2019, 'Volvo FH 540', 'ABC1234'),
('0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3', 2018, 'Scania R450', 'CBA4321');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100', 'dd761061-f588-4cbc-a06e-d88f7559c4f0');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3', '24ee7c1d-8969-4094-8ff4-92d4c3a30c1a');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230404001547_InsertDeDados', N'3.1.19');

GO

DELETE FROM [tbl_motr_veicl]
WHERE [veicl_id] = '0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3' AND [motor_id] = '24ee7c1d-8969-4094-8ff4-92d4c3a30c1a';
SELECT @@ROWCOUNT;


GO

DELETE FROM [tbl_motr_veicl]
WHERE [veicl_id] = 'fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100' AND [motor_id] = 'dd761061-f588-4cbc-a06e-d88f7559c4f0';
SELECT @@ROWCOUNT;


GO

DELETE FROM [tbl_motor]
WHERE [motor_id] = '24ee7c1d-8969-4094-8ff4-92d4c3a30c1a';
SELECT @@ROWCOUNT;


GO

DELETE FROM [tbl_motor]
WHERE [motor_id] = 'dd761061-f588-4cbc-a06e-d88f7559c4f0';
SELECT @@ROWCOUNT;


GO

DELETE FROM [tbl_veicl]
WHERE [veicl_id] = '0ba3f5d5-c6c4-485c-aa95-b0dd3e0625e3';
SELECT @@ROWCOUNT;


GO

DELETE FROM [tbl_veicl]
WHERE [veicl_id] = 'fdf2bbd9-7cdf-4d71-98d2-94a32c9dd100';
SELECT @@ROWCOUNT;


GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_motor]') AND [c].[name] = N'cnh');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [tbl_motor] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [tbl_motor] ALTER COLUMN [cnh] varchar(30) NOT NULL;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] ON;
INSERT INTO [tbl_motor] ([motor_id], [ativo], [cnh], [nome], [validadeCNH])
VALUES ('dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65', CAST(1 AS bit), '1234567891', 'Samuel Almeida', '2024-02-23T00:00:00.000'),
('05df018c-4e86-4327-8f3a-6f72c8c15bc9', CAST(1 AS bit), '9876549283', 'João da Silva', '2025-07-25T00:00:00.000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] ON;
INSERT INTO [tbl_veicl] ([veicl_id], [ano], [modelo], [placa])
VALUES ('8d93e52d-36df-4c6b-9505-239a9ac54966', 2019, 'Volvo FH 540', 'ABC1234'),
('bfd9f2db-032a-4dbe-8a07-019aab9f29ae', 2018, 'Scania R450', 'CBA4321');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('8d93e52d-36df-4c6b-9505-239a9ac54966', 'dc1398ac-bf6b-4cd6-9d9b-b1a50c24ad65');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('bfd9f2db-032a-4dbe-8a07-019aab9f29ae', '05df018c-4e86-4327-8f3a-6f72c8c15bc9');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230404002031_InsertDadosTabelas', N'3.1.19');

GO

