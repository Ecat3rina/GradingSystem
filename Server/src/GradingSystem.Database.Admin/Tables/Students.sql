﻿CREATE TABLE [dbo].[Students]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [IDNP] NCHAR(13) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [Address] NVARCHAR(200) NULL, 
    [GroupId] UNIQUEIDENTIFIER NOT NULL
)