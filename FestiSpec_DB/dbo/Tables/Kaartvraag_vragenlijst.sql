CREATE TABLE [dbo].[Kaartvraag_vragenlijst] (
    [Vragenlijst_ID]    NCHAR (10)     NOT NULL,
    [Kaartvraag_ID]     NCHAR (10)     NOT NULL,
    [Filepath_Antwoord] NVARCHAR (MAX) NULL,
    [Opmerking]         NTEXT          NULL,
    [Positie]           NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_Kaartvraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Kaartvraag_ID] ASC),
    CONSTRAINT [FK_Kaartvraag_vragenlijst_Kaartvraag] FOREIGN KEY ([Kaartvraag_ID]) REFERENCES [dbo].[Kaartvraag] ([ID]),
    CONSTRAINT [FK_Kaartvraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

