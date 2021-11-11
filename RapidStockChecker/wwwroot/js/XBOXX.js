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

const alarmType = 'alarmTypeXBOXX';
const group = 'XBOXSeriesX';

$(document).ready(function () {

    $("#notification").hide();

    if ($.cookie(alarmType) == undefined) {
        $.cookie(alarmType, 'Default', { expires: 1 });
    }
    var audioSelected = $.cookie(alarmType);
    $("#alarmType").val(audioSelected);
});

const signalrConnection = new signalR.HubConnectionBuilder()
    .withUrl("/messagebroker")
    .withAutomaticReconnect([0, 2000, 10000, 30000, 60000, 120000])
    .build();

signalrConnection.start().then(res => {
    signalrConnection.invoke("JoinGroup", group)
        .catch(err => {
            console.log(err);
        });
}).catch(err => {
    console.log(err);
});

signalrConnection.onreconnecting((error) => {
    console.log("DISCONNECTED FROM SERVER. Attempting to reconnect");
});

signalrConnection.onreconnected((connectionId) => {
    console.log("Connection Reestablished");
    signalrConnection.invoke("JoinGroup", group)
        .catch(err => {
        console.log(err);
    });
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

signalrConnection.on("XBOXSeriesXAlertTrigger", function (products) {
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