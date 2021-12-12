app.directive("w3TestDirective", function () {
    return {
        restrict: "E",
        template: "<h1>Made by a directive rs3!</h1>"
    };
});

//$(".Errorclass").each(function () {
//    $(this).hide();
//    console.log("errorclass count");

//});







function NameassignI() {
    $('.intPOS').each(function (i, obj) {
        $(this).attr("name", "Interndata[" + i + "].role");

    });
    $('.intTS').each(function (i, obj) {
        $(this).attr("name", "Interndata[" + i + "].Timespan");

    });
    $('.intCN').each(function (i, obj) {
        $(this).attr("name", "Interndata[" + i + "].CompanyName");

    });

}
function NameassignP() {
    $('.Ptitle').each(function (i, obj) {
        $(this).attr("name", "Projectdata[" + i + "].title");

    });
    $('.PMembers').each(function (i, obj) {
        $(this).attr("name", "Projectdata[" + i + "].Members");

    });
    $('.Pproject_description').each(function (i, obj) {
        $(this).attr("name", "Projectdata[" + i + "].project_description");
        var maxchar = $(this).attr("maxlenn");
        $(this).attr("onkeyup", "checkfunc(this," + i + "," + maxchar + ")");
        $(this).attr("id", "pd"+i+"")
    });
}


   
function checkfunc(event,i,maxlenn) {
    console.log("runninignign : " + i);
    // var $("#pd0").attr("id");
    if (event.value.length <= maxlenn) {
        $("#pd0").siblings(".Infoclass")[i].innerHTML = maxlenn - event.value.length + " Left";
        $("#pd0").siblings(".Errorclass")[i].innerHTML = "";
    } else {
        $("#pd0").siblings(".Errorclass")[i].innerHTML = "Limit Exceeds";
        $("#pd0").siblings(".Infoclass")[i].innerHTML ="";
    }
    
}


var onecopy = ``;
var cnt = 0;

function Icountinput() {
    if (cnt == 0) {
        var inthtml = $(".internqns").html();
        onecopy = inthtml;
        $(".internqns").show();

        cnt++;
    } else {

        cnt++;
    }
 
    var intloopno = $("#21").val();
    console.log("intloopno : " + intloopno);
    var temp = ``;
    if (intloopno > 0) {
        for (i = 0; i < intloopno; i++) {
            if (i == 0) {
                temp = onecopy;
            } else {
                temp += onecopy;
            }
        }
        //console.log("html : " + inthtml);
        $(".internqns").html(temp);
        NameassignI();

    } else {
        $(".internqns").html(``);
    }
}



var copy = ``;
var count = 0;
function Pcountinput() {

    


    if (count == 0) {
        var inthtml = $(".projectqns").html();
        copy = inthtml;
        $(".projectqns").show();

        count++;
    } else {

        count++;
    }
    
    var Ploopno = $("#22").val();
    console.log("Ploopno : " + Ploopno);
    var temp = ``;
    if (Ploopno > 0) {
        for (i = 0; i < Ploopno; i++) {
            if (i == 0) {
                temp = copy;
            } else {
                temp += copy;
            }
        }
        //console.log("html : " + inthtml);
        $(".projectqns").html(temp);
        NameassignP();

    } else {
        $(".projectqns").html(``);
    }
} 




$("#submitbt").click(function (e) {
    console.log("insiddfe submit fun c");
    $(".perc").each(function () {
        if ($(this).html() != ``) {
            console.log("above prevent default perc");
            e.preventDefault();


        } else {
            console.log(" elseeeeee perc ");
            $(this).unbind('#submitbt').submit();

        }          
    })



    $(".Errorclass").each(function () {
        console.log("1111 ");   
        if ($(this).html() != ``) {
            console.log("above prevent default");
            e.preventDefault();


        } else {
            console.log(" elseeeeee ");
            $(this).unbind('#submitbt').submit();
            
        }          
    })



})


$(".allformcon").addClass("addon");
$(".internqns").hide();
$(".projectqns").hide();
$(".perc").hide();
app.controller("ResumeProcess3Controller", function ($scope, $http, $sce, $compile) {
    //$scope.Icountval;
    $scope.tenp = '';
    $scope.IcSetting = function (maxvali) {
        
        
        $scope.Icountval =maxvali;
        
    };


    $scope.PcSetting = function (maxvalp) {


        $scope.Pcountval = maxvalp;

    };
    var letterNumber =/^[a-zA-Z]+$/;

    var letterspe = /^[A-Z@~`!@#$%^&*()_=+\\\\';:\"\\/?><,-]*$/i;
    var dotr = /^[.]*$/i;       
    $scope.numericonly =  function (inputtxt,id) {
        var settle = 0;
        var dummystr = inputtxt;
      
        for (i = 0; i < dummystr.length; i++) {
            if (dummystr[i].match(letterspe)) {
                inputtxt = setCharAt(inputtxt, i - settle, '');
                settle++;         
            }
        }
        
        $("#" + id + "").val(inputtxt);
        if ($scope.checkdot(inputtxt)) {
            console.log("runs error part");
            $("#" + id + "E").html(`<span  class="Errorclass ">Not in Correct format</span>`);
            $("#" + id + "E").show();
        } else {
            $("#" + id + "E").html(``);
        }
    }  

    $scope.checkdot = function (val) {
        var dotcount = 0;
        var dotindex = 0;
        for (i = 0; i < val.length; i++) {
            if (val[i] == ".") {
                dotcount++;
                dotindex = i;
            }

        }

        if (dotcount > 1) {
            return true;
        } else if (dotcount == 1) {
            return dotindex == 1 || dotindex == 2 ? false : true;
        } else {
            return val.length>2?true:false;
        }                          

      
    }

    function setCharAt(str, index, chr) {
        if (index > str.length - 1) return str;
        return str.substring(0, index) + chr + str.substring(index + 1);
    }


});


