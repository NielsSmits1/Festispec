CREATE TABLE [dbo].[Inspecteur] (
    [ID]             NCHAR (10)    NOT NULL,
    [Gebruikersnaam] NVARCHAR (50) NOT NULL,
    [Wachtwoord]     NVARCHAR (50) NOT NULL,
    [NAW]            NCHAR (10)    NOT NULL,
    [Actief]         BIT           NOT NULL,
    CONSTRAINT [PK_Inspecteur] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Inspecteur_NAW_inspecteur] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_inspecteur] ([ID])
);

