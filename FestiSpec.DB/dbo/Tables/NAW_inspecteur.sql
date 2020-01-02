CREATE TABLE [dbo].[NAW_inspecteur] (
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
    CONSTRAINT [PK_NAW_inspecteur] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_BirthdateInFuture] CHECK (getdate()>[Geboortedatum]),
    CONSTRAINT [CHK_IncorrectZipcodeInspector] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_InsAchternaamNotNull] CHECK (datalength([Achternaam])>=(1)),
    CONSTRAINT [CHK_InsEmailNotNull] CHECK (datalength([Email])>=(1)),
    CONSTRAINT [CHK_InsHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_InsIBANNotNull] CHECK (datalength([IBAN])>=(1)),
    CONSTRAINT [CHK_InsStraatnaamNotNull] CHECK (datalength([Straatnaam])>=(1)),
    CONSTRAINT [CHK_InsVoornaamNotNull] CHECK (datalength([Voornaam])>=(1))
);

