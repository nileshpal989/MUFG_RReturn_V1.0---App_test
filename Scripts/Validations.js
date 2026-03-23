
//This function is used to trim all the white spaces from input field.
function trimAll(sString) {
    while (sString.substring(0, 1) == ' ') {
        sString = sString.substring(1, sString.length);
    }

    while (sString.substring(sString.length - 1, sString.length) == ' ') {
        sString = sString.substring(0, sString.length - 1);
    }
   
    return sString;
}
function removeSpecialCharacters(str) {
    if (str.indexOf("!") != -1 || str.indexOf("<") != -1 || str.indexOf(">") != -1 || str.indexOf("|") != -1) {
        for (var i = 0; i < str.length; i++) {
            var chr = str.charAt(i);
            if (chr == "<")
                str = str.replace("<", "");
            else if (chr == ">")
                str = str.replace(">", "");
            else if (chr == "!>")
                str = str.replace("!", "");
            else if (chr == "|")
                str = str.replace("|", "");
        }
    }
    return str;
}

//This event is used to prevent input of special characters in the input field.
function checkcharacter(e) {
    if (e.keyCode == '33' || e.keyCode == '60' || e.keyCode == '62' || e.keyCode == '124')
        return false;
    else
        return true;
}

//This event is used to check if special characters are present in the input string or not on keypress.
function validatecharacter(evnt) {
    if (evnt.keyCode == 13)
        return false;
    else {
        if (checkcharacter(evnt) == true)
            return true;
        else {
            alert('!, |, <, and > are not allowed.');
            return false;
        }
    }
}

//This function is used to check if special characters are present in the input string or not.
function checkspecialcharacter(element, message) {
    var _txtvalue = trimAll(element.value);
    if (_txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
        alert(message);
        try {
            element.focus();
            return false;
        }
        catch (err) {
            return false;
        }
    }
    else {
        return true;
    }
}

function Allowdigits(objevent) {
    var keypressed = objevent.keyCode;
    if (keypressed >= 48 && keypressed <= 57)
        return true;
    else
        return false;
}
function AllowDecimalValues(objevent) {
    var keypressed = objevent.keyCode;
    if ((keypressed >= 48 && keypressed <= 57) || keypressed == 46)
        return true;
    else
        return false;
}
function isValidDecimalNumber(sText, message) {
    var RegExpres = /^[0-9]+(\.[0-9]{1,1})?$/;
    if (RegExpres.test(sText.value))
        return true;
    else {
        alert(message);
        return false;
    }
}

function CheckEmail(txtmail, message) {
    var FieldValue;
    FieldValue = txtmail.value;
    var RegExpres = /^['a-zA-Z0-9._-]+@([a-zA-Z0-9._-]+\.)+[a-zA-Z0-9._-]{2,8}$/;
    if (RegExpres.test(FieldValue)) {
        var bValid = true;
        for (i = 0; i < FieldValue.length; i++) {
            if (FieldValue.charAt(i) == ".") {
                if (FieldValue.charAt(i + 1) == ".") {
                    bValid = false;
                    break;
                }
            }
            if (FieldValue.charAt(i) == "@") {
                if (FieldValue.charAt(i + 1) == ".") {
                    bValid = false;
                    break;
                }
            }
        }
        if (bValid) {
            if (FieldValue.charAt(0) == "." || FieldValue.charAt(0) == "@" || FieldValue.charAt(0) == "_" || FieldValue.charAt(0) == "-")
                bValid = false;
            else
                if (FieldValue.charAt(FieldValue.length - 1) == "." || FieldValue.charAt(FieldValue.length - 1) == "@" || FieldValue.charAt(FieldValue.length - 1) == "_" || FieldValue.charAt(FieldValue.length - 1) == "-")
                    bValid = false;
                else
                    bValid = true;
        }
    }
    else {
        bValid = false;
    }
    if (!bValid) {
        alert(message);
        return false;
    }
    else
        return true;
}
function isValidDate(sText) {
    var result = true;
    var dateStr = sText;
    var slash1 = dateStr.indexOf("/");
    // if no slashes, invalid date
    if (slash1 == -1) { return false; }
    var dateMonth = dateStr.substring(0, slash1)
    var dateMonthAndYear = dateStr.substring(slash1 + 1, dateStr.length);
    var slash2 = dateMonthAndYear.indexOf("/");
    if (slash2 == -1) { slash2 = dateMonthAndYear.indexOf("-"); }
    // if not a second slash, invalid date
    if (slash2 == -1) { return false; }
    var dateDay = dateMonthAndYear.substring(0, slash2);
    var dateYear = dateMonthAndYear.substring(slash2 + 1, dateMonthAndYear.length);
    if ((dateMonth == "") || (dateDay == "") || (dateYear == "")) { return false; }
    // if any non-digits in the month, invalid date
    for (var x = 0; x < dateMonth.length; x++) {
        var digit = dateMonth.substring(x, x + 1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }
    // convert the text month to a number
    var numMonth = 0;
    for (var x = 0; x < dateMonth.length; x++) {
        digit = dateMonth.substring(x, x + 1);
        numMonth *= 10;
        numMonth += parseInt(digit);
    }
    if ((numMonth <= 0) || (numMonth > 12)) { return false; }
    // if any non-digits in the day, invalid date
    for (var x = 0; x < dateDay.length; x++) {
        digit = dateDay.substring(x, x + 1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }
    // convert the text day to a number
    var numDay = 0;
    for (var x = 0; x < dateDay.length; x++) {
        digit = dateDay.substring(x, x + 1);
        numDay *= 10;
        numDay += parseInt(digit);
    }
    if ((numDay <= 0) || (numDay > 31)) { return false; }
    // February can't be greater than 29 (leap year calculation comes later)
    if ((numMonth == 2) && (numDay > 29)) { return false; }
    // check for months with only 30 days
    if ((numMonth == 4) || (numMonth == 6) || (numMonth == 9) || (numMonth == 11)) {
        if (numDay > 30) { return false; }
    }
    // if any non-digits in the year, invalid date
    for (var x = 0; x < dateYear.length; x++) {
        digit = dateYear.substring(x, x + 1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }
    // convert the text year to a number
    var numYear = 0;
    for (var x = 0; x < dateYear.length; x++) {
        digit = dateYear.substring(x, x + 1);
        numYear *= 10;
        numYear += parseInt(digit);
    }
    // Year must be a 2-digit year or a 4-digit year
    //if ( (dateYear.length != 2) && (dateYear.length != 4) ) { return false; }
    if (dateYear.length != 4) { return false; }
    // if 2-digit year, use 50 as a pivot date
    if ((numYear < 50) && (dateYear.length == 2)) { numYear += 2000; }
    if ((numYear < 100) && (dateYear.length == 2)) { numYear += 1900; }
    if ((numYear <= 0) || (numYear > 9999)) { return false; }
    // check for leap year if the month and day is Feb 29
    if ((numMonth == 2) && (numDay == 29)) {
        var div4 = numYear % 4;
        var div100 = numYear % 100;
        var div400 = numYear % 400;
        // if not divisible by 4, then not a leap year so Feb 29 is invalid
        if (div4 != 0) { return false; }
        // at this point, year is divisible by 4. So if year is divisible by
        // 100 and not 400, then it's not a leap year so Feb 29 is invalid
        if ((div100 == 0) && (div400 != 0)) { return false; }
    }
    // date is valid
    return true;
}


function addCommas(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}


function formatAmt(str)
{
    var s = '';
    s=str.toFixed(2);
    return s;
}

