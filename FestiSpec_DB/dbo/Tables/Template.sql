CREATE TABLE [dbo].[Template] (
    [ID]             INT   IDENTITY (1, 1) NOT NULL,
    [Type]           NTEXT NOT NULL,
    [Vragenlijst_ID] INT   NOT NULL,
    CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Template_ID_vragenlijst] FOREIGN KEY ([Vragenlijst_ID]) REFERENCES [dbo].[Vragenlijst] ([ID])
);

