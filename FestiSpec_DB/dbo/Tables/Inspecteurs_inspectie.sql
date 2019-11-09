CREATE TABLE [dbo].[Inspecteurs_inspectie] (
    [Inspecteur_ID]   NCHAR (10) NOT NULL,
    [Inspectienummer] NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Inspecteurs_inspectie] PRIMARY KEY CLUSTERED ([Inspecteur_ID] ASC, [Inspectienummer] ASC),
    CONSTRAINT [FK_Inspecteurs_inspectie_Inspecteur] FOREIGN KEY ([Inspecteur_ID]) REFERENCES [dbo].[Inspecteur] ([ID]),
    CONSTRAINT [FK_Inspecteurs_inspectie_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer])
);

