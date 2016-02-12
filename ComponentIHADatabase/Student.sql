﻿CREATE TABLE [dbo].[Student]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
	[Name] NVARCHAR(MAX) NOT NULL,
	[Efternavn] NVARCHAR(MAX) NOT NULL,
    [StudentId] NVARCHAR(MAX) NOT NULL, 
    [MobileNr] NVARCHAR(MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
