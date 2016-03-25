$(function () {
    var $searchGuardForm = $('#searchGuardForm'),
        $guardDetailsResult = $('.guardDetailsResult'),
        $datepicker = $('#datepicker'),
        $searchSchedule = $('#searchSchedule')
    ;

    var initializationScript = function () {
        $datepicker.datetimepicker();
    },
        displayGuardDetails = function (data) {
            $guardDetailsResult.html(data);
            var $scheduleDate = $('#scheduleDate');
            $scheduleDate.datetimepicker();
        },
        ajaxFormSubmit = function () {
            var $form = $(this);
            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };
            $.ajax(options).done(function (data) {
                displayGuardDetails(data);
                $('#scheduleGuardForm').submit(scheduleGuard);
            });
            return false;
        },
        retrieveSchedule = function () {
            var $form = $('#searchSchedule');
            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };
            $.ajax(options).done(function (data) {
                $('.schedules').html(data);
            });
            return false;
        },
        scheduleGuard = function () {
            var $form = $(this);
            var options = {
                url: $form.attr("action"),
                type: $form.attr("method"),
                data: $form.serialize()
            };
            $.ajax(options).done(function (data) {
                $('.result').html(data);
                retrieveSchedule();
            });
            return false;
        }
    ;

    $searchSchedule.submit(retrieveSchedule);
    $searchGuardForm.submit(ajaxFormSubmit);
    initializationScript();
    
    $guardDetailsResult.on('click', 'table tbody tr .btnAction span', function () {
        var id = $(this).data('id');
        var options = {
            url: '/SGmanagement/GetGuardDetails/' + id,
            type: 'get'
        };

        $.ajax(options).done(function (data) {
            displayGuardDetails(data);
            $('#scheduleGuardForm').submit(scheduleGuard);
        });
    });
});