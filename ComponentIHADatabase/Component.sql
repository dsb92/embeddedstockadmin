CREATE TABLE [dbo].[Component]
(
	[ComponentId] INT NOT NULL IDENTITY(1,1), 
	[CategoryId] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL, 
	[Image] NVARCHAR(MAX) NULL, 
	[ImageBytes] VARBINARY(max) NULL,
    [Info] NVARCHAR(MAX) NULL, 
    [Datasheet] NVARCHAR(MAX) NULL, 
    [ManufacturerLink] NVARCHAR(MAX) NULL, 
    [AdminComment] NVARCHAR(MAX) NULL, 
    [UserComment] NVARCHAR(MAX) NULL, 
	 
    PRIMARY KEY CLUSTERED ([ComponentId] ASC),
    CONSTRAINT [FK_Component_To_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]) ON DELETE CASCADE, 
)
