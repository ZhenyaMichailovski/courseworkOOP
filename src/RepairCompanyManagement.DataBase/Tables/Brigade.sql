CREATE TABLE [dbo].[Brigade] (
    [Id]               INT            IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [Title]            NVARCHAR (MAX) NOT NULL,
    [IdSpecialization] INT            NOT NULL, 
);

