CREATE TABLE [dbo].[OrderTask] (
    [Id]      INT IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [IdTask]  INT NOT NULL,
    [IdOrder] INT NOT NULL,
    [TaskCompletionDate] DATETIMEOFFSET (7) NOT NULL, 
    [Status] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL
);

