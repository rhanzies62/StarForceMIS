$(function () {

    var $SearchScheduleForm = $('#SearchScheduleForm');

    var submitForm = function () {
        var $form = $(this);

        var options = {
            url: $form.attr('action'),
            type: $form.attr('method'),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            $('.guardScheduleView').html(data);
        });
        return false;
    }

    $SearchScheduleForm.submit(submitForm);

});