CREATE TABLE [dbo].[Vragenlijst] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Titel]       NVARCHAR (50) NOT NULL,
    [Versie]      NVARCHAR (50) NOT NULL,
    [Template_ID] INT           NULL,
    [Opmerking]   NTEXT         NULL,
    [Is_Ingevuld] BIT           NOT NULL,
    CONSTRAINT [PK_Vragenlijst] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_VragenlijstTitelNotNull] CHECK (datalength([Titel])>=(1)),
    CONSTRAINT [CHK_VragenlijstVersieNotNull] CHECK (datalength([Versie])>=(1)),
    CONSTRAINT [FK_Vragenlijst_Template] FOREIGN KEY ([Template_ID]) REFERENCES [dbo].[Template] ([ID])
);

