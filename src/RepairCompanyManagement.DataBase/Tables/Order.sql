CREATE TABLE [dbo].[Order] (
    [Id]           INT     IDENTITY(1, 1) PRIMARY KEY       NOT NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [IdCustomers]  INT            NOT NULL,
    [OrderStatus]  INT NOT NULL,
    [Requirements] NVARCHAR (MAX) NOT NULL, 
);

