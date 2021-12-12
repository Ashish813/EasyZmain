$(".allformcon").addClass("addon");


app.controller("ResumeProcess4Controller", function ($scope, $http, $sce, $compile) {
    $scope.checkk = "Ashsih check";
    $scope.skillsarray = [];
    $scope.certiarray = [];
    $scope.hobbarray = [];
    $scope.langarray = [];
    $scope.submitvar = false;




    $scope.addfunc = function (skillvalue) {
        console.log("skillvalue : " + skillvalue);
        if (skillvalue != "" && skillvalue != undefined) {
            $scope.skillsarray.push(skillvalue);
            $scope.skvalue = '';
            console.log("array data : " + $scope.skillsarray);
        } else {
            jQuery("#23").focus();
            
        }
        
    }

    $scope.addCfunc = function (certitext) {
        if (certitext != "" && certitext != undefined) {
            $scope.certiarray.push(certitext);
            $scope.certivalue = '';
            console.log("certi array data : " + $scope.certiarray);
        } else {
            jQuery("#24").focus();

        }
    }


    $scope.addHfunc = function (hobbtext) {

        if (hobbtext != "" && hobbtext != undefined) {
            $scope.hobbarray.push(hobbtext);
            $scope.hobbvalue = '';
            console.log("hobb array data : " + $scope.certiarray);
        } else {
            jQuery("#26").focus();

        }

    }

    $scope.addLfunc = function (langtext) {

        if (langtext != "" && langtext != undefined) {
            $scope.langarray.push(langtext);
            $scope.langvalue = '';
            console.log("Lang array data : " + $scope.langarray);
        } else {
            jQuery("#25").focus();
        }

    }

 

});


$("#submitbtt").click(function (e) {
    
    console.log("insiddfe submit fun ccccccccccc : ");

    var scope = angular.element(document.getElementById('RP4')).scope();
  


    if (scope.hobbarray.length == 0 || scope.certiarray.length == 0 || scope.langarray.length == 0 || scope.skillsarray.length == 0) {
        console.log("inside new scope check ");
        e.preventDefault();
    } else {
        console.log("else new scope check");

        $(this).unbind('#submitbt').submit();
    }
    setTimeout(function () { 
        console.log("Inside timeout")
        var eachcount = 0;
        $(".Errorclass").each(function () {
            console.log("1111");
            
            if ($(this).html() != ``) {
                console.log("above prevent default");
                eachcount++;
                e.preventDefault();

                eachcount == 1 ? $(this).prev("input").focus() :'';


            }
        })

    }, 200);






})



