CREATE PROCEDURE [dbo].[UpdatePlaceholder]
    @Id INT,
    @PlaceholderText NVARCHAR(255),
    @PlaceholderIndex INT
AS
BEGIN
    UPDATE Placeholders
    SET PlaceholderText = @PlaceholderText, [PlaceholderIndex] = @PlaceholderIndex
    WHERE Id = @Id;
END;