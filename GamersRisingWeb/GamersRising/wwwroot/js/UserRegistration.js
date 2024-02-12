$(function () {

    $("#RegistrationDrop input[name='UserName']").blur(function () {

        userName = $("#RegistrationDrop input[name='UserName']").val();

        url = "UserAuth/UsernameExists?name=" + email;

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data == true) {
                    var displayHtml = '<div class="alert alert-warning alert-dismissible">' +
                        '<button type="button" class="btn-close" data-bs-dismiss="alert"></button>' +
                        '<strong>Username In Use.</strong> This username is assosiated with an account. <a>Login here.</a>' +
                        '</div>'


                    //PresentClosableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid Email", "This email address has already been registered");

                }
                else {

                    var displayHtml = ""

                }

                $("#alert_placeholder_register").html(displayHtml);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

            }
        });

    });

    $("#RegistrationDrop input[name='Email']").blur(function () {

        email = $("#RegistrationDrop input[name='Email']").val();

        url = "UserAuth/UsernameExists?name=" + email;

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data == true) {
                    var displayHtml = '<div class="alert alert-warning alert-dismissible">' +
                        '<button type="button" class="btn-close" data-bs-dismiss="alert"></button>' +
                        '<strong>Email In Use.</strong> This email is assosiated with an account. <a>Login here.</a>' +
                    '</div>'

                   
                    //PresentClosableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid Email", "This email address has already been registered");

                }
                else {

                    var displayHtml = ""
                    
                }

                $("#alert_placeholder_register").html(displayHtml);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                //PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);

            }
        });

    });

        var registrationUserButton = $("#RegistrationDrop button[name='Register']").click(onRegisterClick);

        function onRegisterClick() {

            var url = "UserAuth/Register";

            var antiForgeryToken = $("#RegistrationDrop input[name='__RequestVerificationToken']").val();

            var userName = $("#RegistrationDrop input[name = 'UserName']").val();
            var fullName = $("#RegistrationDrop input[name = 'FullName']").val();

            var email = $("#RegistrationDrop input[name = 'Email']").val();
            var password = $("#RegistrationDrop input[name = 'Password']").val();
            var confirmPassword = $("#RegistrationDrop input[name = 'ConfirmPassword']").val();

            var birthDate = $("#RegistrationDrop input[name = 'BirthDate']").val();
            var phoneNum = $("#RegistrationDrop input[name = 'PhoneNumber']").val();

            var user = {
                __RequestVerificationToken: antiForgeryToken,
                UserName: userName,
                FullName: fullName,
                Email: email,
                password: password,
                ConfirmPassword: confirmPassword,
                BirthDate: birthDate,
                PhoneNumber: phoneNum
            };

            $.ajax({
                type: "POST",
                url: url,
                data: user,
                success: function (data) {

                    var parsed = $.parseHTML(data);

                    var hasErrors = $(parsed).find("input[name='RegistrationInValid']").val() == "true";

                    if (hasErrors == true) {
                        $("#RegistrationDrop").html(data)

                        userLoginButton = $("#RegistrationDrop button[name='Register']").click(onRegisterClick);

                        var form = $("#UserregistrationForm");

                        $(form).removeData("validator");
                        $(form).removeData("unobtrusiveValidation");
                        $.validator.unobtrusive.parse(form);

                    }
                    else {
                        location.href = 'Home/Index';

                    }
                },

                error: function (xhr, ajaxOptions, thrownError) {
                    //var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                    //PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);

                    console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
                }
            });
        }

    

});