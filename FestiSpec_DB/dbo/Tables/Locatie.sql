CREATE TABLE [dbo].[Locatie] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [Postcode]   NVARCHAR (6) NOT NULL,
    [Huisnummer] NVARCHAR (1) NOT NULL,
    CONSTRAINT [PK_Locatie] PRIMARY KEY CLUSTERED ([ID] ASC)
);

