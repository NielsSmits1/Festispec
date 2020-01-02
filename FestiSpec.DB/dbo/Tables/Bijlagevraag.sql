CREATE TABLE [dbo].[Bijlagevraag] (
    [ID]    INT          IDENTITY (1, 1) NOT NULL,
    [Vraag] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Bijlagevraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_VraagNotNull] CHECK (datalength([Vraag])>=(1))
);

