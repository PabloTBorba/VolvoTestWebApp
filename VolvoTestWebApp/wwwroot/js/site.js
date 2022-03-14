// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var errorMessage = "";
    var messageFunc = function () { return errorMessage; };

    /**
     * Custom validation method 
     */
    jQuery.validator.addMethod("validateModel", function (value, element, params) {
        var model = $(params[0]).val();

        if (model !== 'FH' && model !== 'FM') {
            errorMessage = "Selected Model must be FH or FM";
            return false;
        }

        return true;
    }, messageFunc);

    /**
     * Using jQuery Validator to check the truck model
     */
    $('#UpdateTruck').validate({
        rules: {
            Model: {
                validateModel: ["#Model"]
            }
        },
        highlight: function (input) {
            $(input).parents('.form-control').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-control').removeClass('error');
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("model-error");
            error.insertAfter(element.parent());
        }
    });
});