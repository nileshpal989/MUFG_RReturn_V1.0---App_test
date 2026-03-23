
var prm=Sys.WebForms.PageRequestManager.getInstance();
prm.add_initializeRequest(InitReq);
prm.add_endRequest(EndRequest);

function InitReq(sender, args)
{   
      var myHeight = 0;

    if (typeof (window.innerWidth) == 'number') {

        //Non-IE

        myHeight = window.innerHeight;

    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {

        //IE 6+ in 'standards compliant mode'

        myHeight = document.documentElement.clientHeight;

    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {

        //IE 4 compatible

        myHeight = document.body.clientHeight;

    }


    var bodyHeight = window.availHeight;

    var myWidth = 0;

    if (typeof (window.innerWidth) == 'number') {

        //Non-IE

        myWidth = window.innerWidth;

    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {

        //IE 6+ in 'standards compliant mode'

        myWidth = document.documentElement.clientWidth;

    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {

        //IE 4 compatible

        myWidth = document.body.clientWidth;

    }


    var bodyWidth = myWidth;
    var bodyHeight = myHeight;

    document.getElementById('progressBackgroundMain').style.height = bodyHeight + 'px';
    document.getElementById('progressBackgroundMain').style.width = bodyWidth + 'px';
    document.getElementById('processMessage').style.height = '32' + 'px';
    document.getElementById('processMessage').style.width = '32' + 'px';
    document.getElementById('processMessage').style.top = (bodyHeight / 2 - 16) + 'px';
    document.getElementById('processMessage').style.left = (bodyWidth / 2 - 32) + 'px';
    $get('updateProgress').style.display = 'block';
   
}

function EndRequest(sender, args) {

   $get('updateProgress').style.display = 'none';
}

function Visible_AllDropdowns(strvisibility) 
{
    detect_Browser();   
    if (detectb != '' )
    {
        if (strvisibility == 'hidden')
        {
            if (detectb !='IE6')
            {
                strvisibility = 'visible';
            }
         }
    }  
    for (i=0; i<document.forms[0].length; i++)
    {
        doc = document.forms[0].elements[i];
        switch (doc.type) 
        {
           case "select-one" :
               doc.style.visibility=strvisibility;
               break;                      
           case "select-multiple" :
               doc.style.visibility=strvisibility;
               break;                                                      
           default :
                break;
        }
    }
}

 //This function is used to detect the browser and its version.
    function detect_Browser()
    {
    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent))
    { 
    var ieversion=new Number(RegExp.$1)
    if(ieversion>=6)
    {
    if (ieversion==8)
    {
    detectb='IE8';
    try 
    { 
    document.getElementById('hdnbrowser').value='IE8';
    } 
    catch(err)
    {
    }
    }
    else 
    if (ieversion==7)
    {
    detectb='IE7';
    try 
    { 
    document.getElementById('hdnbrowser').value='IE7';
    } 
    catch(err)
    {
    }
    }
    else if (ieversion==6)
    {
    detectb='IE6';
    try 
    { 
    document.getElementById('hdnbrowser').value='IE6';
    } 
    catch(err)
    {
    }
    }
    }
    }
    }