CREATE PROCEDURE SaveUserResponse
    @StoryId INT,
    @PlaceholderId INT,
    @PlaceholderIndex INT,
    @Word NVARCHAR(50),
    @BatchId INT,
    @BatchName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[UserResponses] ([StoryId], [PlaceholderId], [PlaceholderIndex], [Word], [BatchId], [BatchName])
    VALUES (@StoryId, @PlaceholderId, @PlaceholderIndex, @Word, @BatchId, @BatchName);

END;