USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[update_patdatbybtkid]    Script Date: 02.10.2015 12:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[update_patdatbybtkid]
	@patid int,
	@btkid int,
	@zustaendigkeit nvarchar(5)  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	UPDATE patdaten SET btkid=@btkid, zustaendigkeit=@zustaendigkeit WHERE patid=@patid
    
END
