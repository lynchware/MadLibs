CREATE TABLE [dbo].[Placeholders] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [StoryId]     INT           NOT NULL,
    [PlaceholderText] NVARCHAR (50) NOT NULL,
    [PlaceholderIndex] INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

