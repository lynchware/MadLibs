﻿CREATE TABLE [dbo].[Placeholders]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[StoryId] INT NOT NULL,
	[Placeholder] NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_StoriesPlaceholders FOREIGN KEY ([StoryId]) REFERENCES [dbo].[Stories]([Id])
)
