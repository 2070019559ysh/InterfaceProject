CREATE TYPE [dbo].[WeatherCityTable] AS TABLE
(
	[Id] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [ParentId] VARCHAR(20) NOT NULL DEFAULT 'null', 
    [Name] NVARCHAR(20) NOT NULL, 
	[WCityLevel] INT NOT NULL,
	[WCityLevelName] NVARCHAR(20) NOT NULL,
    [CreateTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NULL
)
