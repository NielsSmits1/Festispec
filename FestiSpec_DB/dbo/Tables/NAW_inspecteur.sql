CREATE TABLE [dbo].[NAW_inspecteur] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [Voornaam]       NVARCHAR (50) NOT NULL,
    [Tussenvoegsel]  NVARCHAR (50) NULL,
    [Achternaam]     NVARCHAR (50) NOT NULL,
    [Postcode]       NVARCHAR (6)  NOT NULL,
    [Huisnummer]     NVARCHAR (10) NOT NULL,
    [Straatnaam]     NVARCHAR (50) NOT NULL,
    [Geboortedatum]  DATE          NOT NULL,
    [IBAN]           NVARCHAR (50) NOT NULL,
    [Email]          NVARCHAR (50) NOT NULL,
    [Telefoonnummer] NVARCHAR (50) NOT NULL,
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

