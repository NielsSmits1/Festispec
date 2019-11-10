CREATE TABLE [dbo].[Meerkeuzevraag_vragenlijst] (
    [Vragenlijst_ID]    INT           NOT NULL,
    [Meerkeuzevraag_ID] INT           NOT NULL,
    [Antwoord]          NVARCHAR (50) NULL,
    [Positie]           INT           NOT NULL,
    CONSTRAINT [PK_Meerkeuzevraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Meerkeuzevraag_ID] ASC),
    CONSTRAINT [FK_Meerkeuzevraag_vragenlijst_Meerkeuzevraag] FOREIGN KEY ([Meerkeuzevraag_ID]) REFERENCES [dbo].[Meerkeuzevraag] ([ID]),
    CONSTRAINT [FK_Meerkeuzevraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

