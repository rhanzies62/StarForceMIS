$(function () {
    var $searchGuardForm = $('#searchGuardForm'),
        $guardList = $('.guardList');

    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            $guardList.html(data);
        });
        return false;
    };

    $searchGuardForm.submit(ajaxFormSubmit);
});