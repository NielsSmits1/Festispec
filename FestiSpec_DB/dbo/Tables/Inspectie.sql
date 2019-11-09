CREATE TABLE [dbo].[Inspectie] (
    [Inspectienummer] NCHAR (10)    NOT NULL,
    [StartDate]       DATETIME      NOT NULL,
    [EndDate]         DATETIME      NOT NULL,
    [Werknemer_ID]    NCHAR (10)    NOT NULL,
    [Klant_ID]        NCHAR (10)    NOT NULL,
    [Titel]           NVARCHAR (50) NOT NULL,
    [Versie]          NVARCHAR (50) NOT NULL,
    [Locatie_ID]      NCHAR (10)    NOT NULL,
    [Voltooid]        BIT           NOT NULL,
    CONSTRAINT [PK_Inspectie] PRIMARY KEY CLUSTERED ([Inspectienummer] ASC),
    CONSTRAINT [FK_Inspectie_Klant] FOREIGN KEY ([Klant_ID]) REFERENCES [dbo].[Klant] ([ID]),
    CONSTRAINT [FK_Inspectie_Locatie] FOREIGN KEY ([Locatie_ID]) REFERENCES [dbo].[Locatie] ([ID])
);

