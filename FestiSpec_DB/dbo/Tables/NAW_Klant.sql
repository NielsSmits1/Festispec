CREATE TABLE [dbo].[NAW_Klant] (
    [ID]         NCHAR (10)    NOT NULL,
    [Postcode]   NVARCHAR (6)  NOT NULL,
    [Huisnummer] NVARCHAR (10) NOT NULL,
    [KvkNummer]  NVARCHAR (50) NOT NULL,
    [IBAN]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_Klant] PRIMARY KEY CLUSTERED ([ID] ASC)
);

