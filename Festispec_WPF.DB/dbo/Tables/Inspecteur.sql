CREATE TABLE [dbo].[Inspecteur] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Username]   NVARCHAR (50) NOT NULL,
    [Wachtwoord] NVARCHAR (50) NOT NULL,
    [NAW]        INT           NOT NULL,
    [Actief]     BIT           CONSTRAINT [DF_InsActief] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Inspecteur] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_UsernameNotNull] CHECK (datalength([Username])>=(1)),
    CONSTRAINT [CHK_WachtwoordNotNull] CHECK (datalength([Wachtwoord])>=(1)),
    CONSTRAINT [FK_Inspecteur_NAW_inspecteur] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_inspecteur] ([ID]),
    CONSTRAINT [UC_UsernameInspector] UNIQUE NONCLUSTERED ([Username] ASC)
);

