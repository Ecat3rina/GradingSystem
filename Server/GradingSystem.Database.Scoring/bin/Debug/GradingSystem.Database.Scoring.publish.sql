﻿/*
Deployment script for Scoring

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Scoring"
:setvar DefaultFilePrefix "Scoring"
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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating Table [dbo].[EvaluationRepartitions]...';


GO
CREATE TABLE [dbo].[EvaluationRepartitions] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [EvaluatorId]     UNIQUEIDENTIFIER NOT NULL,
    [RepartitionDate] DATETIME         NOT NULL,
    [ThesisPageId]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Scores]...';


GO
CREATE TABLE [dbo].[Scores] (
    [EvaluationRepartitionId] UNIQUEIDENTIFIER NOT NULL,
    [EvaluationSchemeId]      UNIQUEIDENTIFIER NOT NULL,
    [FinalScore]              INT              NOT NULL,
    [EvaluationDate]          DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([EvaluationRepartitionId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Theses]...';


GO
CREATE TABLE [dbo].[Theses] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [StudentId]     UNIQUEIDENTIFIER NOT NULL,
    [ExamId]        NVARCHAR (50)    NOT NULL,
    [FinalScore]    INT              NOT NULL,
    [GradationDate] DATETIME         NOT NULL,
    [FinalGrade]    DECIMAL (18)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[ThesisPages]...';


GO
CREATE TABLE [dbo].[ThesisPages] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [ThesisId] UNIQUEIDENTIFIER NOT NULL,
    [FileName] NVARCHAR (50)    NOT NULL,
    [PageNr]   INT              NOT NULL,
    [BlobId]   UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[EvaluationRepartitions]...';


GO
ALTER TABLE [dbo].[EvaluationRepartitions]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Theses]...';


GO
ALTER TABLE [dbo].[Theses]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[ThesisPages]...';


GO
ALTER TABLE [dbo].[ThesisPages]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Creating Foreign Key [dbo].[FK_ThesisId]...';


GO
ALTER TABLE [dbo].[ThesisPages] WITH NOCHECK
    ADD CONSTRAINT [FK_ThesisId] FOREIGN KEY ([ThesisId]) REFERENCES [dbo].[Theses] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[ThesisPages] WITH CHECK CHECK CONSTRAINT [FK_ThesisId];


GO
PRINT N'Update complete.';


GO