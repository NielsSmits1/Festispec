CREATE TABLE [dbo].[Verzoek] (
    [ID]                   NCHAR (10) NOT NULL,
    [Klant_ID]             NCHAR (10) NOT NULL,
    [Verzoek_geaccepteerd] BIT        NOT NULL,
    [Aanvraag]             NTEXT      NOT NULL,
    CONSTRAINT [PK_Verzoek] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Verzoek_Klant] FOREIGN KEY ([Klant_ID]) REFERENCES [dbo].[Klant] ([ID])
);

