CREATE TABLE [dbo].[Klant] (
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    [Bedrijfsnaam] VARCHAR (50) NOT NULL,
    [NAW]          INT          NOT NULL,
    CONSTRAINT [PK_Klant] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_BedrijfsnaamNotNull] CHECK (datalength([Bedrijfsnaam])>=(1)),
    CONSTRAINT [FK_Klant_NAW_Klant] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_Klant] ([ID])
);

