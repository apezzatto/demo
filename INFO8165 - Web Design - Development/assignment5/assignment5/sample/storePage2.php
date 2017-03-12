<?php
    session_start();

	if (isset($_POST["userName"]))
	{
		$_SESSION["myUserName"] = $_POST["userName"];
	}
?>

<!DOCTYPE html>
<html>

    <head>
	    <title>Store Page 2</title>
    </head>

    <body>
	    <h1>Store Page 2</h1>

<?php
//        echo $_SESSION["myUserName"];
?>

        <form name="storePage2Form" action="storePage3.php" method="POST">
            <select name="quantity">
			
<?php
			for ($i = 0; $i < 10; $i ++)
			{
				
				echo "<option value='$i'";
				
				if (isset($_SESSION["Quantity"]) && $_SESSION["Quantity"] == $i)
				{
					
					echo " selected='selected'";
					
				}
				
				echo ">$i</option>";
				
			}
?>
            </select>

    		<input type="submit" value="Go To Store Page 3">
        </form>

        <form name="LogoutForm" action="index.php" method="POST">
    		<input type="submit" name="Logout" value="Logout">
        </form>
		

    </body>
<html>



