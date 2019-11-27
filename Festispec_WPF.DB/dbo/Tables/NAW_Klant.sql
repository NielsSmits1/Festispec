CREATE TABLE [dbo].[NAW_Klant] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [Postcode]         NVARCHAR (6)  NOT NULL,
    [Huisnummer]       NVARCHAR (10) NOT NULL,
    [Plaatsnaam]       NVARCHAR (50) NOT NULL,
    [KvkNummer]        NVARCHAR (50) NOT NULL,
    [Vestigingsnummer] INT           NOT NULL,
    [IBAN]             NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_Klant] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_IncorrectZipcodeCustomer] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_KlantHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_KlantIBANNotNull] CHECK (datalength([IBAN])>=(1)),
    CONSTRAINT [CHK_KlantKvkNotNull] CHECK (datalength([KvkNummer])>=(1)),
    CONSTRAINT [CHK_KlantPlaatsnaamNotNull] CHECK (datalength([Plaatsnaam])>=(1)),
    CONSTRAINT [CHK_VestigingsNummerHas12Numbers] CHECK ([Vestigingsnummer] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

