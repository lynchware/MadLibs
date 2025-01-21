CREATE PROCEDURE [dbo].[DeleteUserResponsesByStory]
    @StoryId INT
AS
BEGIN
    DELETE FROM [dbo].[UserResponses]
    WHERE StoryId = @StoryId;
END;