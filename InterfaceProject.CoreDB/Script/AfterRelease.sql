/*
后期部署脚本模板							
--------------------------------------------------------------------------------------
 此文件包含将附加到生成脚本中的 SQL 语句。		
 使用 SQLCMD 语法将文件包含到后期部署脚本中。			
 示例:      :r .\myfile.sql								
 使用 SQLCMD 语法引用后期部署脚本中的变量。		
 示例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS(SELECT Id FROM SysConfigInfo WHERE [Key]='Version')
	INSERT INTO SysConfigInfo([Name],[Key],[Value],[CreateTime]) VALUES(N'整个系统的当前版本','Version','1.0.0',GETDATE())