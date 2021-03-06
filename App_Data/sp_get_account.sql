USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[get_account]    Script Date: 05.10.2015 15:10:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 06-07-09
-- Description:	select from table account
-- =============================================
ALTER PROCEDURE [dbo].[get_account] 
	-- Add the parameters for the stored procedure here
	@id smallint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM accounts WHERE id=@id
END