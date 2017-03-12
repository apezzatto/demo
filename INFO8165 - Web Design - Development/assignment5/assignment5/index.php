<?php
	session_start();
	
	if (isset($_POST["logout"]))
	{
		session_unset();
		session_destroy();
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
		<?php 
			include("header.php");
		?>
		<div class="main">
			<form action="page1.php" name="signin" method="POST">
				<table>
					<tr>
						<th class="header" colspan="2">
							Sign In
						</th>
					</tr>
					<tr>
						<td><input type="text" name="nickname" placeholder="Nickname" pattern="[A-Za-z]{5}" autofocus required></td>
					</tr>					
					<tr>
						<td class="bottom">
							<input type="submit" value="Enter Store" class="fancybutton"">
						</td>
					</tr>	
				</table>
				<p id="message"></p>			
			</form>
		</div>		
		<footer>&copy; All rights reserved</footer>
    </body>
</html>



