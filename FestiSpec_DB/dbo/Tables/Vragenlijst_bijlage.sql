CREATE TABLE [dbo].[Vragenlijst_bijlage] (
    [Bijlage_ID]     INT             NOT NULL,
    [Vragenlijst_ID] INT             NOT NULL,
    [FileBytes]      VARBINARY (MAX) NULL,
    [MimeType]       NVARCHAR (50)   NULL,
    CONSTRAINT [PK_Vragenlijst_bijlage] PRIMARY KEY CLUSTERED ([Bijlage_ID] ASC, [Vragenlijst_ID] ASC),
    CONSTRAINT [FK_Vragenlijst_bijlage_Bijlagevraag] FOREIGN KEY ([Bijlage_ID]) REFERENCES [dbo].[Bijlagevraag] ([ID]),
    CONSTRAINT [FK_Vragenlijst_bijlage_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

