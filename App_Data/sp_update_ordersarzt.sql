USE [TecfiCare]
GO
/****** Object:  StoredProcedure [dbo].[update_ordersarzt]    Script Date: 23.07.2014 21:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[update_ordersarzt]
	@patid int,
	@name nvarchar(100),
	@vorname nvarchar(50),
	@adresse nvarchar(100),
	@plz nvarchar(10),
	@ort nvarchar(100),
	@geschlecht nvarchar(10),
	@titel nvarchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	SELECT * FROM ordersArzt WHERE patid = @patid
	IF @@ROWCOUNT=0 
		BEGIN
			INSERT INTO ordersArzt (patid,name,vorname,adresse,plz,ort,geschlecht,titel) VALUES (@patid,@name,@vorname,@adresse,@plz,@ort,@geschlecht,@titel)
		END 
    ELSE
		BEGIN
			UPDATE ordersArzt SET name=@name,vorname=@vorname,adresse=@adresse,plz=@plz,ort=@ort,geschlecht=@geschlecht,titel=@titel WHERE patid=@patid
		END
END
