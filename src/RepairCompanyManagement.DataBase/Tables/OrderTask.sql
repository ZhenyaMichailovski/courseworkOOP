CREATE TABLE [dbo].[OrderTask] (
    [Id]      INT NOT NULL,
    [IdTask]  INT NOT NULL,
    [IdOrder] INT NOT NULL,
    CONSTRAINT [PK__OrderTas__3214EC070FA614DB] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderTask_Task] FOREIGN KEY ([IdTask]) REFERENCES [dbo].[Task] ([Id]) ON DELETE CASCADE
);

