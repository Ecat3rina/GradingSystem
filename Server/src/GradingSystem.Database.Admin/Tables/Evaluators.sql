CREATE TABLE [dbo].[Evaluators]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [SubjectId] UNIQUEIDENTIFIER NOT NULL,
    --CONSTRAINT [FK_EvaluatorSubjectId] FOREIGN KEY (SubjectId) REFERENCES Subjects(Id) 

)
