CREATE PROCEDURE [dbo].[UpdatePlaceholder]
    @Id INT,
    @PlaceholderText NVARCHAR(255)
AS
BEGIN
    UPDATE Placeholders
    SET PlaceholderText = @PlaceholderText
    WHERE Id = @Id;
END;