USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[get_orderproducts]    Script Date: 02.10.2015 14:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		glaky
-- Create date: 06-07-09
-- Description:	select from table dkskontakt all contacts for a patient
-- =============================================
CREATE PROCEDURE [dbo].[get_ordersmsdatabase] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM msdatabase.dbo.bestellung
END
