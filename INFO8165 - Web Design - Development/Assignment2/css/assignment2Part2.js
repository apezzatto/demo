var slideImages = new Array();
var slideLinks = new Array();

function SlideShowImages()
{
    for (i = 0; i < SlideShowImages.arguments.length; i ++)
    {
        slideImages[i] = new Image();
        slideImages[i].src = SlideShowImages.arguments[i];
    }
}

function SlideShowLinks()
{
    for (i = 0; i < SlideShowLinks.arguments.length; i ++)
    {
        slideLinks[i] = SlideShowLinks.arguments[i];
    }
}


function GoToShow()
{
    if (!window.winslide || winslide.closed)
    {
        winslide = window.open(slideLinks[whichLink]);
    }
    else
    {
        winslide.location = slideLinks[whichLink];
    }
    winslide.focus();
}

// TO DO: Configure the paths of the images
SlideShowImages("img/pct1.jpg", "img/pct3.jpg", "img/pct4.jpg", "img/pct5.jpg");

// TO DO: Configure corresponding target links
SlideShowLinks("http://www.cruiseweb.com", "http://www.aloha-hawaiian.com", "http://www.astonhotels.com", "http://www.pixabay.com");

// TO DO: Configure the speed of the slideshow, in milliseconds
var slideShowSpeed = 3000;
var whichLink = 0;
var whichImage = 0;

function SlideIt()
{
    if (!document.images)
    {
        return;
    }

    document.images.slide.src = slideImages[whichImage].src;
    whichLink = whichImage;

    if (whichImage < slideImages.length - 1)
    {
        whichImage ++;
    }
    else
    {
        whichImage = 0;
    }
    setTimeout("SlideIt()", slideShowSpeed);
}



