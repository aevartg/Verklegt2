$(document).ready(function () {

    $(".table tbody tr").each(function () {
        $(this).attr("searchitem", $(this).find(".username").text().toLowerCase());
    });

    $(".leitarbox").on("keyup", function () {
        var searchTerm = $(this).val().toLowerCase();

        $(".table tbody tr").each(function () {
            if ($(this).filter("[searchitem *= " + searchTerm + "]").length > 0 || searchTerm.length < 1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
});

$("assignmentform").on("submit", function ()
{
    
    var elems = document.getElementsByClassName("weight");

    var sum = 0;

    for (var i = 0; i < elems.length; i++)
    {
        sum += elems[i].value;
    }

    if (sum == 100)
    {
        confirm("þetta virkaði");
        return false;
    }
});