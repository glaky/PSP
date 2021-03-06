USE [PSP]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_account]    Script Date: 05.10.2015 15:29:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gunter Laky
-- Create date: 06-07-09
-- Description:	Insert entry into accounts
-- =============================================
CREATE PROCEDURE [dbo].[insert_account]
	@name nvarchar(50),
	@forename nvarchar(50),
	@account nvarchar(50),
	@pwd nvarchar(50),
	@role nvarchar(50),
	@phone nvarchar(50),
	@fax nvarchar(50),
	@email nvarchar(70),
	@gendate nvarchar(30),
	@status nvarchar(2),
	@title nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

    INSERT INTO accounts (name,forename,account,pwd,role, phone, fax, email, gendate,status,title)
	VALUES (@name,@forename,@account,@pwd,@role,@phone,@fax,@email,@gendate,@status,@title)
END


