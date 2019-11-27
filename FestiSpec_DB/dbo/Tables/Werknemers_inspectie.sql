CREATE TABLE [dbo].[Werknemers_inspectie] (
    [Werknemer_ID]    INT NOT NULL,
    [Inspectienummer] INT NOT NULL,
    CONSTRAINT [PK_Werknemers_inspectie] PRIMARY KEY CLUSTERED ([Werknemer_ID] ASC, [Inspectienummer] ASC),
    CONSTRAINT [FK_Werknemers_inspectie_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer]),
    CONSTRAINT [FK_Werknemers_inspectie_Werknemer] FOREIGN KEY ([Werknemer_ID]) REFERENCES [dbo].[Werknemer] ([ID])
);

