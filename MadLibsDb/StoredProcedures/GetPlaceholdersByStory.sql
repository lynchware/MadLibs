CREATE PROCEDURE GetPlaceholdersByStory
    @StoryId INT
AS
BEGIN
    SELECT * FROM Placeholders WHERE StoryId = @StoryId;
END
