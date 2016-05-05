$(document).ready(function () {

    $(".leitarlisti option").each(function () {
        $(this).attr("data-search-term", $(this).text().toLowerCase());
    });

    $(".leitarbox").on("keyup", function () {

        var searchTerm = $(this).val().toLowerCase();

        $(".leitarlisti option").each(function () {

            if ($(this).filter("[data-search-term *= " + searchTerm + "]").length > 0 || searchTerm.length < 1) {
                $(this).show();
            } else {
                $(this).hide();
            }

        });

    });

});