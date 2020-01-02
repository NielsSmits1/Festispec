CREATE TABLE [dbo].[Applicant_certificaat] (
    [Applicant_ID]   INT NOT NULL,
    [Certificaat_ID] INT NOT NULL,
    CONSTRAINT [PK_Applicant_Certificaat] PRIMARY KEY CLUSTERED ([Applicant_ID] ASC, [Certificaat_ID] ASC),
    CONSTRAINT [FK_Applicant_certificaat_applicant] FOREIGN KEY ([Applicant_ID]) REFERENCES [dbo].[Applicant] ([ID]),
    CONSTRAINT [FK_Applicant_certificaat_certificaat] FOREIGN KEY ([Certificaat_ID]) REFERENCES [dbo].[Certificaat] ([ID])
);

