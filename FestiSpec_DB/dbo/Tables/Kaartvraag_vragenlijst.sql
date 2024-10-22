﻿CREATE TABLE [dbo].[Kaartvraag_vragenlijst] (
    [Vragenlijst_ID]    INT           NOT NULL,
    [Kaartvraag_ID]     INT           NOT NULL,
    [Filepath_Antwoord] VARCHAR (MAX) NULL,
    [Opmerking]         NTEXT         NULL,
    [Positie]           INT           NOT NULL,
    CONSTRAINT [PK_Kaartvraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Kaartvraag_ID] ASC),
    CONSTRAINT [FK_Kaartvraag_vragenlijst_Kaartvraag] FOREIGN KEY ([Kaartvraag_ID]) REFERENCES [dbo].[Kaartvraag] ([ID]),
    CONSTRAINT [FK_Kaartvraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

