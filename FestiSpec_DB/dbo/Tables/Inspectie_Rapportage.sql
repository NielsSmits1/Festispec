CREATE TABLE [dbo].[Inspectie_Rapportage] (
    [ID]              NCHAR (10)    NOT NULL,
    [Titel]           NVARCHAR (50) NOT NULL,
    [Text]            NTEXT         NOT NULL,
    [Inspectienummer] NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Inspectie_Rapportage] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Inspectie_Rapportage_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer])
);

