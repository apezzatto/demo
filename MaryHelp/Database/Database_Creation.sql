-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema acsm_2a1c579781f0cfc
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema acsm_2a1c579781f0cfc
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `acsm_2a1c579781f0cfc` DEFAULT CHARACTER SET utf8 ;
USE `acsm_2a1c579781f0cfc` ;

-- -----------------------------------------------------
-- Table `acsm_2a1c579781f0cfc`.`province`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `acsm_2a1c579781f0cfc`.`province` (
  `Id` INT(11) NOT NULL,
  `Name` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `acsm_2a1c579781f0cfc`.`city`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `acsm_2a1c579781f0cfc`.`city` (
  `Id` INT(11) NOT NULL,
  `Name` VARCHAR(45) NULL DEFAULT NULL,
  `province_Id` INT(11) NOT NULL,
  PRIMARY KEY (`Id`, `province_Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  INDEX `fk_city_province_idx` (`province_Id` ASC),
  CONSTRAINT `fk_city_province`
    FOREIGN KEY (`province_Id`)
    REFERENCES `acsm_2a1c579781f0cfc`.`province` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `acsm_2a1c579781f0cfc`.`neighborhood`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `acsm_2a1c579781f0cfc`.`neighborhood` (
  `Id` INT(11) NOT NULL,
  `Name` VARCHAR(45) NULL DEFAULT NULL,
  `city_Id` INT(11) NOT NULL,
  `province_Id` INT(11) NOT NULL,
  PRIMARY KEY (`Id`, `city_Id`, `province_Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),
  INDEX `fk_neighborhood_city1_idx` (`city_Id` ASC, `province_Id` ASC),
  CONSTRAINT `fk_neighborhood_city1`
    FOREIGN KEY (`city_Id` , `province_Id`)
    REFERENCES `acsm_2a1c579781f0cfc`.`city` (`Id` , `province_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `acsm_2a1c579781f0cfc`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `acsm_2a1c579781f0cfc`.`user` (
  `idUser` CHAR(36) NOT NULL,
  `Name` VARCHAR(20) NOT NULL,
  `MiddleName` VARCHAR(30) NULL DEFAULT NULL,
  `LastName` VARCHAR(30) NOT NULL,
  `Address1` VARCHAR(100) NOT NULL,
  `Address2` VARCHAR(50) NULL DEFAULT NULL,
  `ZipCode` CHAR(6) NOT NULL,
  `Email` VARCHAR(50) NOT NULL,
  `Password` CHAR(8) NOT NULL,
  `neighborhood_Id` INT(11) NOT NULL,
  `city_Id` INT(11) NOT NULL,
  `province_Id` INT(11) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `idUser_UNIQUE` (`idUser` ASC),
  INDEX `fk_user_neighborhood1_idx` (`neighborhood_Id` ASC, `city_Id` ASC, `province_Id` ASC),
  CONSTRAINT `fk_user_neighborhood1`
    FOREIGN KEY (`neighborhood_Id` , `city_Id` , `province_Id`)
    REFERENCES `acsm_2a1c579781f0cfc`.`neighborhood` (`Id` , `city_Id` , `province_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'This table handles user\'s basic information';


-- -----------------------------------------------------
-- Table `acsm_2a1c579781f0cfc`.`job`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `acsm_2a1c579781f0cfc`.`job` (
  `idClient` CHAR(36) NOT NULL,
  `idWorker` CHAR(36) NULL DEFAULT NULL,
  `idJob` CHAR(36) NOT NULL,
  `dtJobCreation` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `dtJobStarts` TIMESTAMP NULL DEFAULT NULL,
  `dtJobFinishes` TIMESTAMP NULL DEFAULT NULL COMMENT 'Estimated date&time job end',
  `dtJobEnd` DATETIME NULL DEFAULT NULL COMMENT 'Real date&time job ends',
  `hoursConclusion` DOUBLE NOT NULL,
  `qtyRooms` INT(11) NOT NULL,
  `qtyBaths` INT(11) NOT NULL,
  `qtyWindows` INT(11) NOT NULL,
  `qtyMirrors` INT(11) NOT NULL,
  `vacuum` TINYINT(1) NOT NULL,
  `laundry` TINYINT(1) NOT NULL,
  `garage` TINYINT(1) NOT NULL,
  `dishes` TINYINT(1) NOT NULL,
  `jobOpen` INT(11) NOT NULL DEFAULT '1' COMMENT 'FINISHED = 0, OPEN = 1, APPROVAL = 2, APPROVED = 3, CANCELED_BY_WORKER = 4, CANCELED_BY_CLIENT = 5',
  `price` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`idJob`),
  UNIQUE INDEX `idJob_UNIQUE` (`idJob` ASC),
  INDEX `fk_Job_user1_idx` (`idClient` ASC),
  INDEX `fk_Job_user2_idx` (`idWorker` ASC),
  CONSTRAINT `fk_Job_user1`
    FOREIGN KEY (`idClient`)
    REFERENCES `acsm_2a1c579781f0cfc`.`user` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Job_user2`
    FOREIGN KEY (`idWorker`)
    REFERENCES `acsm_2a1c579781f0cfc`.`user` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

USE `acsm_2a1c579781f0cfc` ;

-- -----------------------------------------------------
-- procedure SP_INS_JOB
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_INS_JOB`(
IN		p_idClient			CHAR(36),
IN		p_dtJobStarts		DATETIME,
IN		p_hoursConclusion	DOUBLE,
IN		p_qtyRooms			INT,
IN		p_qtyBaths			INT,
IN		p_qtyWindows		INT,
IN		p_qtyMirrors		INT,
IN		p_vacuum			TINYINT(1),
IN		p_laundry			TINYINT(1),
IN		p_garage			TINYINT(1),
IN		p_dishes			TINYINT(1),
IN 		p_price				DECIMAL(10,2)
)
BEGIN

INSERT INTO JOB
(
`idJob`				,
`idClient`			,
`dtJobStarts`		,
`hoursConclusion`	,
`qtyRooms`			,
`qtyBaths`			,
`qtyWindows`		,
`qtyMirrors`		,
`vacuum`			,
`laundry`			,
`garage`			,
`dishes`			,
`price`
)
VALUES
(
(SELECT UUID())		,
p_idClient			,
p_dtJobStarts		,
p_hoursConclusion	,
p_qtyRooms			,
p_qtyBaths			,
p_qtyWindows		,
p_qtyMirrors		,
p_vacuum			,
p_laundry			,
p_garage			,
p_dishes			,
p_price
);

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_INS_USER
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_INS_USER`(
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
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_CITY_BY_PROVINCE
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_CITY_BY_PROVINCE`(
IN			p_id_province	INT)
BEGIN

SELECT
		Id,
        Name
FROM
		CITY
WHERE
		province_id = p_id_province;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_CLIENT_EMAIL_BY_JOBID
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_CLIENT_EMAIL_BY_JOBID`(
IN		p_id_job	CHAR(36)
)
BEGIN

SELECT 		client.email
FROM		job
INNER JOIN	user as client ON job.idClient = client.idUser
WHERE		job.idJob = p_id_job;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_HISTORY_BY_CITY
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_HISTORY_BY_CITY`(
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

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_HISTORY_BY_NEIGHBORHOOD
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_HISTORY_BY_NEIGHBORHOOD`(
IN		p_id_worker			CHAR(36),
IN		p_id_neighborhood	INT
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
AND			worker.neighborhood_id						= p_id_neighborhood;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_JOBS_APPLIED
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_JOBS_APPLIED`(
IN		p_id_worker		CHAR(36)
)
BEGIN

SELECT	idJob,
		dtJobStarts,
        hoursConclusion
FROM	job
WHERE	idWorker 	= 	p_id_worker
AND		jobOpen 	IN(0,2); /*0=CLOSED 2=APPROVING*/

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_JOBS_BY_CITY
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_JOBS_BY_CITY`(
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

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_JOBS_BY_NEIGHBORHOOD
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_JOBS_BY_NEIGHBORHOOD`(
IN		p_neighborhood_id		INT		,
IN		p_user_id				VARCHAR(36)
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
WHERE		B.neighborhood_id			= p_neighborhood_id
AND			A.idClient					<> p_user_id
AND			A.jobOpen					= 1;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_JOB_DETAILS
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_JOB_DETAILS`(
IN		p_id_job	CHAR(36)
)
BEGIN

SELECT		job.dtJobStarts,
			job.hoursConclusion,
			job.dtJobFinishes,
			job.Price,
			worker.Email,
			worker.Name,
			worker.LastName,
			worker.Address1,
			worker.Address2,
			city.Name as CityName,
			neighborhood.Name as NeighborhoodName        
FROM		job
INNER JOIN	user as worker 	ON job.idWorker 			= worker.idUser
INNER JOIN	city 			ON worker.city_id 			= city.Id
INNER JOIN	neighborhood 	ON worker.neighborhood_id 	= neighborhood.Id
WHERE 		job.idJob 									= p_id_job
AND			job.jobOpen									= 2;


END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_NEIGHBORHOOD_BY_CITY
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_NEIGHBORHOOD_BY_CITY`(
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
		
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_PROVINCE
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_PROVINCE`()
BEGIN

SELECT	
		Id,
		Name
FROM	
		province;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_USER_BY_EMAIL_PASSWORD
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_USER_BY_EMAIL_PASSWORD`(
	IN p_email		VARCHAR(50),
    IN p_password 	CHAR(8)
)
BEGIN

SELECT 	idUser
FROM	user a
WHERE	a.Email = p_email
AND		a.Password = p_password;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_SEL_USER_PROFILE
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_SEL_USER_PROFILE`(
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
WHERE	idUser = p_id_user;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_UPD_APPROVE_WORKER
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_UPD_APPROVE_WORKER`(
IN  p_id_job			VARCHAR(36)
)
BEGIN

	UPDATE 	JOB
	SET 	jobOpen		= 3
	WHERE	idJob 		= p_id_job;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_UPD_JOB_STATUS
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_UPD_JOB_STATUS`(		
IN  p_id_job			VARCHAR(36)	,
IN  p_id_worker			VARCHAR(36)	,
IN  p_status_job       	INT
)
BEGIN

	UPDATE 	JOB
	SET 	idWorker	= p_id_worker,
			jobOpen		= p_status_job
	WHERE	idJob 		= p_id_job;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_UPD_REPROVE_WORKER
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_UPD_REPROVE_WORKER`(
IN  p_id_job			VARCHAR(36)
)
BEGIN

	UPDATE 	JOB
	SET 	jobOpen		= 1,
			idWorker 	= null
	WHERE	idJob 		= p_id_job;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SP_UPD_USER
-- -----------------------------------------------------

DELIMITER $$
USE `acsm_2a1c579781f0cfc`$$
CREATE PROCEDURE `SP_UPD_USER`(
		IN  p_id_user			VARCHAR(36)	,
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

	UPDATE 	USER
	SET 	Name			= p_name			,
			MiddleName		= p_middlename		,
			LastName		= p_lastname		,
			Address1		= p_address1		,
			Address2		= p_address2		,
			province_id		= p_id_province		,
			city_id			= p_id_city			,
			neighborhood_id	= p_id_neighborhood	,
			ZipCode			= p_zipcode			,
			Email			= p_email			,
			Password		= p_password
	WHERE	idUser = p_id_user;
END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
