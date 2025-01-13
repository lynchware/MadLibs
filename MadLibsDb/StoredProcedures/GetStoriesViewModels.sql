CREATE PROCEDURE GetStoriesViewModels
AS
BEGIN
    SELECT 
        s.Id AS StoryId,
        s.Title AS StoryTitle,
        s.Template,
        s.ThemeId,
        t.Theme AS ThemeName,
        p.Id AS PlaceholderId,
        p.Placeholder,
        r.Id AS ResponseId,
        r.PlaceholderId AS ResponsePlaceholderId,
        r.Word AS ResponseWord
    FROM Stories s
    LEFT JOIN Theme t ON s.ThemeId = t.Id
    LEFT JOIN Placeholders p ON s.Id = p.StoryId
    LEFT JOIN UserResponses r ON p.Id = r.PlaceholderId;
END;
