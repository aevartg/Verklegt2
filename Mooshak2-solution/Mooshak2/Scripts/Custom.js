$(document).ready(function () {

    $(".searchclass option").each(function () {
        $(this).attr("data-search-term", $(this).text().toLowerCase());
    });

    $(".leitarbox").on("keyup", function () {

        var searchTerm = $(this).val().toLowerCase();

        $(".searchclass option").each(function () {

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

$("#addstudent")
	.click(function () {
	    $("#notselectedstudent option:selected").appendTo("#selectedstudent").removeAttr("selected");
	});

$("#createform").submit(function () {
    $("#notselectedstudent option:selected").removeAttr("selected");
    var select = $("#selectedstudent");
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

/*
 * https://stackoverflow.com/questions/14972470/c-sharp-mvc3-ajax-beginform-to-upload-file-not-working
 * Used answer 2 unfortunatelly this does mean that IE is not supported.
 */


window.addEventListener("submit", function (e) {
	var form = e.target;
	if (form.getAttribute("enctype") === "multipart/form-data") {
		if (form.dataset.ajax) {
			e.preventDefault();
			e.stopImmediatePropagation();
			var xhr = new XMLHttpRequest();
			xhr.open(form.method, form.action);
			xhr.onreadystatechange = function () {
				if (xhr.readyState == 4 && xhr.status == 200) {
					if (form.dataset.ajaxUpdate) {
						var updateTarget = document.querySelector(form.dataset.ajaxUpdate);
						if (updateTarget) {
							updateTarget.innerHTML = xhr.responseText;
						}
					}
				}
			};
			xhr.send(new FormData(form));
		}
	}
}, true);






/*sýnir mismunandi töflur fyrir users, teachers og students og afhakar þann sem er valið ef annar listi er valinn*/
$(document).ready(function () {
	$(".userlist1").show();
	$("input[name$='users']").click(function () {
		var selected = $('option:selected');
		selected.removeAttr("selected");
		$(".hider").hide();
		var radio = $(this).val();
		$(".userlist" + radio).show();
	})
})