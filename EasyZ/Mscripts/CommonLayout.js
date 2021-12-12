var app = angular.module("myMainApp", []);

//var rightheight = $(document).height();

////console.log("heighttt : " + rightheight);
//var nav = document.getElementsByClassName("custnav");


//var navheight = $(".custnav").height();
//console.log("n" + nav.height());


$(window).on('load', function () {
    $(".Loader").fadeOut("slow");
});
$(window).on('beforeunload', function () {
    $(".Loader").fadeIn(50);
});
   