CREATE TABLE [tbl_motor] (
    [motor_id] varchar(50) NOT NULL,
    [nome] varchar(150) NOT NULL,
    [cnh] varchar(30) NOT NULL,
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


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] ON;
INSERT INTO [tbl_motor] ([motor_id], [ativo], [cnh], [nome], [validadeCNH])
VALUES ('f40a488c-624f-4b7a-b8e3-7d7de86e5f31', CAST(1 AS bit), '1234567891', 'Samuel Almeida', '2024-02-23T00:00:00.000'),
('4df75800-4d9a-4f36-a4a3-8f138cc998af', CAST(1 AS bit), '9876549283', 'João da Silva', '2025-07-25T00:00:00.000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'motor_id', N'ativo', N'cnh', N'nome', N'validadeCNH') AND [object_id] = OBJECT_ID(N'[tbl_motor]'))
    SET IDENTITY_INSERT [tbl_motor] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] ON;
INSERT INTO [tbl_veicl] ([veicl_id], [ano], [modelo], [placa])
VALUES ('ceb4b390-bf00-4043-917d-499fbbfa9b22', 2019, 'Volvo FH 540', 'ABC1234'),
('18f09e13-bc1b-49d6-bc05-8f3a62331a83', 2018, 'Scania R450', 'CBA4321');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'ano', N'modelo', N'placa') AND [object_id] = OBJECT_ID(N'[tbl_veicl]'))
    SET IDENTITY_INSERT [tbl_veicl] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('ceb4b390-bf00-4043-917d-499fbbfa9b22', 'f40a488c-624f-4b7a-b8e3-7d7de86e5f31');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] ON;
INSERT INTO [tbl_motr_veicl] ([veicl_id], [motor_id])
VALUES ('18f09e13-bc1b-49d6-bc05-8f3a62331a83', '4df75800-4d9a-4f36-a4a3-8f138cc998af');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'veicl_id', N'motor_id') AND [object_id] = OBJECT_ID(N'[tbl_motr_veicl]'))
    SET IDENTITY_INSERT [tbl_motr_veicl] OFF;
GO


CREATE INDEX [IX_tbl_motr_veicl_motor_id] ON [tbl_motr_veicl] ([motor_id]);
GO


