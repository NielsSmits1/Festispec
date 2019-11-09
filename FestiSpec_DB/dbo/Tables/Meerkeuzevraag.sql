CREATE TABLE [dbo].[Meerkeuzevraag] (
    [ID]    NCHAR (10)    NOT NULL,
    [Vraag] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Meerkeuzevraag] PRIMARY KEY CLUSTERED ([ID] ASC)
);

