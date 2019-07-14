CREATE TABLE [dbo].[WeatherCity]
(
	[Id] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ParentId] VARCHAR(20) NOT NULL DEFAULT 'null', 
    [Name] NVARCHAR(20) NOT NULL, 
	[WCityLevel] INT NOT NULL,
	[WCityLevelName] NVARCHAR(20) NOT NULL,
    [CreateTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天气城市代号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父级城市代号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = 'ParentId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'城市名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最近更新时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'UpdateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天气城市等级：1=省份，2=城市，3=市区',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'WCityLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天气城市等级名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'WeatherCity',
    @level2type = N'COLUMN',
    @level2name = N'WCityLevelName'