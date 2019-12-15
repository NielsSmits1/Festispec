CREATE TABLE [dbo].[Openvraag] (
    [ID]    INT          IDENTITY (1, 1) NOT NULL,
    [Vraag] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Openvraag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_OpenvraagNotNull] CHECK (datalength([Vraag])>=(1))
);

