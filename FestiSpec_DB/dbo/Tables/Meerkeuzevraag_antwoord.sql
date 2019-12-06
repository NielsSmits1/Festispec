CREATE TABLE [dbo].[Meerkeuzevraag_antwoord] (
    [Meerkeuzevraag_ID] INT          NOT NULL,
    [Antwoord]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Meerkeuzevraag_antwoord] PRIMARY KEY CLUSTERED ([Meerkeuzevraag_ID] ASC, [Antwoord] ASC),
    CONSTRAINT [CHK_MeerkeuzeAntwoordNotNull] CHECK (datalength([Antwoord])>=(1)),
    CONSTRAINT [FK_Meerkeuzevraag_antwoord_Meerkeuzevraag] FOREIGN KEY ([Meerkeuzevraag_ID]) REFERENCES [dbo].[Meerkeuzevraag] ([ID])
);

