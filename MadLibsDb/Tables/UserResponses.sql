CREATE TABLE [dbo].[UserResponses]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[StoryId] INT NOT NULL,
	[PlaceholderId] INT NOT NULL,
	[Word] NVARCHAR(50) NOT NULL,
	CONSTRAINT FK_UserResponses_Stories FOREIGN KEY (StoryId) REFERENCES Stories(Id),
	CONSTRAINT FK_UserResponses_Placeholders FOREIGN KEY (PlaceholderId) REFERENCES Placeholders(Id)
)
