function IsNameEmpty() {
    if (document.getElementById('Name').value == "") {
        return 'Name should not be empty\n';
    }
    else { return ""; }
}

function IsNameInvalid() {
    if (document.getElementById('Name').value.indexOf("@") != -1) {
        return 'Name should not contain @\n';
    }
    else { return ""; }
}
function IsNameTooShort() {
    if (document.getElementById('Name').value.length < 2) {
        return 'Name should not contain less than 2 character\n';
    }
    else { return ""; }
}
function IsValid() {
    var FinalErrorMessage = "";
    FinalErrorMessage += IsNameEmpty();
    FinalErrorMessage += IsNameInvalid();
    FinalErrorMessage += IsNameTooShort();
    if (FinalErrorMessage.length > 0) {
        FinalErrorMessage = "Errors:\n" + FinalErrorMessage;
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}
function MyOnLoad() {
    var ls = document.getElementsByTagName("button")
    alert(ls.length)
}


//do osobnego pliku
function ToggleFold(className, btnId) {
    var ls = document.querySelectorAll("[class^=" + className + "-]");
    var b = document.getElementById(btnId);
    if (sessionStorage.getItem(b.id) == null) sessionStorage.setItem(b.id, b.value);

    var str, disp;
    if (sessionStorage.getItem(b.id) == "Fold") {//fold items
        b.value = "Expand"; 
        disp = "none";
    }
    else {//unfold items
        b.value = "Fold";
        disp = "";
    }
    
    for (var i = 0; i < ls.length; i++) {
        ls[i].style.display = disp;
        ls[i].value = "Fold";
    }

    sessionStorage.setItem(b.id, b.value);
}

window.onload = function () {
    var lsb = document.getElementsByTagName("button");

    // If sessionStorage is storing default values (ex. name), exit the function and do not restore data
    if (sessionStorage.getItem('name') == "name") {
        return;
    }

    // If values are not blank, restore them to the fields
    var name = sessionStorage.getItem('name');
    if (name !== null) $('#inputName').val(name);

    var email = sessionStorage.getItem('email');
    if (email !== null) $('#inputEmail').val(email);

    var subject = sessionStorage.getItem('subject');
    if (subject !== null) $('#inputSubject').val(subject);

    var message = sessionStorage.getItem('message');
    if (message !== null) $('#inputMessage').val(message);
}

// Before refreshing the page, save the form data to sessionStorage
window.onbeforeunload = function () {
    sessionStorage.setItem("name", $('#inputName').val());
    sessionStorage.setItem("email", $('#inputEmail').val());
    sessionStorage.setItem("subject", $('#inputSubject').val());
    sessionStorage.setItem("message", $('#inputMessage').val());
}

