function Control() {
    var hdnValue = document.getElementById('hdnResult').value;
    var txtValue = document.getElementById('txtAnswer').value;
    if (hdnValue != txtValue) {
        document.getElementById('txtAnswer').style.border = "3px solid red";
        return false;
    }
    else {
        return true;
    }
}