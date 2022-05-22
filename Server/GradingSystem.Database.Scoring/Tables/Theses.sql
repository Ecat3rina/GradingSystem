CREATE TABLE [dbo].[Theses]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [StudentId] UNIQUEIDENTIFIER NOT NULL, 
    [ExamId] NVARCHAR(50) NOT NULL, 
    [FinalScore] DECIMAL NULL, 
    --[Evaluators] NCHAR(10) NOT NULL, 
    --[Scores] INT NOT NULL, 
    [GradationDate] DATETIME NULL,
    [FinalGrade] DECIMAL NULL, 
    [BlobId] UNIQUEIDENTIFIER NULL, 
   -- CONSTRAINT [FK_StudentId] FOREIGN KEY (StudentId) REFERENCES Students(Id) 

)
