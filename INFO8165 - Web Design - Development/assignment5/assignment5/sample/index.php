<?php

    session_start();

/*
    require("header.php");
*/

    function PrintCounter($maxNumber)
	{
	    for ($i = 0; $i < $maxNumber; $i ++)
		{

			echo "$i <br>";

		}
	}
?>

<!DOCTYPE html>
<html>

    <head>
	    <title>Sample</title>
    </head>

    <body>
	    <h1>Example HTML Document - James</h1>

		<form name="Page1Form" action="storePage2.php" method="POST">
		
		    <input type="text" name="userName">
		    <input type="submit" name="submit" value="Go To Store">
		
		</form>
<?php		
	if (isset($_POST["Logout"]))
	{
		
		echo "TEST";
		session_unset();
		session_destroy();
		
	}
	
?>
<?php
//	    PrintCounter(20);
//        include ("footer.php");
?>

    </body>
<html>



