CREATE TABLE [dbo].[Locatie] (
    [ID]         NCHAR (10)    NOT NULL,
    [Postcode]   NVARCHAR (6)  NOT NULL,
    [Huisnummer] NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Locatie] PRIMARY KEY CLUSTERED ([ID] ASC)
);

