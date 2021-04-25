CREATE TABLE [dbo].[Customer] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Gender]         NVARCHAR (MAX) NOT NULL,
    [IdentityUserID] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


