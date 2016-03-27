$(function () {
    var $btnPopulateList = $('.btnPopulateList'),
        $DayOffDate = $('#DayOffDate');

    var populateDropDowns = function () {
        var date = $('#DayOffDate').val(),
            option = {
                url: '/SGManagement/RetrieveGuardsToDropDown',
                method: 'Get',
                data : {scheduledDate : date}
            };

        $.ajax(option).success(function (data) {
            $('.guards').html(data);
        });

    };
    populateDropDowns();
    $btnPopulateList.on('click', populateDropDowns);
    $DayOffDate.on('blur', populateDropDowns);
});