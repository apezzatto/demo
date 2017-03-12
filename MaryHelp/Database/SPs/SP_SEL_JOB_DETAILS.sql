CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_JOB_DETAILS`(
IN		p_id_job	CHAR(36)
)
BEGIN

SELECT		job.dtJobStarts,
			job.hoursConclusion,
			job.dtJobFinishes,
			job.Price,
			worker.Email as WorkerEmail,
			worker.Name as WorkerName,
			worker.LastName as WorkerLastName,
			worker.Address1 as WorkerAddress1,
			worker.Address2 as WorkerAddress2,
			wcity.Name as WorkerCityName,
			wneighbor.Name as WorkerNeighborhoodName,
			client.Email as ClientEmail,
			client.Name as ClientName,
			client.LastName as ClientLastName,
			client.Address1 as ClientAddress1,
			client.Address2 as ClientAddress2,
            client.ZipCode as ClientZipCode,
			ccity.Name as ClientCityName,
			cneighbor.Name as ClientNeighborhoodName           

FROM		job
INNER JOIN	user as worker 				ON job.idWorker 			= worker.idUser
INNER JOIN	user as client 				ON job.idClient 			= client.idUser
INNER JOIN	city as wcity				ON worker.city_id 			= wcity.Id
INNER JOIN	neighborhood as wneighbor 	ON worker.neighborhood_id 	= wneighbor.Id
INNER JOIN	city as ccity				ON client.city_id 			= ccity.Id
INNER JOIN	neighborhood as cneighbor 	ON client.neighborhood_id 	= cneighbor.Id

WHERE 		job.idJob 												= p_id_job
AND			job.jobOpen												= 2;


END