var headermessage = "Please fix the issues bellow before continue:<br><br>";
var nameError = "";
var emailError = "";
var emailFormatError = "";
var passError = "";
var confPassError = "";
var phoneError = "";
var websiteError = "";

function validate()
{
	var pattEmail = /[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,3}$/g;
	var pattPhone = /^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$/g;
	var pattSite = /^http:\/\/[a-zA-Z0-9]+.[a-zA-Z0-9]+.[a-zA-Z0-9]*$/g;	
	
	var name = document.forms["signup"]["name"].value;
	var email = document.forms["signup"]["email"].value;
	var pass = document.forms["signup"]["pass"].value;
	var confpass = document.forms["signup"]["confpass"].value;
	var telephone = document.forms["signup"]["telephone"].value;
	var website = document.forms["signup"]["site"].value;
	var noErrorsFlag = true;

	nameError = "";
	emailError = "";
	passError = "";
	confPassError = "";
	phoneError = "";
	websiteError = "";
	
	document.getElementById("nameMark").innerHTML = "";	
	document.getElementById("emailMark").innerHTML = "";
	document.getElementById("passMark").innerHTML = "";
	document.getElementById("confPassMark").innerHTML = "";
	document.getElementById("telMark").innerHTML = "";
	document.getElementById("siteMark").innerHTML = "";
	
	if (name == null || name == "")
	{
		nameError = "- Name is mandatory.<br>";
		document.getElementById("nameMark").innerHTML = "*";
		noErrorsFlag = false;
	}
	
	if (email == null || email == "")
	{
		emailError = "- E-mail is mandatory.<br>";
		document.getElementById("emailMark").innerHTML = "*";
		noErrorsFlag = false;
	}
	else if (!pattEmail.test(email))
	{
		emailError = "- E-mail MUST be in 'x@y.z' format.<br>";
		document.getElementById("emailMark").innerHTML = "*";
		noErrorsFlag = false;
	}

	if (pass != confpass)
	{
		passError = "- The passwords typed don't matches.<br>";
		document.getElementById("passMark").innerHTML = "*";
		document.getElementById("confPassMark").innerHTML = "*";
		noErrorsFlag = false;		
	}
	
	if (pass == null || pass == "")
	{
		passError = "- Password is mandatory.<br>";
		document.getElementById("passMark").innerHTML = "*";
		noErrorsFlag = false;
	}

	if (confpass == null || confpass == "")
	{
		confPassError = "- Confirm Password is mandatory.<br>";
		document.getElementById("confPassMark").innerHTML = "*";
		noErrorsFlag = false;
	}
	
	if (telephone != null && telephone != "")
	{
		if (!pattPhone.test(telephone))
		{
			phoneError = "- Telephone MUST be in 'ddd-ddd-dddd' format.<br>";
			document.getElementById("telMark").innerHTML = "*";
			noErrorsFlag = false;
		}
	}
	
	if (website != null && website != "")
	{	
		if (!pattSite.test(website))
		{
			websiteError = "- Web Site MUST be in 'http://x.y.z' format.<br>";
			document.getElementById("siteMark").innerHTML = "*";
			noErrorsFlag = false;
		}	
	}
	
	printErrors();
	return noErrorsFlag;
}

function printErrors()
{
	document.getElementById("message").style.color = "red";
	document.getElementById("message").style.borderStyle = "solid";
	document.getElementById("message").style.borderColor = "brown";
	document.getElementById("message").style.backgroundColor = "#ffe3c0";
		
	document.getElementById("message").innerHTML = headermessage + nameError + emailError + passError + confPassError + phoneError + websiteError;	
}