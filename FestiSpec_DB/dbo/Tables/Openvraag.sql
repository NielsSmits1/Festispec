CREATE TABLE [dbo].[Openvraag] (
    [ID]    NCHAR (10)    NOT NULL,
    [Vraag] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Openvraag] PRIMARY KEY CLUSTERED ([ID] ASC)
);

