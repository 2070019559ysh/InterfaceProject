CREATE TABLE [dbo].[SysConfigInfo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(200) NOT NULL, 
    [Key] VARCHAR(50) NOT NULL, 
    [Value] VARCHAR(200) NOT NULL, 
    [CreateTime] DATETIME NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'使用简单整型ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SysConfigInfo',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'名称或者说明',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SysConfigInfo',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配置的Key',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SysConfigInfo',
    @level2type = N'COLUMN',
    @level2name = N'Key'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配置的值',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SysConfigInfo',
    @level2type = N'COLUMN',
    @level2name = N'Value'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'SysConfigInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO

CREATE UNIQUE INDEX [IX_SysConfigInfo_Key] ON [dbo].[SysConfigInfo] ([Key])
