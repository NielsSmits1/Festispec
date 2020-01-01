CREATE TABLE [dbo].[Applicant] (
    [ID]             INT          IDENTITY (1, 1) NOT NULL,
    [Voornaam]       VARCHAR (50) NOT NULL,
    [Tussenvoegsel]  VARCHAR (50) NULL,
    [Achternaam]     VARCHAR (50) NOT NULL,
    [Postcode]       VARCHAR (6)  NOT NULL,
    [Huisnummer]     VARCHAR (10) NOT NULL,
    [Straatnaam]     VARCHAR (50) NOT NULL,
    [Geboortedatum]  DATE         NOT NULL,
    [IBAN]           VARCHAR (50) NOT NULL,
    [Email]          VARCHAR (50) NOT NULL,
    [Telefoonnummer] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ID_Applicant] PRIMARY KEY CLUSTERED ([ID] ASC)
);

