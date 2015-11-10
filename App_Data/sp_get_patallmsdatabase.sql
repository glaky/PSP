USE PSP
GO
/****** Object:  StoredProcedure [dbo].[sp_get_patall]    Script Date: 01.10.2015 10:39:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 230509
-- Description:	select from table patient
-- =============================================
CREATE PROCEDURE [dbo].[sp_get_patallmsdatabase] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM msdatabase.dbo.vw_patall
END
