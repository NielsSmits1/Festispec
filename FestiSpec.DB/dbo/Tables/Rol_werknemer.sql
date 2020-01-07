CREATE TABLE [dbo].[Rol_werknemer] (
    [Rol] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Rol_werknemer] PRIMARY KEY CLUSTERED ([Rol] ASC),
    CONSTRAINT [CHK_RolNotNull] CHECK (datalength([Rol])>=(1))
);

