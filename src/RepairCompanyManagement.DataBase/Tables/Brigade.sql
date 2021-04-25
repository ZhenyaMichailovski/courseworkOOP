CREATE TABLE [dbo].[Brigade] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (MAX) NOT NULL,
    [IdSpecialization] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Brigade_Brigade] FOREIGN KEY ([IdSpecialization]) REFERENCES [dbo].[Specialization] ([Id]) ON DELETE CASCADE
);

