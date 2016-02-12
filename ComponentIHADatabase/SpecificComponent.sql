CREATE TABLE [dbo].[SpecificComponent]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Number] INT NOT NULL, 
    [SerieNr] NVARCHAR(MAX) NOT NULL,
	[Component] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_SpecificComponent_To_Component] FOREIGN KEY ([Component]) REFERENCES [Component]([ComponentId]) ON DELETE CASCADE
)
