CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_WORKER_DETAILS_BY_JOBID`(
IN 		p_id_job	CHAR(36)
)
BEGIN

SELECT						worker.Name,
							worker.LastName,
							worker.Address1,
							worker.Address2,
							worker.ZipCode,
							city.Name as CityName,
							neighborhood.Name as NeighborhoodName
FROM		job
INNER JOIN	user as worker 	ON job.idWorker 			= worker.idUser
INNER JOIN	city 			ON worker.city_id 			= city.Id
INNER JOIN	neighborhood 	ON worker.neighborhood_id 	= neighborhood.Id
WHERE 		job.idJob 									= p_id_job
AND			job.jobOpen									= 2;

END