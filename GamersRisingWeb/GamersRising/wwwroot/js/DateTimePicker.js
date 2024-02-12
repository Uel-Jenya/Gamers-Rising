$(function () {
    $(".datetimeselector").flatpickr({
        dateFormat: "d/m/Y H:i",
        enableTime: true,
        time_24hr: true,
        monthSelectorType: "static"
    });
})