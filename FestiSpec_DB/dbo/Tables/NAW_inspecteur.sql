CREATE TABLE [dbo].[NAW_inspecteur] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Voornaam]      NVARCHAR (50) NOT NULL,
    [Tussenvoegsel] NVARCHAR (50) NULL,
    [Achternaam]    NVARCHAR (50) NOT NULL,
    [Postcode]      NVARCHAR (6)  NOT NULL,
    [Huisnummer]    NVARCHAR (50) NOT NULL,
    [Geboortedatum] DATE          NOT NULL,
    [IBAN]          NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_inspecteur] PRIMARY KEY CLUSTERED ([ID] ASC)
);

