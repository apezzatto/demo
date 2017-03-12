<?php

	session_start();

	if (isset($_POST["lap_quantity1"]))
	{
		$_SESSION["lap_quantity1"] = $_POST["lap_quantity1"];
	}	
	
	if (isset($_POST["lap_quantity2"]))
	{
		$_SESSION["lap_quantity2"] = $_POST["lap_quantity2"];
	}	

	if (isset($_POST["lap_quantity3"]))
	{
		$_SESSION["lap_quantity3"] = $_POST["lap_quantity3"];
	}

?>
<!DOCTYPE html>
<html>
    <head>
        <title>Smartphones</title>
        <link rel="stylesheet" type="text/css" href="css/styles.css" title="Style1">
		<script src="js/functions.js"></script>
    </head>
    <body>
		<?php 
			include("header.php");
			
			if (!isset($_SESSION["nickname"]) || $_SESSION["nickname"] == "")
			{
		?>
			<p>You are not logged in.</p>
			<a id="return" href="index.php"> Return to Home Page </a>
		<?php 
			}
			else
			{
		?>
		<form name="LogoutForm" action="index.php" method="POST">
			<input type="submit" name="logout" value="Logout">
		</form>
		<div class="main">
			<form action="page1.php" name="Smartphones" method="POST">
				<div class="left">
				  <img class="imagestore" src="img/smart1.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='smart_quantity1' min='1' max='5' ";
						
						if (isset($_SESSION["smart_quantity2"]))
						{
							echo "value='".$_SESSION["smart_quantity1"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="center">
				  <img class="imagestore" src="img/smart2.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='smart_quantity2' min='1' max='5' ";
						
						if (isset($_SESSION["smart_quantity2"]))
						{
							echo "value='".$_SESSION["smart_quantity2"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="right">
				  <img class="imagestore" src="img/smart3.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='smart_quantity3' min='1' max='5' ";
						
						if (isset($_SESSION["smart_quantity2"]))
						{
							echo "value='".$_SESSION["smart_quantity3"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="bottom1">
					<input class="fancybutton" type="submit" value="Back to laptops"/>
				</div>
			</form>
		</div>		
		<?php 
			}
		?>
		<footer>&copy; All rights reserved</footer>
	</body>
</html>



