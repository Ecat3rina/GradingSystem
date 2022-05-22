CREATE TABLE [dbo].[EvaluationRepartitions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL  DEFAULT newid(), 
    [EvaluatorId] UNIQUEIDENTIFIER NOT NULL , 
    [RepartitionDate] DATETIME NOT NULL, 
    [ThesisId] UNIQUEIDENTIFIER NOT NULL, 
    [EvaluationStatus] BIT NOT NULL,
    --CONSTRAINT [FK_EvaluatorId] FOREIGN KEY (EvaluatorId) REFERENCES Evaluators(Id),
    --CONSTRAINT [FK_ThesisPaged] FOREIGN KEY (ThesisPageId) REFERENCES ThesisPages(Id) 


)
