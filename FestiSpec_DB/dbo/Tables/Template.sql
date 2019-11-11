CREATE TABLE [dbo].[Template] (
    [ID]   INT   IDENTITY (1, 1) NOT NULL,
    [Type] NTEXT NOT NULL,
    CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([ID] ASC)
);

