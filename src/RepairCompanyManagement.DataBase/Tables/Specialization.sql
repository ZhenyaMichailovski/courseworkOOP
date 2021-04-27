CREATE TABLE [dbo].[Specialization] (
    [Id]          INT            IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
);

