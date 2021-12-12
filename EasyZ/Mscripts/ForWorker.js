var time = 0;

function timer() {
    time = time + 1;
    console.log("time : " + time);
    setTimeout("timer()", 100);
}
timer();  