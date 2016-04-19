--	Если заказ харектеризуется общим [salesid], а порядок элементов характеризуется полем [datetime]
-- 	как в примере:
--	salesid		productid		datetime 					customerid
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	1			2				'01/01/2016 23:59:10'		1
--	2			1				'02/01/2016 10:00:00'		1
--	2			2				'02/01/2016 10:00:10'		1

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[productid]
	,COUNT(s.[productid]) AS [count]
FROM [Sales] s
JOIN (
	SELECT
		[salesid]
		,MIN([datetime]) AS [minDate]
	FROM [Sales]
	GROUP BY [salesid]
	) salesIdAndMinDate
	ON salesIdAndMinDate.[salesid] = s.[salesid]
	AND salesIdAndMinDate.[minDate] = s.[datetime]
GROUP BY s.[productid]

--	Если же заказ харектеризуется общим [datetime], а порядок элементов характеризуется полем [salesid]
-- 	как в примере:
--	salesid		productid		datetime 					customerid
--	-------		---------		---------------------		----------
--	1			1				'01/01/2016 23:59:00'		1
--	2			2				'01/01/2016 23:59:00'		1
--	3			1				'02/01/2016 10:00:10'		1
--	4			2				'02/01/2016 10:00:10'		1

-- тогда запрос будет выглядеть таким образом:
SELECT
	s.[productid]
	,COUNT(s.[productid]) AS [count]
FROM [Sales] s
JOIN (
	SELECT
		MIN([salesid]) AS [minId]
	FROM [Sales]
	GROUP BY [datetime]
	) minProdId
	ON minProdId.[minId] = s.[salesid]
GROUP BY s.[productid]