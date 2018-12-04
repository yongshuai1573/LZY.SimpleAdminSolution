(function ($) {

    $("form .field-validation-valid,form .field-validation-error")
    .each(function () {
        var tip = $(this);
        var fname = tip.attr("data-valmsg-for");
        var input = $("#" + fname);
        var vgName = "vg" + fname;
        $("<div class='vg' id='" + vgName + "'></div>").insertBefore(input);
        input.appendTo("#" + vgName);
        tip.appendTo("#" + vgName);

    });

})(jQuery);