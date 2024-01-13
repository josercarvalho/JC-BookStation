$(function () {
    if ($(window).scrollTop() > $('#corpo').offset().top) {
        $('#nav').removeClass('container body-content');
        $('#nav').addClass('navbar-fixed-top');

    } else {
        $('#nav').removeClass('navbar-fixed-top');
        $('#nav').addClass('container body-content');
    };

    $(window).scroll(function() {
        if ($(this).scrollTop() > $('#corpo').offset().top) {
            $('#nav').removeClass('container body-content');
            $('#nav').addClass('navbar-fixed-top');
        } else {
            $('#nav').removeClass('navbar-fixed-top');
            $('#nav').addClass('container body-content');
        };
    });
})