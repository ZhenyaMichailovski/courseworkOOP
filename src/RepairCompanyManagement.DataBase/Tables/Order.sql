CREATE TABLE [dbo].[Order] (
    [Id]           INT            NOT NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [IdBrigade]    INT            NOT NULL,
    [IdCustomers]  INT            NOT NULL,
    [IdManager]    INT            NOT NULL,
    [OrderStatus]  NVARCHAR (MAX) NOT NULL,
    [Requirements] NVARCHAR (MAX) NOT NULL, 
    CONSTRAINT [FK_Order_Brigade] FOREIGN KEY (IdBrigade) REFERENCES dbo.[Brigade]([Id]),
    CONSTRAINT [FK_Order_Customers] FOREIGN KEY (IdCustomers) REFERENCES dbo.[Customer]([Id]),
    CONSTRAINT [FK_Order_Manager] FOREIGN KEY (IdManager) REFERENCES dbo.[Manager]([Id]),
);

