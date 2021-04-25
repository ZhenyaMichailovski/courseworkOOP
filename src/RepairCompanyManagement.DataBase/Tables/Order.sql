CREATE TABLE [dbo].[Order] (
    [Id]           INT            NOT NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [IdBrigade]    INT            NOT NULL,
    [IdCustomers]  INT            NOT NULL,
    [IdManager]    INT            NOT NULL,
    [OrderStatus]  NVARCHAR (MAX) NOT NULL,
    [Requirements] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK__Order__3214EC07295DD3E4] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Brigade] FOREIGN KEY ([IdBrigade]) REFERENCES [dbo].[Brigade] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Customer] FOREIGN KEY ([IdCustomers]) REFERENCES [dbo].[Customer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Manager] FOREIGN KEY ([IdManager]) REFERENCES [dbo].[Manager] ([Id]) ON DELETE CASCADE
);

