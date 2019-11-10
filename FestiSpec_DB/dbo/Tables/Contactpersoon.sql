CREATE TABLE [dbo].[Contactpersoon] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Klant_ID]      INT           NOT NULL,
    [Voornaam]      NVARCHAR (50) NOT NULL,
    [Tussenvoegsel] NVARCHAR (50) NULL,
    [Achernaam]     NVARCHAR (50) NOT NULL,
    [Telefoonummer] NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Contactpersoon] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Contactpersoon_Klant] FOREIGN KEY ([Klant_ID]) REFERENCES [dbo].[Klant] ([ID])
);

