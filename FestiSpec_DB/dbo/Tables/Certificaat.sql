CREATE TABLE [dbo].[Certificaat] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Certificaat] PRIMARY KEY CLUSTERED ([ID] ASC)
);

