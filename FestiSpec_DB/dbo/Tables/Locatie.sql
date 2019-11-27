CREATE TABLE [dbo].[Locatie] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Postcode]   NVARCHAR (6)  NOT NULL,
    [Huisnummer] NVARCHAR (10) NOT NULL,
    [Straatnaam] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Locatie] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_IncorrectZipcodeLocation] CHECK ([Postcode] like '[0-9][0-9][0-9][0-9][A-Z][A-Z]'),
    CONSTRAINT [CHK_LocHuisnummerNotNull] CHECK (datalength([Huisnummer])>=(1)),
    CONSTRAINT [CHK_LocStraatnaamNotNull] CHECK (datalength([Straatnaam])>=(1)),
    CONSTRAINT [UC_LocationData] UNIQUE NONCLUSTERED ([Postcode] ASC, [Huisnummer] ASC, [Straatnaam] ASC)
);

