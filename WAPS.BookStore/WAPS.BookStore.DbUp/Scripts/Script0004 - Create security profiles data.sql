/*
This script was created by Visual Studio on 10/12/2013 at 9:57 PM.
Run this script on ST-01827-LT\SQLEXPRESS.WAPS_BookStore (MIRANDA\st1th) to make it the same as ST-01827-LT.MTTWebAPI_DB (MIRANDA\st1th).
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_RoleId]
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_UserId]
ALTER TABLE [dbo].[UserFeature] DROP CONSTRAINT [FK_UserFeature_Feature]
ALTER TABLE [dbo].[UserFeature] DROP CONSTRAINT [FK_UserFeature_webpages_Membership]
ALTER TABLE [dbo].[RoleFeature] DROP CONSTRAINT [FK_RoleFeature_Feature]
ALTER TABLE [dbo].[RoleFeature] DROP CONSTRAINT [FK_RoleFeature_webpages_Roles]
SET IDENTITY_INSERT [dbo].[Feature] ON
INSERT INTO [dbo].[Feature] ([FeatureId], [Description], [Url], [IsActive]) VALUES (1, N'Delete Board                                      ', N'/api/board/remove', 1)
INSERT INTO [dbo].[Feature] ([FeatureId], [Description], [Url], [IsActive]) VALUES (2, N'Get Board                                         ', N'/api/board/retrieve', 1)
SET IDENTITY_INSERT [dbo].[Feature] OFF
INSERT INTO [dbo].[RoleFeature] ([RoleId], [FeatureId]) VALUES (2, 2)
INSERT INTO [dbo].[RoleFeature] ([RoleId], [FeatureId]) VALUES (1, 1)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, '20131105 05:26:19.357', NULL, 1, NULL, 0, N'AB26FIgWiUVL3/NqWB171VODsDfYLY2k2Nu5P5m32FkCGX9Y3x8NM1Pdj2bnIUiieA==', '20131105 05:26:19.357', N'', NULL, NULL)
INSERT INTO [dbo].[UserFeature] ([UserId], [FeatureId]) VALUES (2, 2)
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT INTO [dbo].[UserProfile] ([UserId], [FirstName], [LastName], [Email], [CreatedOn]) VALUES (2, N'System', N'Administrator', N'MTTWebAPIAdmin@gmail.com', '20131105 05:26:19.283')
INSERT INTO [dbo].[UserProfile] ([UserId], [FirstName], [LastName], [Email], [CreatedOn]) VALUES (5, N'System', N'User5', N'user5@gmail.com', '20131204 00:00:00.000')
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (5, 2)
ALTER TABLE [dbo].[webpages_UsersInRoles]
    ADD CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
ALTER TABLE [dbo].[webpages_UsersInRoles]
    ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])
ALTER TABLE [dbo].[UserFeature]
    ADD CONSTRAINT [FK_UserFeature_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Feature] ([FeatureId])
ALTER TABLE [dbo].[UserFeature]
    ADD CONSTRAINT [FK_UserFeature_webpages_Membership] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId])
ALTER TABLE [dbo].[RoleFeature]
    ADD CONSTRAINT [FK_RoleFeature_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Feature] ([FeatureId])
ALTER TABLE [dbo].[RoleFeature]
    ADD CONSTRAINT [FK_RoleFeature_webpages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
COMMIT TRANSACTION
