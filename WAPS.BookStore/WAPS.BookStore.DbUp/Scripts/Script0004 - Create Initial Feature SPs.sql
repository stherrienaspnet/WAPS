USE [WAPS_BookStore]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllFeatures]    Script Date: 12/08/2013 15:36:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAllFeatures]
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT * FROM Feature
	
END

GO

/****** Object:  StoredProcedure [dbo].[sp_CanUserAccessFeature]    Script Date: 12/08/2013 15:36:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CanUserAccessFeature]
	@username				nvarchar(100),
	@featureUrl				nvarchar(100)
AS
BEGIN

	SET NOCOUNT ON;
	Declare @featureId		int
	Declare @featureCount	int
	Declare @userId			int

	set @featureId = (select FeatureId from Feature where Url = @featureUrl COLLATE SQL_Latin1_General_CP1_CI_AS)
	set @userId = (select UserId from UserProfile where Email = @username COLLATE SQL_Latin1_General_CP1_CI_AS)

	set @featureCount = (select count (UserId) 
		from UserFeature 
		where FeatureId = @featureId and UserId = @userId)

	set @featureCount = @featureCount + (select count(FeatureId) from RoleFeature where FeatureId = @featureId and RoleId in 
		(select RoleId from webpages_UsersInRoles where UserId = @userId))

	select @featureCount

END

GO



