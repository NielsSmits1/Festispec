CREATE TABLE [dbo].[Tabelvraag_antwoord] (
    [Tabelvraag_ID]  NCHAR (10)    NOT NULL,
    [Situatie]       NVARCHAR (50) NOT NULL,
    [Antwoord]       NVARCHAR (50) NOT NULL,
    [Vragenlijst_ID] NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Tabelvraag_antwoord] PRIMARY KEY CLUSTERED ([Tabelvraag_ID] ASC, [Situatie] ASC, [Vragenlijst_ID] ASC),
    CONSTRAINT [FK_Tabelvraag_antwoord_Tabelvraag] FOREIGN KEY ([Tabelvraag_ID]) REFERENCES [dbo].[Tabelvraag] ([ID])
);

