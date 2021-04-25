CREATE TABLE [dbo].[Employee] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [IdBrigade]      INT            NOT NULL,
    [Salary]         FLOAT (53)     NOT NULL,
    [IdJobPosition]  INT            NOT NULL,
    [IdentityUserID] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_Brigade] FOREIGN KEY ([IdBrigade]) REFERENCES [dbo].[Brigade] ([Id]),
    CONSTRAINT [FK_Employee_AspNetRoles] FOREIGN KEY ([IdentityUserID]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employee_JobPositionController] FOREIGN KEY ([IdJobPosition]) REFERENCES [dbo].[JobPositionController] ([Id]) ON DELETE CASCADE
);

