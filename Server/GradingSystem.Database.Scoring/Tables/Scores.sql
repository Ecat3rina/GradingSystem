CREATE TABLE [dbo].[Scores]
(
[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
	[EvaluationRepartitionId] UNIQUEIDENTIFIER NOT NULL , 
    [ItemNumber] INT NOT NULL, 
    [Score] INT NOT NULL, 
    [Comments] NVARCHAR(1024),
    [EvaluationDate] DATETIME NOT NULL,
    --CONSTRAINT [FK_EvaluationRepartitionId] FOREIGN KEY (EvaluationRepartitionId) REFERENCES EvaluationRepartitions(Id), 
    --PRIMARY KEY ([Id]),
    --CONSTRAINT [FK_ScoreRepartitionEvaluationSchemeId] FOREIGN KEY (EvaluationSchemeId) REFERENCES EvaluationSchemes(Id) 


)
