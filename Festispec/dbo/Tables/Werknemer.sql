﻿CREATE TABLE [dbo].[Werknemer] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Rol]        NVARCHAR (50) NOT NULL,
    [Username]   NVARCHAR (50) NOT NULL,
    [Wachtwoord] NVARCHAR (50) NOT NULL,
    [NAW]        INT           NOT NULL,
    [Actief]     BIT           NOT NULL,
    CONSTRAINT [PK_Werknemer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Werknemer_NAW_werknemer] FOREIGN KEY ([NAW]) REFERENCES [dbo].[NAW_werknemer] ([ID]),
    CONSTRAINT [FK_Werknemer_Rol_werknemer] FOREIGN KEY ([Rol]) REFERENCES [dbo].[Rol_werknemer] ([Rol])
);
