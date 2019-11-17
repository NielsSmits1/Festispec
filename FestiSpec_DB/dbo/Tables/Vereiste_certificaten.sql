CREATE TABLE [dbo].[Vereiste_certificaten] (
    [Inspectienummer] INT NOT NULL,
    [Certificaat_ID]  INT NOT NULL,
    CONSTRAINT [PK_Vereiste_certificaten] PRIMARY KEY CLUSTERED ([Inspectienummer] ASC, [Certificaat_ID] ASC),
    CONSTRAINT [FK_Vereiste_certificaten_Certificaat] FOREIGN KEY ([Certificaat_ID]) REFERENCES [dbo].[Certificaat] ([ID]),
    CONSTRAINT [FK_Vereiste_certificaten_Inspectie] FOREIGN KEY ([Inspectienummer]) REFERENCES [dbo].[Inspectie] ([Inspectienummer])
);

