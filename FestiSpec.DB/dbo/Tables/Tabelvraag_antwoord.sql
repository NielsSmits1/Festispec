CREATE TABLE [dbo].[Tabelvraag_antwoord] (
    [Tabelvraag_ID]  INT          NOT NULL,
    [Situatie]       VARCHAR (50) NOT NULL,
    [Antwoord]       VARCHAR (50) NOT NULL,
    [Vragenlijst_ID] INT          NOT NULL,
    CONSTRAINT [PK_Tabelvraag_antwoord] PRIMARY KEY CLUSTERED ([Tabelvraag_ID] ASC, [Situatie] ASC, [Vragenlijst_ID] ASC),
    CHECK (datalength([Antwoord])>=(1)),
    CHECK (datalength([Situatie])>=(1)),
    CONSTRAINT [FK_Tabelvraag_antwoord_Tabelvraag] FOREIGN KEY ([Tabelvraag_ID]) REFERENCES [dbo].[Tabelvraag] ([ID])
);

