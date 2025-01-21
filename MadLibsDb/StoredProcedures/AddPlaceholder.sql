CREATE PROCEDURE AddPlaceholder
    @Id INT = NULL, -- Ignored parameter
    @StoryId INT,
    @PlaceholderText NVARCHAR(255),
    @PlaceholderIndex INT,
    @NewId INT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Placeholders] (StoryId, PlaceholderText, PlaceholderIndex)
    VALUES (@StoryId, @PlaceholderText, @PlaceholderIndex);

    -- Gets last id generated
    SET @NewId = SCOPE_IDENTITY();
END;
