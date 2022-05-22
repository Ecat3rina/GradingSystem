CREATE TABLE [dbo].[Theses]
(
	[Id] uniqueidentifier NOT NULL PRIMARY KEY,
	[FileContent] varbinary(max) not null, 
    [StudentId] UNIQUEIDENTIFIER NOT NULL, 
    [ExamId] UNIQUEIDENTIFIER NOT NULL, 
    [Filename] NVARCHAR(50) NULL,
    [NumberOfPages] INT NULL, 
    CONSTRAINT UQ_STUD_EXM UNIQUE(StudentId, ExamId)
)
