CREATE PROCEDURE [dbo].[GetEverything]
AS
BEGIN
    SELECT * FROM Theme;
    SELECT * FROM Stories;
    SELECT * FROM Placeholders;
    SELECT * FROM UserResponses;
END
