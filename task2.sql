Sales: Id, ProductId, CustomerId, DateCreated

--	Если заказ харектеризуется общим [Id], а порядок элементов характеризуется полем [DateCreated]
-- 	как в примере:
--	Id			ProductId		DateCreated 				CustomerId
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	1			2				'01/01/2016 23:59:10'		1
--	2			1				'02/01/2016 10:00:00'		1
--	2			2				'02/01/2016 10:00:10'		1

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[ProductId]
	,COUNT(s.[ProductId]) AS [count],
	s.[CustomerId]
FROM [Sales] s
JOIN (
		SELECT
			[Id]
			,MIN([DateCreated]) AS [minDate]
		FROM [Sales]
		GROUP BY [Id]
	) salesIdAndMinDate
	ON salesIdAndMinDate.[Id] = s.[Id]
	AND salesIdAndMinDate.[minDate] = s.[DateCreated]
GROUP BY s.[ProductId], s.[CustomerId]

--	Если же заказ харектеризуется общим [DateCreated], а порядок элементов характеризуется полем [Id]
-- 	как в примере:
--	Id			ProductId		DateCreated 				CustomerId
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	2			2				'01/01/2016 23:59:00'		1
--	3			1				'02/01/2016 10:00:10'		1
--	4			2				'02/01/2016 10:00:10'		1

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[ProductId]
	,COUNT(s.[ProductId]) AS [count]
	,s.[CustomerId]
FROM [Sales] s
JOIN (
		SELECT
			MIN([Id]) AS [minId]
		FROM [Sales]
		GROUP BY [DateCreated], [CustomerId]
	) minProdId
	ON minProdId.[minId] = s.[Id]
GROUP BY s.[ProductId], s.[CustomerId]