CREATE TABLE [dbo].[Vragenlijst] (
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    [Titel]           VARCHAR (50) NOT NULL,
    [Versie]          VARCHAR (50) NOT NULL,
    [Template_ID]     INT          NULL,
    [Stamt_af_van_ID] INT          NULL,
    [Opmerking]       NTEXT        NULL,
    [Is_Ingevuld]     BIT          NOT NULL,
    [Actief]          BIT          NOT NULL,
    CONSTRAINT [PK_Vragenlijst] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_VragenlijstTitelNotNull] CHECK (datalength([Titel])>=(1)),
    CONSTRAINT [CHK_VragenlijstVersieNotNull] CHECK (datalength([Versie])>=(1)),
    CONSTRAINT [FK_SelfReference_Vragenlijst] FOREIGN KEY ([Stamt_af_van_ID]) REFERENCES [dbo].[Vragenlijst] ([ID]),
    CONSTRAINT [FK_Vragenlijst_Template] FOREIGN KEY ([Template_ID]) REFERENCES [dbo].[Template] ([ID])
);

