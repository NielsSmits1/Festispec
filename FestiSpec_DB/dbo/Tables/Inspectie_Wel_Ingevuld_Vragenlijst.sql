CREATE TABLE [dbo].[Inspectie_Wel_Ingevuld_Vragenlijst] (
    [Inspectienummer] INT NOT NULL,
    [Vragenlijst_ID]  INT NOT NULL,
    [Inspecteur_ID]   INT NOT NULL,
    CONSTRAINT [PK_Inspectie_Wel_Ingevuld_Vragenlijst] PRIMARY KEY CLUSTERED ([Inspectienummer] ASC, [Vragenlijst_ID] ASC, [Inspecteur_ID] ASC),
    CONSTRAINT [FK_Inspectie_Wel_Ingevuld_Vragenlijst_Inspecteur] FOREIGN KEY ([Inspecteur_ID]) REFERENCES [dbo].[Inspecteur] ([ID]),
    CONSTRAINT [FK_Inspectie_Wel_Ingevuld_Vragenlijst_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer]),
    CONSTRAINT [FK_Inspectie_Wel_Ingevuld_Vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

