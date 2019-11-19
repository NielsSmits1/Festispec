CREATE TABLE [dbo].[Inspectie] (
    [Inspectienummer] INT           IDENTITY (1, 1) NOT NULL,
    [StartDate]       DATETIME      NOT NULL,
    [EndDate]         DATETIME      NOT NULL,
    [Werknemer_ID]    INT           NOT NULL,
    [Klant_ID]        INT           NOT NULL,
    [Titel]           NVARCHAR (50) NOT NULL,
    [Versie]          NVARCHAR (50) NOT NULL,
    [Locatie_ID]      INT           NOT NULL,
    [Voltooid]        BIT           NOT NULL,
    CONSTRAINT [PK_Inspectie] PRIMARY KEY CLUSTERED ([Inspectienummer] ASC),
    CONSTRAINT [CHK_TitelNotNull] CHECK (datalength([Titel])>=(1)),
    CONSTRAINT [CHK_VersieNotNull] CHECK (datalength([Versie])>=(1)),
    CONSTRAINT [FK_Inspectie_Klant] FOREIGN KEY ([Klant_ID]) REFERENCES [dbo].[Klant] ([ID]),
    CONSTRAINT [FK_Inspectie_Locatie] FOREIGN KEY ([Locatie_ID]) REFERENCES [dbo].[Locatie] ([ID])
);

