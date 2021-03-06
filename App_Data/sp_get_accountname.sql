USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_accountname]    Script Date: 05.10.2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 100609
-- Description:	select from table account
-- =============================================
CREATE PROCEDURE [dbo].[get_accountname] 
@account nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM accounts WHERE account=@account
END
