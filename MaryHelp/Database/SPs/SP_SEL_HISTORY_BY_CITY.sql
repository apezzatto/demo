CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_HISTORY_BY_CITY`(
IN		p_id_worker		CHAR(36),
IN		p_id_city		INT
)
BEGIN

SELECT 		job.IdJob,
			job.dtJobStarts,
			job.dtJobEnd,
            client.Name AS ClientName,
            city.Name AS City,
            neighborhood.Name AS Neighborhood,
            job.qtyRooms,
            job.qtyBaths,
            job.qtyWindows,
            job.qtyMirrors,
            job.vacuum,
            job.laundry,
			job.garage,
            job.dishes,
            job.jobOpen,
            job.price

FROM		job
INNER JOIN	user AS client			ON client.idUser 	= job.idClient
INNER JOIN	user AS worker			ON worker.idUser	= job.idWorker
INNER JOIN	city 					ON city.id			= client.city_id
INNER JOIN	neighborhood 			ON neighborhood.id 	= client.neighborhood_id
WHERE		job.idWorker 								= p_id_worker
AND			worker.city_id								= p_id_city;

END