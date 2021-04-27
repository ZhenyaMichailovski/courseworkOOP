CREATE TABLE [dbo].[Customer] (
    [Id]             INT            IDENTITY (1, 1)  PRIMARY KEY NOT NULL,
    [Gender]         NVARCHAR (MAX) NOT NULL,
    [IdentityUserID] NVARCHAR (128) NOT NULL,

);


