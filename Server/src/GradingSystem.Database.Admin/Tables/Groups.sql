﻿CREATE TABLE [dbo].[Groups]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(64) NULL, 
    [Description] NVARCHAR(128) NULL
)
