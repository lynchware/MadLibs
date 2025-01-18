CREATE TABLE [dbo].[UserResponses]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[StoryId] INT NOT NULL,
	[PlaceholderId] INT NOT NULL,
    [PlaceholderIndex] INT           DEFAULT ((0)) NOT NULL,
	[Word] NVARCHAR(50) NOT NULL,
	[BatchId] INT NOT NULL,
	[BatchName] NVARCHAR(50) NULL,
	CONSTRAINT FK_UserResponses_Stories FOREIGN KEY (StoryId) REFERENCES Stories(Id),
	CONSTRAINT FK_UserResponses_Placeholders FOREIGN KEY (PlaceholderId) REFERENCES Placeholders(Id)
)
