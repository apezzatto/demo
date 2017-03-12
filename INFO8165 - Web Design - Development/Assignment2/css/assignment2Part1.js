function ChangeParagraph1()
{

    document.getElementById("paragraph1").innerHTML = "New Text!";

}

function BackParagraph1Original()
{

    document.getElementById("paragraph1").innerHTML = "Original Text";

}

function InsertCurrentDate()
{

    document.getElementById("dateParagraph").innerHTML = Date();

}

function ChangeColor()
{

    document.getElementById("ParagraphWithStyle").style.color = "red";

}

function BackBlackColor()
{

    document.getElementById("ParagraphWithStyle").style.color = "black";

}


