CREATE TABLE [dbo].[Openvraag_vragenlijst] (
    [Vragenlijst_ID] NCHAR (10)    NOT NULL,
    [Openvraag_ID]   NCHAR (10)    NOT NULL,
    [Antwoord]       NVARCHAR (50) NULL,
    [Positie]        NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Openvraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Openvraag_ID] ASC),
    CONSTRAINT [FK_Openvraag_vragenlijst_Openvraag] FOREIGN KEY ([Openvraag_ID]) REFERENCES [dbo].[Openvraag] ([ID]),
    CONSTRAINT [FK_Openvraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

