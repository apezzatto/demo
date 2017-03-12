CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_INS_USER`(
        IN  p_name       		VARCHAR(20) , 
        IN  p_middlename 		VARCHAR(15) , 
        IN  p_lastname   		VARCHAR(30) , 
        IN  p_address1   		VARCHAR(100), 
        IN  p_address2   		VARCHAR(50) , 
        IN  p_id_province		INT			,
        IN  p_id_city			INT			,
        IN  p_id_neighborhood	INT			,
        IN  p_zipcode    		CHAR(6)     ,
        IN  p_email      		VARCHAR(50) , 
        IN  p_password   		CHAR(8)        
     )
BEGIN

	INSERT INTO USER
		(
		`idUser`			,
		`Name`				,
		`MiddleName`		,
		`LastName`			,
		`Address1`			,
		`Address2`			,
        `province_id`		,
        `city_id`			,
        `neighborhood_id`	,
		`ZipCode`			,
		`Email`				,
		`Password`
		)
	VALUES
		(
        (SELECT UUID())		,
        p_name				,     	 
        p_middlename		,
        p_lastname  		,
        p_address1  		,
        p_address2  		,
        p_id_province		,
        p_id_city			,
        p_id_neighborhood	,
        p_zipcode   		,
        p_email     		,
        p_password
		);
END