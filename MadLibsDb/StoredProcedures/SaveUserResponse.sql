CREATE PROCEDURE SaveUserResponse
    @StoryId INT,
    @PlaceholderId INT,
    @Word NVARCHAR(50),
    @BatchId INT,
    @BatchName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[UserResponses] ([StoryId], [PlaceholderId], [Word], [BatchId], [BatchName])
    VALUES (@StoryId, @PlaceholderId, @Word, @BatchId, @BatchName);

END;