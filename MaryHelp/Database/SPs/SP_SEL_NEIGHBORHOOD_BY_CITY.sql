CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_NEIGHBORHOOD_BY_CITY`(
IN		p_id_city		INT
)
BEGIN

SELECT
		Id,
        Name
FROM
		neighborhood
WHERE
		city_id 	= p_id_city;
		
END