CREATE PROCEDURE AddPlaceholder
    @StoryId INT,
    @PlaceholderText NVARCHAR(255),
    @PlaceholderIndex INT
AS
BEGIN
    INSERT INTO [dbo].[Placeholders] (StoryId, PlaceholderText, PlaceholderIndex)
    VALUES (@StoryId, @PlaceholderText, @PlaceholderIndex);
END;
