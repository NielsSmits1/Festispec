CREATE TABLE [dbo].[Afspraak] (
    [ID]                NCHAR (10) NOT NULL,
    [Datum]             DATETIME   NOT NULL,
    [Contactpersoon_ID] NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Afspraak] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Afspraak_Contactpersoon] FOREIGN KEY ([Contactpersoon_ID]) REFERENCES [dbo].[Contactpersoon] ([ID])
);

