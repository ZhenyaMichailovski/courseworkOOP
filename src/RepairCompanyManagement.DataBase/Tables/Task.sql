CREATE TABLE [dbo].[Task] (
    [Id]                 INT           IDENTITY (1, 1) PRIMARY KEY     NOT NULL,
    [Title]              NVARCHAR (50)     NOT NULL,
    [IdSpecialization]   INT                NOT NULL,
    [Price]              FLOAT (53)         NOT NULL,
    [Description]        NVARCHAR (150)     NOT NULL,
    [IdBrigade] INT NOT NULL, 

);

