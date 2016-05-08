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

$(document)
	.on("click",
		".accordion-anchor",
		function(x)
		{
			var y = x.target.parentNode.getElementsByClassName("w3-accordion-content")[0];
			if (y.className.indexOf("w3-show") == -1)
			{
				y.className += " w3-show";
			}
			else
			{
				y.className = y.className.replace(" w3-show", "");
			}
		});


$("#add")
	.click(function()
	{
		$("#notselected option:selected").appendTo("#selected").removeAttr("selected");
	});

$("#createform").submit(function()
{
	$("#notselected option:selected").removeAttr("selected");
	var select = $("#selected");
	$("option", select).prop("selected", true);
})

$(document).ready(function()
{
	var index = 0;
	$("#assignmentform")
		.on("click",
			".addbutton",
			function()
			{
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
			function()
			{
				var $row = $(this).parents(".form-group"),
					index = $row.attr("data-index");

				$row.remove();
			});
});