CREATE PROCEDURE SaveUserResponse
    @StoryId INT,
    @PlaceholderId INT,
    @PlaceholderIndex INT,
    @Word NVARCHAR(50),
    @BatchId INT,
    @BatchName NVARCHAR(50),
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[UserResponses] ([StoryId], [PlaceholderId], [PlaceholderIndex], [Word], [BatchId], [BatchName])
    VALUES (@StoryId, @PlaceholderId, @PlaceholderIndex, @Word, @BatchId, @BatchName);

    SET @NewId = SCOPE_IDENTITY();

END;