function checkNumericValue(e)
{
    var key;
    key=e.which?e.which:e.keyCode;
    if (key >= 48 && key <= 57 || key == 13)
    {
    return true;
    }
    else
    {
    alert("Enter Numeric Value Only"); 
    return false;
    }
}