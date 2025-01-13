CREATE TABLE [dbo].[Theme]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Theme] VARCHAR(12),
    [StoryId] INT, -- Foreign key to Stories
    CONSTRAINT FK_Theme_Stories FOREIGN KEY (StoryId) REFERENCES [dbo].[Stories](Id)
);
