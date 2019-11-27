CREATE TABLE [dbo].[Locatie] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Postcode]   NVARCHAR (6)  NOT NULL,
    [Huisnummer] NVARCHAR (10) NOT NULL,
    [Plaatsnaam] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Locatie] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_IncorrectZipcodeLocation] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_LocHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_LocPlaatsnaamNotNull] CHECK (datalength([Plaatsnaam])>=(1))
);

