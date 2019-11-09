CREATE TABLE [dbo].[Offerte] (
    [ID]         NCHAR (10)      NOT NULL,
    [Verzoek_ID] NCHAR (10)      NOT NULL,
    [Datum]      DATE            NOT NULL,
    [Bedrag]     DECIMAL (15, 2) NOT NULL,
    [Tekst]      NTEXT           NOT NULL,
    CONSTRAINT [PK_Offerte] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Offerte_Verzoek] FOREIGN KEY ([Verzoek_ID]) REFERENCES [dbo].[Verzoek] ([ID])
);

