CREATE PROCEDURE AddPlaceholder
    @StoryId INT,
    @PlaceholderText NVARCHAR(255)
AS
BEGIN
    INSERT INTO Placeholders (StoryId, PlaceholderText)
    VALUES (@StoryId, @PlaceholderText);
END;
