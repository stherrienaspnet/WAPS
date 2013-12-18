﻿/*
Deployment script for WAPS_BookStore

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "WAPS_BookStore"
--:setvar DefaultFilePrefix "WAPS_BookStore"
--:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
--:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

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
PRINT N'Creating [dbo].[Feature]...';


GO
CREATE TABLE [dbo].[Feature] (
    [FeatureId]   INT            IDENTITY (1, 1) NOT NULL,
    [Description] NCHAR (50)     NOT NULL,
    [Url]         NVARCHAR (100) NOT NULL,
    [IsActive]    BIT            NOT NULL,
    CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED ([FeatureId] ASC)
);


GO
PRINT N'Creating [dbo].[RoleFeature]...';


GO
CREATE TABLE [dbo].[RoleFeature] (
    [RoleId]    INT NOT NULL,
    [FeatureId] INT NOT NULL,
    CONSTRAINT [PK_RoleFeature] PRIMARY KEY CLUSTERED ([RoleId] ASC, [FeatureId] ASC)
);


GO
PRINT N'Creating [dbo].[SchemaVersions]...';


GO
CREATE TABLE [dbo].[SchemaVersions] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ScriptName] NVARCHAR (255) NOT NULL,
    [Applied]    DATETIME       NOT NULL,
    CONSTRAINT [PK_SchemaVersions_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[UserFeature]...';


GO
CREATE TABLE [dbo].[UserFeature] (
    [UserId]    INT NOT NULL,
    [FeatureId] INT NOT NULL,
    CONSTRAINT [PK_UserFeature] PRIMARY KEY CLUSTERED ([UserId] ASC, [FeatureId] ASC)
);


GO
PRINT N'Creating [dbo].[UserProfile]...';


GO
CREATE TABLE [dbo].[UserProfile] (
    [UserId]          INT              IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (50)    NOT NULL,
    [LastName]        NVARCHAR (50)    NOT NULL,
    [Email]           NVARCHAR (100)   NOT NULL,
    [SessionId]       UNIQUEIDENTIFIER NULL,
    [SessionExpireAt] DATETIME         NULL,
    [CreatedOn]       DATETIME         NOT NULL,
    CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating [dbo].[webpages_Membership]...';


GO
CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating [dbo].[webpages_OAuthMembership]...';


GO
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC)
);


GO
PRINT N'Creating [dbo].[webpages_Roles]...';


GO
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);


GO
PRINT N'Creating [dbo].[webpages_UsersInRoles]...';


GO
CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating DF__webpages___IsCon__0519C6AF...';


GO
ALTER TABLE [dbo].[webpages_Membership]
    ADD DEFAULT ((0)) FOR [IsConfirmed];


GO
PRINT N'Creating DF__webpages___Passw__060DEAE8...';


GO
ALTER TABLE [dbo].[webpages_Membership]
    ADD DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess];


GO
PRINT N'Creating FK_RoleFeature_Feature...';


GO
ALTER TABLE [dbo].[RoleFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_RoleFeature_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Feature] ([FeatureId]);


GO
PRINT N'Creating FK_RoleFeature_webpages_Roles...';


GO
ALTER TABLE [dbo].[RoleFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_RoleFeature_webpages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]);


GO
PRINT N'Creating FK_UserFeature_Feature...';


GO
ALTER TABLE [dbo].[UserFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_UserFeature_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Feature] ([FeatureId]);


GO
PRINT N'Creating FK_UserFeature_webpages_Membership...';


GO
ALTER TABLE [dbo].[UserFeature] WITH NOCHECK
    ADD CONSTRAINT [FK_UserFeature_webpages_Membership] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId]);


GO
PRINT N'Creating fk_RoleId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]);


GO
PRINT N'Creating fk_UserId...';


GO
ALTER TABLE [dbo].[webpages_UsersInRoles] WITH NOCHECK
    ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[RoleFeature] WITH CHECK CHECK CONSTRAINT [FK_RoleFeature_Feature];

ALTER TABLE [dbo].[RoleFeature] WITH CHECK CHECK CONSTRAINT [FK_RoleFeature_webpages_Roles];

ALTER TABLE [dbo].[UserFeature] WITH CHECK CHECK CONSTRAINT [FK_UserFeature_Feature];

ALTER TABLE [dbo].[UserFeature] WITH CHECK CHECK CONSTRAINT [FK_UserFeature_webpages_Membership];

ALTER TABLE [dbo].[webpages_UsersInRoles] WITH CHECK CHECK CONSTRAINT [fk_RoleId];

ALTER TABLE [dbo].[webpages_UsersInRoles] WITH CHECK CHECK CONSTRAINT [fk_UserId];


GO
PRINT N'Update complete.';


GO
