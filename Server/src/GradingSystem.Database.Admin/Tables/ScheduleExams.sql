CREATE TABLE [dbo].[ScheduleExams]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ExamId] UNIQUEIDENTIFIER NOT NULL, 
    [GroupId] UNIQUEIDENTIFIER NOT NULL
)
