CREATE TABLE [dbo].[Klant] (
    [ID]           NCHAR (10)    NOT NULL,
    [Bedrijfsnaam] NVARCHAR (50) NOT NULL,
    [NAW]          NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Klant] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Klant_NAW_Klant] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_Klant] ([ID])
);

