CREATE TABLE [dbo].[Openvraag_vragenlijst] (
    [Vragenlijst_ID] INT           NOT NULL,
    [Openvraag_ID]   INT           NOT NULL,
    [Antwoord]       NVARCHAR (50) NULL,
    [Positie]        INT           NOT NULL,
    CONSTRAINT [PK_Openvraag_vragenlijst] PRIMARY KEY CLUSTERED ([Vragenlijst_ID] ASC, [Openvraag_ID] ASC),
    CONSTRAINT [FK_Openvraag_vragenlijst_Openvraag] FOREIGN KEY ([Openvraag_ID]) REFERENCES [dbo].[Openvraag] ([ID]),
    CONSTRAINT [FK_Openvraag_vragenlijst_Vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

