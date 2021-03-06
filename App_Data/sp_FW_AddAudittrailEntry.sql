USE [TecfiCare]
GO
/****** Object:  StoredProcedure [dbo].[_FW_addAuditTrailEntry]    Script Date: 23.07.2014 21:42:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		DolphiCom (Stefan Kalcher)
-- Create date: 10.09.2008
-- Modify date: 23.11.2009
-- Description:	adds an audittrail entry
-- =============================================
ALTER PROCEDURE [dbo].[_FW_addAuditTrailEntry] 
	-- Add the parameters for the stored procedure here
	@Id bigint = NULL OUTPUT,
	@Type int = NULL,
	@DateTime datetime = NULL OUTPUT,
	@UserId int = NULL,
	@Description nvarchar(256) = NULL,
	@ObjectId int = NULL,
	@SecondaryObjectId int = NULL,
	@FieldId int = NULL,
	@FieldName nvarchar(256) = NULL,
	@OldValue sql_variant = NULL,
	@NewValue sql_variant = NULL,
	@OldValueApplicable bit = NULL,
	@NewValueApplicable bit = NULL,
	@FwObjectType tinyint = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF @Type IS NULL
	BEGIN
		RAISERROR('Parameter @Type darf nicht NULL sein!', -1, 1)
		RETURN 1;
	END
	
	IF @UserID IS NULL
	BEGIN
		RAISERROR('Parameter @UserId darf nicht NULL sein!', -1, 1)
		RETURN 1;
	END
	
	SET @DateTime = CURRENT_TIMESTAMP;

	INSERT INTO [_FW_AuditTrail]
		   ([type]
		   ,[datetime]
		   ,[userid]
		   ,[description]
		   ,[objectid]
		   ,[secondaryobjectid]
		   ,[fieldid]
		   ,[fieldname]
		   ,[oldvalue]
		   ,[newvalue]
		   ,[oldvalueapplicable]
		   ,[newvalueapplicable]
		   ,[fwobjecttype])
	 VALUES
		   (@Type
		   ,@DateTime
		   ,@UserId
		   ,@Description
		   ,@ObjectId
		   ,@SecondaryObjectId
		   ,@FieldId
		   ,@FieldName
		   ,@OldValue
		   ,@NewValue
		   ,@OldValueApplicable
		   ,@NewValueApplicable
		   ,@FwObjectType)
           
	 SET @Id = SCOPE_IDENTITY()
END

