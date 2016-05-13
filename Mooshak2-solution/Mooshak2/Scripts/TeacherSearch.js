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
    var index = 0;
    $("#assignmentform")
		.on("click",
			".addbutton",
			function () {
				index++;
				var $template = $("#assignmenttempplate"),
					$clone = $template
						.clone()
						.removeClass("hide")
						.removeAttr("id")
						.attr("data-index", index)
						.insertBefore($template);

				$clone
					.find("[name=name]")
					.attr("name", "milestones[" + index + "].name")
					.end()
					.find("[name=weight]")
					.attr("name", "milestones[" + index + "].weight")
					.end()
					.find("[name=file]")
					.attr("name", "milestones[" + index + "].file")
					.end();
			});

    $("#assignmentform")
		.on("click",
			".removebtn",
			function () {
				var $row = $(this).parents(".form-group"),
					index = $row.attr("data-index");

				$row.remove();
			});
});