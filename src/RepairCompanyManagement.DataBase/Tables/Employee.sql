CREATE TABLE [dbo].[Employee] (
    [Id]             INT            IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [IdBrigade]      INT            NOT NULL,
    [Salary]         DECIMAL(18, 2)     NOT NULL,
    [IdJobPosition]  INT            NOT NULL,
    [IdentityUserID] NVARCHAR (128) NOT NULL,

);

