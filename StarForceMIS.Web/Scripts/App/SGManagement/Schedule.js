$(function () {
    var $searchGuardForm = $('#searchGuardForm'),
        $guardDetailsResult = $('.guardDetailsResult')
    ;




    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };
        $.ajax(options).done(function (data) {
            $guardDetailsResult.html(data);
        });
        return false;
        },
        retrieveGuardDetails = function () {

        }

    $searchGuardForm.submit(ajaxFormSubmit);

    $guardDetailsResult.on('click', 'table tbody tr .btnAction span', function () {
        var id = $(this).data('id');
        var options = {
            url: '/SGmanagement/GetGuardDetails/' + id,
            type: 'get'
        };

        $.ajax(options).done(function (data) {
            $guardDetailsResult.html(data);
        });
    });
});