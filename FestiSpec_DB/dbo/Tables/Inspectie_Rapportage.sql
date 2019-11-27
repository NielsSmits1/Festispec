CREATE TABLE [dbo].[Inspectie_Rapportage] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Titel]           NVARCHAR (50) NOT NULL,
    [Text]            NTEXT         NOT NULL,
    [Inspectienummer] INT           NOT NULL,
    CONSTRAINT [PK_Inspectie_Rapportage] PRIMARY KEY CLUSTERED ([ID] ASC),
    CHECK (datalength([Titel])>=(1)),
    CONSTRAINT [FK_Inspectie_Rapportage_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer])
);

