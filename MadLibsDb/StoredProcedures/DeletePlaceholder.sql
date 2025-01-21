CREATE PROCEDURE [dbo].[DeletePlaceholder]
    @Id INT
AS
BEGIN
    DELETE FROM [dbo].[Placeholders]
    WHERE Id = @Id;

    DELETE FROM [dbo].[UserResponses]
    WHERE PlaceholderId = @Id;
END;
