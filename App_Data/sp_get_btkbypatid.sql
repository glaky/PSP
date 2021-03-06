USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[get_btkbypatid]    Script Date: 24.09.2015 21:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 06-07-09
-- Description:	select from table dkskontakt all contacts for a patient
-- =============================================
ALTER PROCEDURE [dbo].[get_btkbypatid] 
@patid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM btk WHERE patid=@patid  ORDER BY btkid DESC, btkdate DESC
END
