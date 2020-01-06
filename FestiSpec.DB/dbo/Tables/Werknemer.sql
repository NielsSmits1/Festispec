CREATE TABLE [dbo].[Werknemer] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [Rol]        VARCHAR (50) NOT NULL,
    [Username]   VARCHAR (50) NOT NULL,
    [Wachtwoord] VARCHAR (50) NOT NULL,
    [NAW]        INT          NOT NULL,
    [Actief]     BIT          CONSTRAINT [DF_WerkActief] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Werknemer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CHK_WerkUsernameNotNull] CHECK (datalength([Username])>=(1)),
    CONSTRAINT [CHK_WerkWachtwoordNotNull] CHECK (datalength([Wachtwoord])>=(1)),
    CONSTRAINT [FK_Werknemer_NAW_werknemer] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_werknemer] ([ID]),
    CONSTRAINT [FK_Werknemer_Rol_werknemer] FOREIGN KEY ([Rol]) REFERENCES [dbo].[Rol_werknemer] ([Rol]),
    CONSTRAINT [UC_UsernameEmployee] UNIQUE NONCLUSTERED ([Username] ASC)
);

