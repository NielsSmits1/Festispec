CREATE TABLE [dbo].[Tabelvraag] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Vraag]       NVARCHAR (50) NOT NULL,
    [VraagKop]    NVARCHAR (50) NOT NULL,
    [AntwoordKop] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tabelvraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CHECK (datalength([AntwoordKop])>=(1)),
    CHECK (datalength([Vraag])>=(1)),
    CHECK (datalength([Vraagkop])>=(1))
);

