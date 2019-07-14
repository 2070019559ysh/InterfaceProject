CREATE TABLE [dbo].[ProcedureErrorLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ErrorProcedure] VARCHAR(200) NOT NULL, 
    [ErrorNumber] INT NOT NULL, 
    [ErrorSeverity] INT NOT NULL, 
    [ErrorState] INT NOT NULL, 
    [ErrorLine] INT NOT NULL, 
    [ErrorMessage] NVARCHAR(500) NOT NULL, 
    [CreateTime] DATETIME NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'存储过程错误日志ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'出现错误的存储过程或触发器的名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = 'ErrorProcedure'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误代码',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'ErrorNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误严重级别',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'ErrorSeverity'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误状态码',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'ErrorState'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'发生错误的行号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'ErrorLine'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误的具体信息',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'ErrorMessage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'错误发生时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProcedureErrorLog',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'