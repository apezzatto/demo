CREATE DEFINER=`b75188d3b80bd5`@`%` PROCEDURE `SP_INS_JOB`(
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

END