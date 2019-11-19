CREATE TABLE [dbo].[Telefoonnummer] (
    [Telefoonnummer]   NVARCHAR (50) NOT NULL,
    [NAW_Werknemer_ID] INT           NOT NULL,
    CONSTRAINT [PK_Telefoonnummer] PRIMARY KEY CLUSTERED ([Telefoonnummer] ASC),
    CONSTRAINT [FK_Telefoonnummer_NAW_werknemer] FOREIGN KEY ([NAW_Werknemer_ID]) REFERENCES [dbo].[NAW_werknemer] ([ID])
);

