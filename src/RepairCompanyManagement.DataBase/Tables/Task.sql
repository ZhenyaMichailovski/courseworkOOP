CREATE TABLE [dbo].[Task] (
    [Id]                 INT           IDENTITY (1, 1) PRIMARY KEY     NOT NULL,
    [Title]              NVARCHAR (MAX)     NOT NULL,
    [IdSpecialization]   INT                NOT NULL,
    [Price]              FLOAT (53)         NOT NULL,
    [Description]        NVARCHAR (MAX)     NOT NULL,
    [IdBrigade] INT NOT NULL, 
    [Status] INT NOT NULL, 
);

