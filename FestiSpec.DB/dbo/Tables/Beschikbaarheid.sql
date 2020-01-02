CREATE TABLE [dbo].[Beschikbaarheid] (
    [ID]            INT  IDENTITY (1, 1) NOT NULL,
    [Inspecteur_ID] INT  NOT NULL,
    [Datum]         DATE NOT NULL,
    CONSTRAINT [PK_Beschikbaarheid] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Beschikbaarheid_Inspecteur] FOREIGN KEY ([Inspecteur_ID]) REFERENCES [dbo].[Inspecteur] ([ID])
);

