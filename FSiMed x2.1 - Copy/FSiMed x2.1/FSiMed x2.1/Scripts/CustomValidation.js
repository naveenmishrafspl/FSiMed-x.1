
$(document).ready(function () {

    $(".txtAmount, .txtAmountLength").keydown(function (e) {

        var vSkipValues = [9, 32, 17, 18, 20, 13, 8, 25, 30];

        var vCnt = 0;

        vSkipValues.forEach(function (a) {

            if (a == e.keyCode) {
                vCnt = 1;
                return false;
            }

        });

        if (vCnt == 0) {

            if (e.keyCode > 95 && e.keyCode < 106 || e.keyCode > 47 && e.keyCode < 58) {
                return true;
            }
            else {
                alert("Enter Numeric Value Only");
                return false;
            }
        }
    });

});
