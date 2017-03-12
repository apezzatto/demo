<?php
    session_start();

	if (isset($_POST["quantity"]))
	{
		$_SESSION["Quantity"] = $_POST["quantity"];
	}

?>

<!DOCTYPE html>
<html>

    <head>
	    <title>Store Page 3</title>
    </head>

    <body>
	    <h1>Store Page 3</h1>

<?php
//        echo $_SESSION["myUserName"];
//		echo "You have selected " . $_SESSION['Quantity'];
?>

        <form name="storePage3Form" action="storePage2.php" method="POST">
		    <input type="submit" value="Go To Store Page 2">
        </form>

		
		
    </body>
<html>



