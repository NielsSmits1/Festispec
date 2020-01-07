CREATE TABLE [dbo].[Contactpersoon] (
    [ID]             INT          IDENTITY (1, 1) NOT NULL,
    [Klant_ID]       INT          NOT NULL,
    [Voornaam]       VARCHAR (50) NOT NULL,
    [Tussenvoegsel]  VARCHAR (50) NULL,
    [Achternaam]     VARCHAR (50) NOT NULL,
    [Telefoonnummer] VARCHAR (50) NOT NULL,
    [Email]          VARCHAR (50) NOT NULL,
    [Actief]         BIT          NOT NULL,
    CONSTRAINT [PK_Contactpersoon] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_AchternaamNotNull] CHECK (datalength([Achternaam])>=(1)),
    CONSTRAINT [CHK_ContactpersoonNotNull] CHECK (datalength([Voornaam])>=(1)),
    CONSTRAINT [CHK_EmailNotNull] CHECK (datalength([Email])>=(1)),
    CONSTRAINT [CHK_TelNotNull] CHECK (datalength([Telefoonnummer])>=(1)),
    CONSTRAINT [FK_Contactpersoon_Klant] FOREIGN KEY ([Klant_ID]) REFERENCES [dbo].[Klant] ([ID])
);

