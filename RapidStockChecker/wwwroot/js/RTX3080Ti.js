"use strict"

var clock = new Audio('https://rapidstockchecker.s3.us-west-2.amazonaws.com/alarm-clock.mp3');
clock.volume = 0.10;
var rooster = new Audio('https://rapidstockchecker.s3.us-west-2.amazonaws.com/rooster.mp3');
rooster.volume = 0.70;
var nuke = new Audio('https://rapidstockchecker.s3.us-west-2.amazonaws.com/nuke.mp3');
nuke.volume = 0.15;
var challenge = new Audio('https://rapidstockchecker.s3.us-west-2.amazonaws.com/challenge.mp3');
challenge.volume = 0.15;
var ding = new Audio('https://rapidstockchecker.s3.us-west-2.amazonaws.com/ding.mp3');
ding.volume = 0.60;

const alarm = 'alarmRTX3080Ti';
const alarmType = 'alarmTypeRTX3080Ti';

$(document).ready(function () {

    $("#notification").hide();

    if ($.cookie(alarm) == undefined) {
        $.cookie(alarm, 'false', { expires: 1 });
    }

    var audioState = $.cookie(alarm);
    if (audioState == "true") {
        $('#alarmToggle').prop('checked', true);
    }
    else {
        $('#alarmToggle').prop('checked', false);
    }


    if ($.cookie(alarmType) == undefined) {
        $.cookie(alarmType, 'Default', { expires: 1 });
    }
    var audioSelected = $.cookie(alarmType);
    $("#alarmType").val(audioSelected);
});

const signalrConnection = new signalR.HubConnectionBuilder()
    .withUrl("/messagebroker")
    .build();

signalrConnection.start().then(res => {
    signalrConnection.invoke("JoinGroup", "NVIDIAGeForceRTX3080Ti")
        .catch(err => {
            console.log(err);
        });
}).catch(err => {
    console.log(err);
});

$('#alarmToggle').change(function () {
    var audioState = document.getElementById("alarmToggle").checked;
    $.cookie(alarm, audioState, { expires: 1 });
});

$('#alarmType').change(function () {
    var audioSelected = $('#alarmType').find(":selected").text();
    $.cookie(alarmType, audioSelected, { expires: 1 });
});

$('#closeNotification').click(function () {
    $("#notification").hide();
});

$("#testSound").click(function () {
    var audioState = document.getElementById('alarmToggle').checked;

    if ($.cookie(alarmType) == undefined) {
        var audioSelected = $('#alarmType').find(":selected").text();
        $.cookie(alarmType, audioSelected, { expires: 1 });
    }

    if (audioState == true) {

        switch ($.cookie(alarmType)) {
            case 'Default':
                clock.loop = false;
                clock.play();
                break;
            case 'Rooster':
                rooster.loop = false;
                rooster.play();
                break;
            case 'Nuke':
                nuke.loop = false;
                nuke.play();
                break;
            case 'Challenge':
                challenge.loop = false;
                challenge.play();
                break;
            case 'Ding':
                ding.loop = false;
                ding.play();
                break;
        }
    }
});

signalrConnection.on("NVIDIAGeForceRTX3080TiAlertTrigger", function (products) {
    const stockHeader = document.getElementById("stockHeader");
    stockHeader.innerText = "Stock Found on " + products.retailer;
    const stockBody = document.getElementById("stockBody");
    stockBody.innerText = products.title;
    $("#stockBody").attr("href", products.url)
    $("#notification").show();
    $('.toast').toast({ autohide: false });
    $('.toast').toast('show');

    var audioState = document.getElementById('alarmToggle').checked;

    if ($.cookie(alarmType) == undefined) {
        var audioSelected = $('#alarmType').find(":selected").text();
        $.cookie(alarmType, audioSelected, { expires: 1 });
    }

    if (audioState == true) {

        switch ($.cookie(alarmType)) {
            case 'Default':
                clock.loop = false;
                clock.play();
                break;
            case 'Rooster':
                rooster.loop = false;
                rooster.play();
                break;
            case 'Nuke':
                nuke.loop = false;
                nuke.play();
                break;
            case 'Challenge':
                challenge.loop = false;
                challenge.play();
                break;
            case 'Ding':
                ding.loop = false;
                ding.play();
                break;
        }
    }
});