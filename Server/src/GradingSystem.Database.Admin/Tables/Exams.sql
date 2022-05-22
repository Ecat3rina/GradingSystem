CREATE TABLE [dbo].[Exams]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Name] NVARCHAR(50) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [NumberOfPages] INT NOT NULL, 
    [NumberOfEvaluators] INT NOT NULL, 
    [SubjectId] UNIQUEIDENTIFIER NOT NULL, 
    [GradeSchemeId] UNIQUEIDENTIFIER NOT NULL, 
    [WasGenerated] BIT NOT NULL,
    --CONSTRAINT [FK_ExamSubjectId] FOREIGN KEY (SubjectId) REFERENCES Subjects(Id), 
    --CONSTRAINT [FK_ExamEvaluationSchemeId] FOREIGN KEY (EvaluationSchemeId) REFERENCES EvaluationSchemes(Id) 
)
