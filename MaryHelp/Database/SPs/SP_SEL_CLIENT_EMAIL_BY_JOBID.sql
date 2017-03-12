CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_SEL_CLIENT_EMAIL_BY_JOBID`(
IN		p_id_job	CHAR(36)
)
BEGIN

SELECT 		client.email
FROM		job
INNER JOIN	user as client ON job.idClient = client.idUser
WHERE		job.idJob = p_id_job;

END