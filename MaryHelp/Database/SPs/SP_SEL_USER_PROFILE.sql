CREATE PROCEDURE `SP_SEL_USER_PROFILE` (
IN		p_id_user	VARCHAR(36)
)
BEGIN

SELECT 	Name			,
		MiddleName		,
		LastName		,
		Address1		,
		Address2		,
		ZipCode			,
		Email			,
		Password		,
		neighborhood_Id	,
		city_Id			,
		province_Id
FROM 	user
WHERE	idUser=p_id_user;
END