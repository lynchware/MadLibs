CREATE PROCEDURE SaveUserResponseWithValidation
    @StoryId INT,
    @PlaceholderId INT,
    @PlaceholderIndex INT,
    @Word NVARCHAR(50),
    @BatchId INT,
    @BatchName NVARCHAR(50),
    @NewId INT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the Word parameter is empty or null
    IF @Word IS NULL OR @Word = ''
    BEGIN
        SET @Message = 'Response cannot be empty for Placeholder ' + CAST(@PlaceholderIndex AS NVARCHAR(10));
        RETURN;
    END

    IF EXISTS(SELECT 1 FROM UserResponses WHERE StoryId = @StoryId AND PlaceholderIndex = @PlaceholderIndex)
    BEGIN
        UPDATE UserResponses
        SET Word = @Word
        WHERE StoryId = @StoryId AND PlaceholderIndex = @PlaceholderIndex;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[UserResponses] ([StoryId], [PlaceholderId], [PlaceholderIndex], [Word], [BatchId], [BatchName])
        VALUES (@StoryId, @PlaceholderId, @PlaceholderIndex, @Word, @BatchId, @BatchName);

        SET @NewId = SCOPE_IDENTITY();
    END

    SET @Message = 'User response saved successfully.';
END;

