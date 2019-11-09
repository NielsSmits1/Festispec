CREATE TABLE [dbo].[NAW_werknemer] (
    [ID]            NCHAR (10)    NOT NULL,
    [Voornaam]      NVARCHAR (50) NOT NULL,
    [Tussenvoegsel] NVARCHAR (50) NULL,
    [Achternaam]    NVARCHAR (50) NOT NULL,
    [Postcode]      NVARCHAR (6)  NOT NULL,
    [Huisnummer]    NVARCHAR (10) NOT NULL,
    [GeboorteDatum] DATE          NOT NULL,
    [IBAN]          NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_werknemer] PRIMARY KEY CLUSTERED ([ID] ASC)
);

