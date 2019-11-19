CREATE TABLE [dbo].[NAW_Klant] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Postcode]   NVARCHAR (6)  NOT NULL,
    [Huisnummer] NVARCHAR (1)  NOT NULL,
    [KvkNummer]  NVARCHAR (50) NOT NULL,
    [IBAN]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_NAW_Klant] PRIMARY KEY CLUSTERED ([ID] ASC)
);

