USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[get_btkbybtkid]    Script Date: 25.09.2015 11:27:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 06-07-09
-- Description:	select from table dkskontakt all contacts for a patient
-- =============================================
ALTER PROCEDURE [dbo].[get_btkbybtkid] 
@btkid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM btk WHERE btkid=@btkid  ORDER BY btkid DESC, btkdate DESC
END
