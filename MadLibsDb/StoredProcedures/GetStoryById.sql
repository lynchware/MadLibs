﻿CREATE PROCEDURE GetStoryById
    @Id INT
AS
BEGIN
    SELECT * FROM Stories WHERE Id = @Id;
END