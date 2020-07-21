window.jsfunction = {
    focusElement: function (id) {
        setTimeout(internalFocus, 10, id);
    }
}

function internalFocus(Id) {
    var element = document.getElementById(Id);
    if (element == null) {
        setTimeout(internalFocus, 10, Id);
        return;
    }
    element.focus();
    element.select();
}