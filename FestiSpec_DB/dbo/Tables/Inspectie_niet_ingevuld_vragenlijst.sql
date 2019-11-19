CREATE TABLE [dbo].[Inspectie_niet_ingevuld_vragenlijst] (
    [Inspectienummer] INT NOT NULL,
    [Vragenlijst_ID]  INT NOT NULL,
    CONSTRAINT [PK_Inspectie_niet_ingevuld_vragenlijst] PRIMARY KEY CLUSTERED ([Inspectienummer] ASC, [Vragenlijst_ID] ASC),
    CONSTRAINT [FK_Inspectie_niet_ingevuld_vragenlijst_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer]),
    CONSTRAINT [FK_Inspectie_niet_ingevuld_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

