CREATE TABLE [dbo].[Task] (
    [Id]                 INT                NOT NULL,
    [Title]              NVARCHAR (MAX)     NOT NULL,
    [IdSpecialization]   INT                NOT NULL,
    [Price]              FLOAT (53)         NOT NULL,
    [Description]        NVARCHAR (MAX)     NOT NULL,
    [TaskCompletionDate] DATETIMEOFFSET (7) NOT NULL, 
    CONSTRAINT [FK_Task_Specialization] FOREIGN KEY ([IdSpecialization]) REFERENCES dbo.[Specialization]([Id]),
);

