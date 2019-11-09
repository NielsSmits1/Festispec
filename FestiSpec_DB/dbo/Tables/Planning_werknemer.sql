CREATE TABLE [dbo].[Planning_werknemer] (
    [Werknemer_ID] NCHAR (10) NOT NULL,
    [StartDate]    DATETIME   NOT NULL,
    [EndDate]      DATETIME   NOT NULL,
    CONSTRAINT [PK_Planning_werknemer] PRIMARY KEY CLUSTERED ([Werknemer_ID] ASC, [StartDate] ASC, [EndDate] ASC),
    CONSTRAINT [FK_Planning_werknemer_Werknemer] FOREIGN KEY ([Werknemer_ID]) REFERENCES [dbo].[Werknemer] ([ID])
);

