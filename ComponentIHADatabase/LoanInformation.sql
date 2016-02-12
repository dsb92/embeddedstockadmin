CREATE TABLE [dbo].[LoanInformation]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [LoanDate] DATETIME NULL, 
    [ReturnDate] DATETIME NULL, 
    [IsEmailSend] BIT NULL, 
    [ReservationDate] DATETIME NULL, 
    [ReservationId] INT NOT NULL, 
    [SpecificComponentId] INT NOT NULL, 
	PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanInformation_To_Student] FOREIGN KEY ([ReservationId]) REFERENCES [Student]([Id]),
    CONSTRAINT [FK_LoanInformation_To_SpecificComponent] FOREIGN KEY ([SpecificComponentId]) REFERENCES [SpecificComponent]([Id]) ON DELETE CASCADE
)
