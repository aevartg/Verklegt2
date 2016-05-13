﻿$(document)
	.ready(function()
	{
		/**
		 * notað til að finna notendur á key up sem minnkar listann
		 */
		$(".searchclass option")
			.each(function()
			{
				$(this).attr("searchitem", $(this).text().toLowerCase());
			});
		$(".leitarbox")
			.on("keyup",
				function()
				{
					var searchTerm = $(this).val().toLowerCase();
					$(".searchclass option")
						.each(function()
						{
							if ($(this).filter("[searchitem *= " + searchTerm + "]").length > 0 || searchTerm.length < 1)
							{
								$(this).show();
							}
							else
							{
								$(this).hide();
							}
						});
				});

		/*sýnir mismunandi töflur fyrir users, teachers og students og afhakar þann sem er valið ef annar listi er valinn*/
		$(".userlist1").show();
		$("input[name$='users']")
			.click(function () {
				var selected = $("option:selected");
				selected.removeAttr("selected");
				$(".hider").hide();
				var radio = $(this).val();
				$(".userlist" + radio).show();
			});
		/**
		 * færa á milli í töflunum hjá Admin
		 */
		$("#add")
	.click(function () {
		$("#notselected option:selected").appendTo("#selected").removeAttr("selected");
	});

		$("#createform")
			.submit(function () {
				$("#notselected option:selected").removeAttr("selected");
				var select = $("#selected");
				$("option", select).prop("selected", true);
			});
		$("#addstudent")
			.click(function () {
				$("#notselectedstudent option:selected").appendTo("#selectedstudent").removeAttr("selected");
			});

		$("#RemoveTeacher")
			.click(function () {
				if ($("#selected option").count < 1) {
					$("#selected option:selected").appendTo("#notselected").removeAttr("selected");
				}
			});

		$("#RemoveStudent")
			.click(function () {
				$("#selectedstudent option:selected").appendTo("#notselectedstudent").removeAttr("selected");
			});
		$("#createform")
			.submit(function () {
				$("#notselectedstudent option:selected").removeAttr("selected");
				var select = $("#selectedstudent");
				$("option", select).prop("selected", true);
			});
	});

/**
 * /accordion í nav menu
 */
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


/*
 * https://stackoverflow.com/questions/14972470/c-sharp-mvc3-ajax-beginform-to-upload-file-not-working
 * Used answer 2 unfortunatelly this does mean that IE is not supported.
 */


window.addEventListener("submit",
	function(e)
	{
		var form = e.target;
		if (form.getAttribute("enctype") === "multipart/form-data")
		{
			if (form.dataset.ajax)
			{
				e.preventDefault();
				e.stopImmediatePropagation();
				var xhr = new XMLHttpRequest();
				xhr.open(form.method, form.action);
				xhr.onreadystatechange = function()
				{
					if (xhr.readyState == 4 && xhr.status == 200)
					{
						if (form.dataset.ajaxUpdate)
						{
							var updateTarget = document.querySelector(form.dataset.ajaxUpdate);
							if (updateTarget)
							{
								updateTarget.innerHTML = xhr.responseText;
							}
						}
					}
				};
				xhr.send(new FormData(form));
			}
		}
	},
	true);

/*Loader*/

$(document)
	.ajaxComplete(function()
	{
		$(".se-pre-con").fadeOut("slow");;
	});

$(".loader")
	.click(function()
	{
		$("div.injector").addClass("se-pre-con");
		$(".se-pre-con").fadeIn("slow");
	});


/*Fyrir alla takka sem er danger hefur fólk valmöguleika á að canclea*/
$(".btn-danger")
	.click(function()
	{
		var confirmed = confirm("Are you sure you want to continue?");

		if (confirmed == false)
		{
			return false;
		}
	});

/*Setur Icon við hliðiná Add assignment hjá teacher*/
$(".icon-hack").html('<i class="fa fa-plus-square" aria-hidden="true"></i> Add assignment');