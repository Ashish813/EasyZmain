
//web worker 
//if(typeof (Worker) !== "undefined") { 
//    alert("Yes, Web worker is supported");

//}else{
//    alert("No, Web worker is not supported");
//}
//var worker = new Worker("ForWorker.js");  
//worker.onmessage = function (event) {
   
//    $(".formtwo").html(`<h1>abcdefgh</h1>`);
//    $(".formtwo").append(event.data);
//    console.log("event.data : " + event.data);
//};

//worker.terminate();
//web worker end

    $("#nextbid").click(function () {
        //$("#formone").hide();
    })

    $("#prevbid").click(function () {
        //$("#formone").show();
    })

//        - $('body').offset().top
//var rightheight = $(document).height();
//rightheight += 71;
//$("html").css("--bheight", + rightheight + "px");
//console.log("sadsad  :  " + rightheight);
//$(window).resize(function () {
//    rightheight = $(document).height();
//  //  rightheight += 71;
//    $("html").css("--bheight", + rightheight + "px");
//    console.log("resizeheight  :  " + rightheight);

//});

function abc() {
    //var i = 0;
    //setInterval(function () {

    //    i++;
    //    console.log("Set Interval : " + i);

    //}, 3000)

    //console.log("after Line ");
}







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

    });

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


//$(".formfour").hide();
var test = "valueeeeeeeeee";
//var scope = angular.element(document.getElementById('RP')).scope();
//var scope = 0;

app.directive("w3TestDirective", function () {
    return {
        restrict: "E",
        template: "<h1>Made by a directive!</h1>"
    };
});

app.controller("ResumeProcessController", function ($scope, $http, $sce, $compile) {
   
    $scope.pageno = 1;

    $scope.Html = ``;
    $scope.varr = "12345678";

   

      $scope.changepage = function () {
          abc();

      
          $scope.pageno += 1;
          console.log("  function running page no " + $scope.pageno);
          switch ($scope.pageno) {
              case 2:

                  $(".formone").hide();
                  //$(".formtwo").addClass("heightt");

                  $scope.objj = {
                      expid: $(".expsR:checked").val(),
                      eduid: $(".eduR:checked").val()
                  }

                  //http request by ajax but in angular js way
                  $http({
                      url: '/Select-Resume',
                      method: 'POST',
                      data: $scope.objj
                  }).then(function (response) {
                      console.log("Response : " + response);
                      $(".formtwo").html(response.data);
                      //$scope.Html = "abcd";
                      //angular.element(".formtwo").html(response.data);  //Not Working

                  });

                  break;


              case 3:
                  $(".allformcon").addClass("addon");
                  $(".formtwo").hide();



                  var objj = {
                      expid: $(".expsR:checked").val(),
                      Maxeduid: $(".eduR:checked").val(),
                      resumeid: $("#Ridtextbox").val()
                  }




                  //$http({
                  //    url: "/ResumeProcess/ThirdPage",
                  //    method: 'POST',
                  //    data: objj
                  //}).then(function (response) {
                      
                  //    //$(".formthree").html(response.data);
                  //    $scope.Html = $sce.trustAsHtml(response.data);
                  //    $(".internqns").hide();
                  //    $(".projectqns").hide();
                  //    //$compile($(".formthree").contents())($scope);
                  //    //var el = angular.element('.formthree');
                  //    var el  =angular.element(document.querySelector('#f3'));
                  //    $compile(el)($scope);
                  //    //$scope.varr = "111111111";
                  //    //console.log("reached");


                  //});


                  //$(".formfour").show();

                  $.ajax({

                      url: "/ResumeProcess/ThirdPage",
                      type: "POST",
                      data: objj,
                      success: function (response) {

                          // var scope = angular.element(document.getElementById("f3")).html(response);  /*  $(".formthree").html(response)*/
                          //$scope.courseListHtml = $sce.html(response);
                          $(".formthree").html(response);
                          //$scope.courseListHtml = $sce.html(response);
                          
                          $(".internqns").hide(); 
                          $(".projectqns").hide();
                         

                      }, error:
                          function (response) {
                              alert("Error 3 : " + response);
                          }

                  }
                      )



                  break;


              default:
                  break;









          }

         
          


    }


    //$scope.Icount = 0;
    //$scope.pageno = 1;
    //$scope.ifunc = function () {
    //    console.log("pressss worksss");
    //    console.log("value of icount : " + $scope.Icount);
    //}


        $scope.func = function () {
        $scope.username = "SEARCH";

            //$http({
        //    url: '/Home/angularGet',
        //    method: 'GET'
        //}).then(function (response) {


        //    $scope.names = JSON.parse(response.data);


        //    $scope.names.push('fourth');


        //});
    }

    });

