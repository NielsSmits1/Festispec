CREATE TABLE [dbo].[NAW_Klant] (
    [ID]               INT          IDENTITY (1, 1) NOT NULL,
    [Postcode]         VARCHAR (6)  NOT NULL,
    [Huisnummer]       VARCHAR (10) NOT NULL,
    [Straatnaam]       VARCHAR (50) NOT NULL,
    [KvkNummer]        VARCHAR (50) NOT NULL,
    [Vestigingsnummer] VARCHAR (12) NOT NULL,
    [IBAN]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_Klant] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_IncorrectZipcodeCustomer] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_KlantHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_KlantIBANNotNull] CHECK (datalength([IBAN])>=(1)),
    CONSTRAINT [CHK_KlantKvkNotNull] CHECK (datalength([KvkNummer])>=(1)),
    CONSTRAINT [CHK_KlantStraatnaamNotNull] CHECK (datalength([Straatnaam])>=(1)),
    CONSTRAINT [CHK_VestigingsNummerHas12Numbers] CHECK ([Vestigingsnummer] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

