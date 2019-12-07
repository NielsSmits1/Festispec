CREATE TABLE [dbo].[Kaartvraag] (
    [ID]        INT             NOT NULL,
    [Vraag]     VARCHAR (50)    NOT NULL,
    [FileBytes] VARBINARY (MAX) NOT NULL,
    [MimeType]  VARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Kaartvraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_KaartvraagNotNull] CHECK (datalength([Vraag])>=(1))
);

