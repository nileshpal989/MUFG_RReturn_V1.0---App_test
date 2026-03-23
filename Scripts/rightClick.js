var BM = 2; // button middle
var BR = 3; // button right

function mouseDown(e) {
    try { if (event.button == BM || event.button == BR) { return false; } }
    catch (e) { if (e.which == BR) { return false; } }
}
document.oncontextmenu = function () { return false; }
document.onmousedown = mouseDown;