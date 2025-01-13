CREATE TABLE [dbo].[Stories]
(
	[Id] INT NOT NULL PRIMARY KEY,
	Title NVARCHAR(100) NOT NULL,
    Template NVARCHAR(MAX) NOT NULL,
    [ThemeId] INT,        -- Foreign key to Themes
    CONSTRAINT FK_Stories_Theme FOREIGN KEY (ThemeId) REFERENCES [dbo].[Theme](Id)
)
