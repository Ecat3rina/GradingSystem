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
PRINT N'Creating Table [dbo].[EvaluationRepartitions]...';


GO
CREATE TABLE [dbo].[EvaluationRepartitions] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [EvaluatorId]      UNIQUEIDENTIFIER NOT NULL,
    [RepartitionDate]  DATETIME         NOT NULL,
    [ThesisId]         UNIQUEIDENTIFIER NOT NULL,
    [EvaluationStatus] BIT              NOT NULL
);


GO
PRINT N'Creating Table [dbo].[Scores]...';


GO
CREATE TABLE [dbo].[Scores] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [EvaluationRepartitionId] UNIQUEIDENTIFIER NOT NULL,
    [ItemNumber]              INT              NOT NULL,
    [Score]                   INT              NOT NULL,
    [Comments]                NVARCHAR (1024)  NULL,
    [EvaluationDate]          DATETIME         NOT NULL
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[EvaluationRepartitions]...';


GO
ALTER TABLE [dbo].[EvaluationRepartitions]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Scores]...';


GO
ALTER TABLE [dbo].[Scores]
    ADD DEFAULT newid() FOR [Id];


GO
PRINT N'Update complete.';


GO
