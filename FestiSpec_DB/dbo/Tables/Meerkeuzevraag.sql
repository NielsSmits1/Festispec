CREATE TABLE [dbo].[Meerkeuzevraag] (
    [ID]    INT          IDENTITY (1, 1) NOT NULL,
    [Vraag] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Meerkeuzevraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_MeerkeuzevraagNotNull] CHECK (datalength([Vraag])>=(1))
);

