CREATE TABLE [dbo].[NAW_werknemer] (
    [ID]             INT          IDENTITY (1, 1) NOT NULL,
    [Voornaam]       VARCHAR (50) NOT NULL,
    [Tussenvoegsel]  VARCHAR (50) NULL,
    [Achternaam]     VARCHAR (50) NOT NULL,
    [Postcode]       VARCHAR (6)  NOT NULL,
    [Huisnummer]     VARCHAR (10) NOT NULL,
    [Straatnaam]     VARCHAR (50) NOT NULL,
    [GeboorteDatum]  DATE         NOT NULL,
    [IBAN]           VARCHAR (50) NOT NULL,
    [Email]          VARCHAR (50) NOT NULL,
    [Telefoonnummer] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_werknemer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_BirthdateInFutureEmployee] CHECK (getdate()>[Geboortedatum]),
    CONSTRAINT [CHK_IncorrectZipcodeEmployee] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_WerkAchternaamNotNull] CHECK (datalength([Achternaam])>=(1)),
    CONSTRAINT [CHK_WerkEmailNotNull] CHECK (datalength([Email])>=(1)),
    CONSTRAINT [CHK_WerkHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_WerkIBANNotNull] CHECK (datalength([IBAN])>=(1)),
    CONSTRAINT [CHK_WerkStraatnaamNotNull] CHECK (datalength([Straatnaam])>=(1)),
    CONSTRAINT [CHK_WerkVoornaamNotNull] CHECK (datalength([Voornaam])>=(1))
);

