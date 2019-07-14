CREATE PROCEDURE [dbo].[Proc_ClearSystemLog]
	@daysAgo int = 1
AS
	BEGIN TRY
	IF @daysAgo=0
	BEGIN
		TRUNCATE TABLE SystemLog
	END
	ELSE
	BEGIN
		SELECT @daysAgo=-ABS(@daysAgo)
		DECLARE @clearDate DATETIME
		SELECT @clearDate=DATEADD(day,@daysAgo,GETDATE())
		DELETE SystemLog WHERE CreateTime<@clearDate
	END
	END TRY
	BEGIN CATCH
	INSERT INTO ProcedureErrorLog
	SELECT NEWID() AS Id,
		   ERROR_PROCEDURE() AS ErrorProcedure , --出现错误的存储过程或触发器的名称。
		   ERROR_NUMBER() AS ErrorNumber,  --错误代码
		   Error_severity() AS ErrorSeverity,  --错误严重级别，级别小于10 try catch 捕获不到
		   Error_state() AS ErrorState ,  --错误状态码
		   Error_line() AS ErrorLine,  --发生错误的行号
		   Error_message() AS ErrorMessage,  --错误的具体信息
	       GETDATE() AS CreateTime
	END CATCH
RETURN @@ROWCOUNT
