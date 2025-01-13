CREATE PROCEDURE GetUserResponsesByStory
    @StoryId INT
AS
BEGIN
    SELECT UR.*, P.Placeholder
    FROM UserResponses UR
    INNER JOIN Placeholders P ON UR.PlaceholderId = P.Id
    WHERE UR.StoryId = @StoryId;
END
