﻿/*
Deployment script for Admin

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Admin"
:setvar DefaultFilePrefix "Admin"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating Table [dbo].[Exams]...';


GO
CREATE TABLE [dbo].[Exams] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [StartDate]          DATETIME         NOT NULL,
    [EndDate]            DATETIME         NOT NULL,
    [NumberOfPages]      INT              NOT NULL,
    [NumberOfEvaluators] INT              NOT NULL,
    [SubjectId]          UNIQUEIDENTIFIER NOT NULL,
    [EvaluationSchemeId] UNIQUEIDENTIFIER NOT NULL,
    [Name]               NVARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Exams]...';


GO
ALTER TABLE [dbo].[Exams]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Creating Foreign Key [dbo].[FK_ExamSubjectId]...';


GO
ALTER TABLE [dbo].[Exams] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamSubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ExamEvaluationSchemeId]...';


GO
ALTER TABLE [dbo].[Exams] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamEvaluationSchemeId] FOREIGN KEY ([EvaluationSchemeId]) REFERENCES [dbo].[EvaluationSchemes] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_ExamId]...';


GO
ALTER TABLE [dbo].[GradeSchemes] WITH NOCHECK
    ADD CONSTRAINT [FK_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [dbo].[Exams] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Exams] WITH CHECK CHECK CONSTRAINT [FK_ExamSubjectId];

ALTER TABLE [dbo].[Exams] WITH CHECK CHECK CONSTRAINT [FK_ExamEvaluationSchemeId];

ALTER TABLE [dbo].[GradeSchemes] WITH CHECK CHECK CONSTRAINT [FK_ExamId];


GO
PRINT N'Update complete.';


GO
