$(document).ready(function () {

    $("#leitarfunction option").each(function () {
        $(this).attr("data-search-term", $(this).text().toLowerCase());
    });

    $(".leitarbox").on("keyup", function () {

        var searchTerm = $(this).val().toLowerCase();

        $("#leitarfunction option").each(function () {

            if ($(this).filter("[data-search-term *= " + searchTerm + "]").length > 0 || searchTerm.length < 1) {
                $(this).show();
            } else {
                $(this).hide();
            }

        });

    });

});

$(document).on("click",".accordion-anchor", function (x)
{
	var y = x.target.parentNode.getElementsByClassName("w3-accordion-content")[0];
	if (y.className.indexOf("w3-show") == -1)
	{
		y.className += " w3-show";
	} else
	{
		y.className = y.className.replace(" w3-show", "");
	}
})