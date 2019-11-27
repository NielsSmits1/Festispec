CREATE TABLE [dbo].[Telefoonnummer_inspecteur] (
    [Telefoonnummer]    NVARCHAR (50) NOT NULL,
    [NAW_Inspecteur_ID] INT           NOT NULL,
    CONSTRAINT [PK_Telefoonnummer_inspecteur] PRIMARY KEY CLUSTERED ([Telefoonnummer] ASC),
    CONSTRAINT [FK_Telefoonnummer_NAW_inspecteur] FOREIGN KEY ([NAW_Inspecteur_ID]) REFERENCES [dbo].[NAW_inspecteur] ([ID])
);

