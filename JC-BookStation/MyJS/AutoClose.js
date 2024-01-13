$(document).ready(function () {
    $('.alert-autocloseable-success').hide();
    $('.alert-autocloseable-warning').hide();
    $('.alert-autocloseable-danger').hide();
    $('.alert-autocloseable-info').hide();

    $('#autoclosable-btn-success').click(function () {
        $('#autoclosable-btn-success').prop("disabled", true);
        $('.alert-autocloseable-success').show();

        $('.alert-autocloseable-success').delay(5000).fadeOut("slow", function () {
            // Animation complete.
            $('#autoclosable-btn-success').prop("disabled", false);
        });
    });

    $('#normal-btn-success').click(function () {
        $('.alert-normal-success').show();
    });

    $('#autoclosable-btn-warning').click(function () {
        $('#autoclosable-btn-warning').prop("disabled", true);
        $('.alert-autocloseable-warning').show();

        $('.alert-autocloseable-warning').delay(3000).fadeOut("slow", function () {
            // Animation complete.
            $('#autoclosable-btn-warning').prop("disabled", false);
        });
    });

    $('#normal-btn-warning').click(function () {
        $('.alert-normal-warning').show();
    });

    $('#autoclosable-btn-danger').click(function () {
        $('#autoclosable-btn-danger').prop("disabled", true);
        $('.alert-autocloseable-danger').show();

        $('.alert-autocloseable-danger').delay(5000).fadeOut("slow", function () {
            // Animation complete.
            $('#autoclosable-btn-danger').prop("disabled", false);
        });
    });

    $('#normal-btn-danger').click(function () {
        $('.alert-normal-danger').show();
    });

    $('#autoclosable-btn-info').click(function () {
        $('#autoclosable-btn-info').prop("disabled", true);
        $('.alert-autocloseable-info').show();

        $('.alert-autocloseable-info').delay(6000).fadeOut("slow", function () {
            // Animation complete.
            $('#autoclosable-btn-info').prop("disabled", false);
        });
    });

    $('#normal-btn-info').click(function () {
        $('.alert-normal-info').show();
    });

    $(document).on('click', '.close', function () {
        $(this).parent().hide();
    });
});