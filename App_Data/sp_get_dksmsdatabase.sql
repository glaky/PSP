USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[get_patallmsdatabase]    Script Date: 02.10.2015 08:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 230509
-- Description:	select from table patient
-- =============================================
CREATE PROCEDURE [dbo].[get_btkmsdatabase] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM msdatabase.dbo.dkskontakt
END
