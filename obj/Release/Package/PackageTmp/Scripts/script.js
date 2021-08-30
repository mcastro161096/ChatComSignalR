$(function () {
    var connection = $.connection("/echo");
    connection.start().done(function () {

        $("#chat").val("Conected\n");

        $("#sendButton").click(function () {

            connection.send($("#txtBox").val());

            $("#txtBox").val("");
        });

    });

    connection.received(function (data) {

        $("#chat").val($("#chat").val() + data + "\n");
    });

});