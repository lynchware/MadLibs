CREATE PROCEDURE [dbo].[BulkSavePlaceholders]
	@placeholders BasicUDT readonly
AS
BEGIN
-- remove old placeholders
	DELETE FROM Placeholders WHERE StoryId IN (SELECT DISTINCT StoryId FROM @placeholders);
-- insert new placeholders
	INSERT INTO Placeholders (StoryId, PlaceholderText, PlaceholderIndex)
-- instead of using Values() we are using select statement to insert data from table.
    SELECT StoryId, PlaceholderText, PlaceholderIndex 
	FROM @placeholders;
END;