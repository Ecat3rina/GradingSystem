CREATE TABLE [dbo].[Users]
(
	[Id] uniqueidentifier NOT NULL PRIMARY KEY,
	[Firstname] nvarchar(255) not null,
	[Lastname] nvarchar(255) not null,
	[Username] nvarchar(255) not null Unique,
	[Password] nvarchar(1024) not null, 
    [RoleId] UNIQUEIDENTIFIER NOT NULL
)
