CREATE TABLE [dbo].[EvaluationSchemeComponents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [EvaluationSchemeId] UNIQUEIDENTIFIER NOT NULL, 
    [ItemNr] INT NOT NULL, 
    [PageNr] INT NOT NULL, 
    [MinimumScore] INT NOT NULL, 
    [MaximumScore] INT NOT NULL, 
    [CorrectAnswer] NVARCHAR(MAX) NOT NULL, 
    [Specifications] NVARCHAR(MAX) NOT NULL,
    --CONSTRAINT [FK_EvaluationSchemeComponent] FOREIGN KEY (EvaluationSchemeId) REFERENCES EvaluationSchemes(Id) 
)
