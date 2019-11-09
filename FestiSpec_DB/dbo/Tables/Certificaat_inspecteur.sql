CREATE TABLE [dbo].[Certificaat_inspecteur] (
    [Inspecteur]     NCHAR (10) NOT NULL,
    [Certificaat_ID] NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Certificaat_inspecteur] PRIMARY KEY CLUSTERED ([Inspecteur] ASC, [Certificaat_ID] ASC),
    CONSTRAINT [FK_Certificaat_inspecteur_Certificaat] FOREIGN KEY ([Certificaat_ID]) REFERENCES [dbo].[Certificaat] ([ID]),
    CONSTRAINT [FK_Certificaat_inspecteur_Inspecteur] FOREIGN KEY ([Inspecteur]) REFERENCES [dbo].[Inspecteur] ([ID])
);

