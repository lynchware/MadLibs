CREATE PROCEDURE GetStoriesViewModels
AS
BEGIN
    SELECT 
        s.Id,
        s.Title,
        s.Template,
        s.ThemeId,
        t.Theme AS ThemeName,
        t.ImagePath AS ImagePath,
        p.Id,
        p.StoryId,
        p.Placeholder as PlaceholderText,
        r.Id,
        r.StoryId,
        r.PlaceholderId,
        r.Word
    FROM Stories s
    LEFT JOIN Theme t ON s.ThemeId = t.Id
    LEFT JOIN Placeholders p ON s.Id = p.StoryId
    LEFT JOIN UserResponses r ON p.Id = r.PlaceholderId;
END;
