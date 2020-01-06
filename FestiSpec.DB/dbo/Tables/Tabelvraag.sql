CREATE TABLE [dbo].[Tabelvraag] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Vraag]       VARCHAR (50) NOT NULL,
    [VraagKop]    VARCHAR (50) NOT NULL,
    [AntwoordKop] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tabelvraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CHECK (datalength([AntwoordKop])>=(1)),
    CHECK (datalength([Vraag])>=(1)),
    CHECK (datalength([Vraagkop])>=(1))
);

