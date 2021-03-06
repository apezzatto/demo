<?php

	session_start();

	if (isset($_POST["nickname"]))
	{
		$_SESSION["nickname"] = $_POST["nickname"];
	}
	
	if (isset($_POST["smart_quantity1"]))
	{
		$_SESSION["smart_quantity1"] = $_POST["smart_quantity1"];
	}	
	
	if (isset($_POST["smart_quantity2"]))
	{
		$_SESSION["smart_quantity2"] = $_POST["smart_quantity2"];
	}	

	if (isset($_POST["smart_quantity3"]))
	{
		$_SESSION["smart_quantity3"] = $_POST["smart_quantity3"];
	}			
?>
<!DOCTYPE html>
<html>
    <head>
        <title>Notebooks</title>
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
			<form action="page2.php" name="notebooks" method="POST">
				<div class="left">
				  <img class="imagestore" src="img/lap1.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='lap_quantity1' min='1' max='5' ";
						
						if (isset($_SESSION["lap_quantity1"]))
						{
							echo "value='".$_SESSION["lap_quantity1"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="center">
				  <img class="imagestore" src="img/lap2.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='lap_quantity2' min='1' max='5' ";
						
						if (isset($_SESSION["lap_quantity2"]))
						{
							echo "value='".$_SESSION["lap_quantity2"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="right">
				  <img class="imagestore" src="img/lap3.jpg"/>
				  <div class="container">
					<p>Quantity: </p>
					<?php
						echo "<input type='number' name='lap_quantity3' min='1' max='5' ";
						
						if (isset($_SESSION["lap_quantity3"]))
						{
							echo "value='".$_SESSION["lap_quantity3"]."'>";
						}
						else
						{
							echo ">";
						}
					?>
				  </div>
				</div>
				<div class="bottom1">
					<input class="fancybutton" type="submit" value="Proceed"/>
				</div>
			</form>
		</div>		
		<?php 
			}
		?>		
		<footer>&copy; All rights reserved</footer>
    </body>
</html>