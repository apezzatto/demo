CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_CITY_BY_PROVINCE`(
IN			p_id_province	INT)
BEGIN

SELECT
		Id,
        Name
FROM
		CITY
WHERE
		province_id = p_id_province;
END