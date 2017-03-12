CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_USER_BY_EMAIL_PASSWORD`(
	IN p_email		VARCHAR(50),
    IN p_password 	CHAR(8)
)
BEGIN

SELECT 	idUser
FROM	user a
WHERE	a.Email = p_email
AND		a.Password = p_password;

END