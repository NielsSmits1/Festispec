CREATE TABLE [dbo].[Tabelvraag_vragenlijst] (
    [Vragenlijst_ID] INT NOT NULL,
    [Tabelvraag_ID]  INT NOT NULL,
    [Positie]        INT NOT NULL,
    CONSTRAINT [PK_Tabelvraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Tabelvraag_ID] ASC),
    CONSTRAINT [FK_Tabelvraag_vragenlijst_Tabelvraag] FOREIGN KEY ([Tabelvraag_ID]) REFERENCES [dbo].[Tabelvraag] ([ID]),
    CONSTRAINT [FK_Tabelvraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

