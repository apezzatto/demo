<?php
	if (isset($_POST["nickname"]))
	{
		$_SESSION["nickname"] = $_POST["nickname"];
	}
?>
<!DOCTYPE html>
<html>
	<head>
        <title>Script Page</title>
        <link rel="stylesheet" type="text/css" href="css/styles.css" title="Style1">
		<script src="js/functions.js"></script>
    </head>
	<body>
		<div class="top">
			<?php 
				if (isset($_SESSION["nickname"]))
				{
					echo "<div class='menu'><a href='page1.php'>Notebooks</a></div>";
					echo "<div class='menu'><a href='page2.php'>Smartphones</a></div>";
					echo "<div class='about'>Welcome, ".$_SESSION["nickname"];
					echo "!</div>";
				}
				else
				{
					echo "<div class='about'>Welcome, guest!</div>";
				}
			?>
		</div>
    </body>
</html>



