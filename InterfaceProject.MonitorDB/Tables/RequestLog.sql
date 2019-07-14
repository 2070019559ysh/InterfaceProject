CREATE TABLE [dbo].[RequestLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Name] NVARCHAR(200) NOT NULL, 
	[RequestUrl] VARCHAR(1000) NOT NULL,
	[RequestMethod] VARCHAR(10) NOT NULL,
    [RequestMsg] NTEXT NOT NULL, 
    [ResponseMsg] NTEXT NULL, 
    [ExceptionMsg] NTEXT NULL, 
	[ReferenceId] UNIQUEIDENTIFIER NULL,
	[ReferenceTable] VARCHAR(200) NULL,
    [Level] VARCHAR(10) NOT NULL, 
    [Version] VARCHAR(20) NOT NULL, 
    [CreateTime] DATETIME NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的名命',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的请求消息',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'RequestMsg'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的响应消息',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'ResponseMsg'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的异常信息',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'ExceptionMsg'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求接口地址',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'RequestUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求接口的方式：GET,POST',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'RequestMethod'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志的等级',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'Level'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当时系统版本',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'Version'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求日志创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'关联表名',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'ReferenceTable'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'关联表业务Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'RequestLog',
    @level2type = N'COLUMN',
    @level2name = N'ReferenceId'
GO

CREATE INDEX [IX_RequestLog_Name] ON [dbo].[RequestLog] ([Name])

GO

CREATE INDEX [IX_RequestLog_Level] ON [dbo].[RequestLog] ([Level])

GO

CREATE INDEX [IX_RequestLog_CreateTime] ON [dbo].[RequestLog] ([CreateTime])
