CREATE TABLE [dbo].[Employee] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [IdBrigade]      INT            NOT NULL,
    [Salary]         FLOAT (53)     NOT NULL,
    [IdJobPosition]  INT            NOT NULL,
    [IdentityUserID] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Employee_JobPosition] FOREIGN KEY ([IdJobPosition]) REFERENCES dbo.[JobPositionController](Id), 
    CONSTRAINT [FK_Employee_Brigade] FOREIGN KEY ([IdBrigade]) REFERENCES dbo.[Brigade]([Id]),
);

