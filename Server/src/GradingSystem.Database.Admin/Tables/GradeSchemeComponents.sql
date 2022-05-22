CREATE TABLE [dbo].[GradeSchemeComponents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [GradeSchemeId] UNIQUEIDENTIFIER NULL, 
    [Grade] INT NULL, 
    [MinimumScore] INT NULL, 
    [MaximumScore] INT NULL,
    --CONSTRAINT [FK_GradeSchemeComponent] FOREIGN KEY (GradeSchemeId) REFERENCES GradeSchemes(Id) 
)
