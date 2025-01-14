CREATE TABLE [dbo].[Theme]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Theme] VARCHAR(55),
    [ImagePath] NVARCHAR(MAX) NOT NULL,
);
