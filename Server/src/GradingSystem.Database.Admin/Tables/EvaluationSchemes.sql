CREATE TABLE [dbo].[EvaluationSchemes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Name] NVARCHAR(50) NOT NULL, 
    [ExamId] UNIQUEIDENTIFIER NOT NULL, 
    [NumberOfItems] INT NOT NULL, 

   -- CONSTRAINT [FK_EvaluationSchemeExamId] FOREIGN KEY ([ExamId]) REFERENCES Exams(Id) 

)
