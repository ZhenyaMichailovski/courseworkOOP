CREATE TABLE [dbo].[Manager] (
    [Id]             INT                IDENTITY (1, 1) PRIMARY KEY NOT NULL,
    [DateOfBirth]    DATETIMEOFFSET (7) NOT NULL,
    [Address]        NVARCHAR (MAX)     NOT NULL,
    [Salary]         FLOAT (53)         NOT NULL,
    [IdentityUserID] NVARCHAR (128)     NOT NULL,
);

