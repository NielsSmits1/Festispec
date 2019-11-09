CREATE TABLE [dbo].[Beschikbaarheid] (
    [ID]            NCHAR (10) NOT NULL,
    [Inspecteur_ID] NCHAR (10) NOT NULL,
    [Datum]         DATE       NOT NULL,
    CONSTRAINT [PK_Beschikbaarheid] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Beschikbaarheid_Inspecteur] FOREIGN KEY ([Inspecteur_ID]) REFERENCES [dbo].[Inspecteur] ([ID])
);

