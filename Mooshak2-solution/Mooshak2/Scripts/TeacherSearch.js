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
