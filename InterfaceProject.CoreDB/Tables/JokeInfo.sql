CREATE TABLE [dbo].[JokeInfo]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ImageUrl] VARCHAR(400) NULL, 
    [Content] NVARCHAR(2048) NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [CreateBy] NVARCHAR(36) NOT NULL, 
    [UpdateTime] DATETIME NULL, 
    [UpdateBy] NVARCHAR(36) NULL, 
    [Version] VARCHAR(20) NOT NULL, 
    [IsDel] BIT NOT NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'笑话ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'笑话趣图地址',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'ImageUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'笑话内容',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'Content'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID或者名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreateBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最近更新时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'UpdateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最近更新人ID或者名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'UpdateBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时系统版本',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'Version'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否已被删除',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'JokeInfo',
    @level2type = N'COLUMN',
    @level2name = N'IsDel'