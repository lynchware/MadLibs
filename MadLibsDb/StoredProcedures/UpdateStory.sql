CREATE PROCEDURE [dbo].[UpdateStory]
    @Id INT,
    @Template NVARCHAR(MAX)
AS
BEGIN
    UPDATE Stories
    SET Template = @Template
    WHERE Id = @Id;
END;