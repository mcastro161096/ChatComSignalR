$(function () {
    var connection = $.connection("/echo");
    connection.start().done(function () {

        $("#chat").val("Conected\n");

        $("#sendButton").click(function () {
            var objeto = {
                Type: 1,
                Text: $("#txtBox").val(),
                From: $("#user").val()
            };

            connection.send(objeto);

            $("#txtBox").val("");
        });

    });

    connection.received(function (data) {

        $("#chat").val($("#chat").val() + data + "\n");
    });

});