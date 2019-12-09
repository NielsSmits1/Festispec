CREATE TABLE [dbo].[Bijlagevraag_vragenlijst] (
    [Bijlagevraag_ID] INT             NOT NULL,
    [Vragenlijst_ID]  INT             NOT NULL,
    [FileBytes]       VARBINARY (MAX) NULL,
    [Positie]         INT             NOT NULL,
    CONSTRAINT [PK_Bijlagevraag_vragenlijst] PRIMARY KEY CLUSTERED ([Bijlagevraag_ID] ASC, [Vragenlijst_ID] ASC),
    CONSTRAINT [FK_Bijlagevraag_vragenlijst_Bijlagevraag] FOREIGN KEY ([Bijlagevraag_ID]) REFERENCES [dbo].[Bijlagevraag] ([ID]),
    CONSTRAINT [FK_Bijlagevraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

