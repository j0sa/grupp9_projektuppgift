var messageBody = document.getElementById("messageBody");

$(() =>
    $("#sendButton").click((e) => {
        e.preventDefault();
        var message = {};
        message.text = $('#messageBody').val();
        if (message.text.length >= 1 && message.text.length <= 255 && message.text?.trim()) {
            sendAndUpdateMessage();
            messageBody.style.borderColor = "black";
            messageBody.style.borderWidth = "3px";
        } else {
            messageBody.style.borderColor = "red";
            messageBody.style.borderWidth = "3px";
        }
    })
);

async function sendAndUpdateMessage() {
    await postMessage();
    setTimeout(updatePartial, 700);
};

function postMessage() {
    var message = {};
    message.text = $('#messageBody').val();
    $('#messageBody').val("");
    message.receiverId = $('#id').val();
    $.ajax({
        type: "POST",
        url: "/api/MessageAPI/postMessage",
        data: JSON.stringify(message),
        contentType: "application/json",
        dataType: "json"
    });
};

function updatePartial() {
    var message = {};
    message.receiverId = $('#id').val();
    $.ajax({
        url: "Wall/DisplayMessage",
        type: "GET",
        data: message
    }).done(function (partialViewResult) {
        $("#testDiv").html(partialViewResult);
    });
    window.scrollTo(0, document.body.scrollHeight);
};