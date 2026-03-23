
function disable_parent() {
    try {
        if (window.opener != null) {
            window.opener.InitReq();
            window.opener.top.document.getElementById("processMessage").style.display = 'none';
        }

    }
    catch (err) {
        try { window.opener.top.InitReq(); } catch (err) { }
    }
}

function enable_parent() {
    try {
        if (window.opener != null) {
            window.opener.EndRequest();
            //window.opener.top.document.getElementById("processMessage").style.display = 'block';
        }

    }
    catch (err) {
        try { window.opener.top.EndRequest(); } catch (err) { }
    }
}
var GiveFocus = "FALSE";
function CheckFocus() {
    if (GiveFocus == "FALSE") {

        self.focus();

    }
    else {
        return;
    }
}

function Change(obj) {
    GiveFocus = "TRUE";
    CheckFocus();
    obj.focus();
}

function focusOnOPener() {
    GiveFocus = 'FALSE';
    CheckFocus();
}
function focusOnOpenerpopup() {
    GiveFocus = 'FALSE';
    window.opener.focus();
    try {
        window.opener.focusOnOPener();
    }
    catch (err)
        { }

}


var openWindow = new Array();

function trackOpen(winName) {
    openWindow[openWindow.length] = winName;
}

function closeWindows() {
    try {
        var openCount = openWindow.length;
        for (r = 0; r < openCount; r++) {
            openWindow[r].close();
        }
    }
    catch (err) {
    }
}


function open_popup(pagename, height, width, winname)//function to open popup window
{

    var page_name = pagename;           //Page to be opened
    var screenheight = screen.height;   //This is the client's screen height
    var screenwidth = screen.width;     //This is the client's screen width
    var windowheight = height;          //Popup window's height
    var windowwidth = width;            //Popup window's width
    var postop = parseInt((screenheight - windowheight) / 2);  //To set window's position horizontally centered
    var posleft = parseInt((screenwidth - windowwidth) / 2);   //To set window's position vertically centered
    try { $get('updateProgress').style.display = 'block'; } catch (err) { }
    try { window.opener.processMessage.style.display = 'none'; } catch (err) { }
  
    winname = window.open(page_name, '_blank', 'height=' + height + ',width=' + width + ',top=' + postop + ',left=' + posleft + ',status=no,scrollbars=yes,menubar=no,location=no,alwaysRaised=yes');

   // window.document.getElementsByTagName("body")[0].focus(); 
    if (window.focus) { winname.focus(); }
    if (!(winname.closed)) {
        //set focus back to child window
        winname.focus();
    }
    trackOpen(winname);
    return false;
}

function open_popup_Excel(pagename, height, width, winname)//function to open popup window
{
    var page_name = pagename;           //Page to be opened
    var screenheight = screen.height;   //This is the client's screen height
    var screenwidth = screen.width;     //This is the client's screen width
    var windowheight = height;          //Popup window's height
    var windowwidth = width;            //Popup window's width
    var postop = parseInt((screenheight - windowheight) / 2);  //To set window's position horizontally centered
    var posleft = parseInt((screenwidth - windowwidth) / 2);   //To set window's position vertically centered
    try { $get('updateProgress').style.display = 'block'; } catch (err) { }
    try { window.opener.processMessage.style.display = 'none'; } catch (err) { }
    winname = window.open(page_name, '_blank', 'height=' + height + ',width=' + width + ',top=' + postop + ',left=' + posleft + ',status=no,menubar=no,location=no,alwaysRaised=yes');
    if (window.focus) { winname.focus(); }
    //trackOpen(winname);
    return false;
}

function redirectUser(windowLocation) {
    window.location = windowLocation;
}