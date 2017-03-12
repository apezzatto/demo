CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_JOBS_BY_CITY`(
IN		p_city_id		INT				,
IN		p_user_id		VARCHAR(36)	
)
BEGIN

SELECT 		A.idJob						,
			A.dtJobCreation				,
            A.dtJobStarts				,
            A.hoursConclusion			,
            A.qtyRooms					,
            A.qtyBaths					,
            A.qtyWindows				,
            A.qtyMirrors				,
            A.vacuum					,
            A.laundry					,
            A.garage					,
            A.dishes					,
            A.price						,
            A.jobOpen					,
            B.Email as clientEmail		,
            D.Name as NeighborhoodName
FROM 		JOB 			AS A
INNER JOIN 	USER 			AS B 	ON A.idClient 			= B.idUser
INNER JOIN 	NEIGHBORHOOD 	AS D	ON B.neighborhood_Id 	= D.Id
WHERE		B.city_id			= p_city_id
AND			A.idClient			<> p_user_id
AND			A.jobOpen			= 1;

END