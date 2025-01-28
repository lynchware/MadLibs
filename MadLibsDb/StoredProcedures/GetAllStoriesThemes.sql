CREATE PROCEDURE [dbo].[GetAllStoriesThemes]
AS
BEGIN
    SELECT s.Id, s.Title, s.Template, s.ThemeId, t.Theme AS ThemeName, t.ImagePath
    FROM STORIES s
    JOIN THEME t ON s.ThemeId = t.Id
END
