-- Если столбец [id] - суррогантый ключ, тогда он уникальный, 
-- значит может быть два варианта

--	Если пользователь может купить только один продукт в один момент времени [DateCreated]
-- 	как в примере:
--	Id			ProductId		DateCreated 				CustomerId
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	2			2				'01/01/2016 23:59:01'		1
--	3			1				'02/01/2016 10:00:10'		1
--	4			2				'02/01/2016 10:00:11'		1
--	5			1				'01/01/2016 23:59:02'		2
--	6			2				'01/01/2016 23:59:03'		2
--	7			1				'02/01/2016 10:00:12'		2
--	8			2				'02/01/2016 10:00:13'		2

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[ProductId]
	,COUNT(s.[ProductId]) AS [count]
FROM [Sales] s
JOIN (
		SELECT
			MIN([DateCreated]) AS [minDate],
			[CustomerId]
		FROM [Sales]
		GROUP BY [CustomerId]
	) minTime
	ON s.[CustomerId] = minTime.[CustomerId] 
	AND minTime.[minDate] = s.[DateCreated]
GROUP BY s.[ProductId]

--	Если пользователь может купить несколько продуктов одновременно в один момент времени [DateCreated] 
--  и порядок элементов характеризуется полем [Id]
-- 	как в примере:
--	Id			ProductId		DateCreated 				CustomerId
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	2			2				'01/01/2016 23:59:00'		1
--	3			1				'02/01/2016 10:00:10'		1
--	4			2				'02/01/2016 10:00:10'		1
--	5			3				'01/01/2016 23:59:00'		2
--	6			4				'01/01/2016 23:59:00'		2
--	7			3				'02/01/2016 10:00:10'		2
--	8			4				'02/01/2016 10:00:10'		2

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[ProductId]
	,COUNT(s.[ProductId]) AS [count]
FROM [Sales] s
JOIN (
		SELECT
			MIN([Id]) AS [minId]
		FROM [Sales]
		GROUP BY [DateCreated], [CustomerId]
	) firstProductId
	ON firstProductId.[minId] = s.[Id]
GROUP BY s.[ProductId]