CREATE TABLE [dbo].[Manager] (
    [Id]             INT                IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [Salary]         DECIMAL(18, 2)         NOT NULL,
    [IdentityUserID] NVARCHAR (128)     NOT NULL,
);

