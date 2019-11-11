CREATE TABLE [dbo].[Kaartvraag] (
    [ID]        INT             NOT NULL,
    [Vraag]     NVARCHAR (50)   NOT NULL,
    [FileBytes] VARBINARY (MAX) NOT NULL,
    [MimeType]  NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Kaartvraag] PRIMARY KEY CLUSTERED ([ID] ASC)
);

