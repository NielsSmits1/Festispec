CREATE TABLE [dbo].[Tabelvraag] (
    [ID]          NCHAR (10)    NOT NULL,
    [Vraag]       NVARCHAR (50) NOT NULL,
    [VraagKop]    NVARCHAR (50) NOT NULL,
    [AntwoordKop] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tabelvraag] PRIMARY KEY CLUSTERED ([ID] ASC)
);

