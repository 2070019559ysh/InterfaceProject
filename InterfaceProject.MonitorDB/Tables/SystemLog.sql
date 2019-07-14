CREATE TABLE [dbo].[SystemLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID() , 
    [Module] VARCHAR(200) NOT NULL, 
    [Content] NTEXT NOT NULL, 
    [ThreadId] INT NOT NULL, 
    [Level] VARCHAR(10) NOT NULL, 
    [Version] VARCHAR(20) NOT NULL, 
    [CreateTime] DATETIME NOT NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'系统日志ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志产生所在的模块',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'Module'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志的具体内容',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志产生的所在线程Id',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'ThreadId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志等级',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'Level'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当时系统版本',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'Version'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SystemLog',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO

CREATE INDEX [IX_SystemLog_Module] ON [dbo].[SystemLog] ([Module])

GO

CREATE INDEX [IX_SystemLog_Level] ON [dbo].[SystemLog] ([Level])

GO

CREATE INDEX [IX_SystemLog_CreateTime] ON [dbo].[SystemLog] ([CreateTime])
