//Google
google.accounts.id.initialize({
    client_id: '837703262407-h2b1mubj69fu3hkaq0frtsmu6hfidl0i.apps.googleusercontent.com',
    callback: handleCredentialResponse,
    ux_mode: "popup",

});
google.accounts.id.prompt();

const createFakeGoogleWrapper = () => {
    const googleLoginWrapper = document.createElement("div");
    googleLoginWrapper.style.display = "none";
    googleLoginWrapper.classList.add("custom-google-button");

    // Add the wrapper to body
    document.body.appendChild(googleLoginWrapper);

    // Use GSI javascript api to render the button inside our wrapper
    // You can ignore the properties because this button will not appear
    google.accounts.id.renderButton(googleLoginWrapper, {
        theme: 'outline',
    });

    const googleLoginWrapperButton =
        googleLoginWrapper.querySelector("div[role=button]");

    return {
        click: () => {
            googleLoginWrapperButton.click();
        },
    };
}

const googleButtonWrapper = createFakeGoogleWrapper();
$("#login-width-google").on("click", function (e) {
    e.preventDefault();
    googleButtonWrapper.click()
})

function handleCredentialResponse(response) {
    // decodeJwtResponse() is a custom function defined by you
    // to decode the credential response.
    if (response == null || response == undefined) {
        Swal.fire(
            'Đăng nhập thất bại',
            'Vui lòng kiểm tra lại thông tin đăng nhập!',
            'error'
        );
    }
    const responsePayload = decodeJwtResponse(response.credential);

    var obj = {
        id: responsePayload.sub,
        fullName: responsePayload.name,
        photo: responsePayload.picture,
        email: responsePayload.email,
    }

    loginWithGoogle(obj);
}

function loginWithGoogle(obj) {
    $.ajax({
        url: systemConfig.defaultAPIURL + 'api/candidate/sign-in-google',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            //debugger;
            if (responseData.isSucceeded) {
                localStorage.setItem("currentUser", JSON.stringify(responseData.resources));
                localStorage.setItem("token", responseData.resources.token);
                Swal.fire({
                    title: 'Đăng nhập thành công',
                    html: `Chào mừng <b>${responseData.resources.fullname ? responseData.resources.fullname : ""}</b> quay trở lại.`,
                    icon: 'success',
                    focusConfirm: true,
                    allowEnterKey: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                }).then(() => {
                    window.location.href = '/';
                })
            }
            else if (!responseData.isSucceeded) {
                if (responseData.status == 400) {
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: responseData.title,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                } else if (responseData.status == 403 && responseData.title === "BadRequest") {
                    var listError = responseData.errors;
                    var html = '</ul>';
                    $(listError).each(function (index, item) {
                        html += '<li>' + item + '</li>'
                    })
                    html += '</ul>'
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: html,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                }
            }
        },
        error: function (e) {
            //console.log(e);
            Swal.fire(
                'Đăng nhập thất bại',
                'Vui lòng thử lại!',
                'error'
            );
        }
    })
}

//Facebook:
$("#login-width-facebook").on("click", function (e) {
    e.preventDefault();
    FB.login(function (response) {
        // handle the response
        if (response.status === 'connected') {
            // Logged into your webpage and Facebook.
            FB.api('/me', { locale: 'en_US', fields: 'id,first_name,middle_name,last_name,email,birthday,gender,picture,name' }, function (response) {
                //console.log(response);
                if (response && !response.error) {
                    /* handle the result */
                    var facebookAccount = {
                        "id": response.id,
                        "fullname": response.name,
                        "email": response.email || response.id,
                    };
                    //console.log(facebookAccount, );
                    loginWithFacebook(facebookAccount);
                } else {
                    Swal.fire(
                        'Đăng nhập thất bại',
                        'Vui lòng thử lại!',
                        'error'
                    );
                }


            });
        } else {
            // The person is not logged into your webpage or we are unable to tell.

        }

    }, { scope: 'public_profile,email' });
})

function loginWithFacebook(obj) {
    $.ajax({
        url: systemConfig.defaultAPIURL + 'api/candidate/sign-in-facebook',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            //debugger;
            if (responseData.isSucceeded) {
                localStorage.setItem("currentUser", JSON.stringify(responseData.resources));
                localStorage.setItem("token", responseData.resources.token);
                Swal.fire({
                    title: 'Đăng nhập thành công',
                    html: `Chào mừng <b>${responseData.resources.fullname ? responseData.resources.fullname : ""}</b> quay trở lại.`,
                    icon: 'success',
                    focusConfirm: true,
                    allowEnterKey: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                }).then(() => {
                    window.location.href = '/';
                })
            }
            else if (!responseData.isSucceeded) {
                if (responseData.status == 400) {
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: responseData.title,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                } else if (responseData.status == 403 && responseData.title === "BadRequest") {
                    var listError = responseData.errors;
                    var html = '</ul>';
                    $(listError).each(function (index, item) {
                        html += '<li>' + item + '</li>'
                    })
                    html += '</ul>'
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: html,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                }
            }
        },
        error: function (e) {
            //console.log(e);
            Swal.fire(
                'Đăng nhập thất bại',
                'Vui lòng kiểm tra lại thông tin đăng nhập!',
                'error'
            );
        }
    })
}

//LinkedIn
$("#login-width-linkedin").on("click", function (e) {
    e.preventDefault();

    // Replace with your own Client ID and Redirect URI
    const clientId = '861141my00jbs5';
    //const redirectUri = systemConfig.defaultURL + 'call-back';
    const redirectUri = 'https://localhost:7295/call-back';
    const responseType = 'code';
    const clientSecret = 'L4iHVS4h4Uu3fGxj';
    const scope = 'r_liteprofile r_emailaddress';

    // Construct the authorization URL
    const authUrl = `https://www.linkedin.com/oauth/v2/authorization?client_id=${clientId}&redirect_uri=${encodeURIComponent(redirectUri)}&response_type=${responseType}&scope=${scope}`;


    // Open a popup window for LinkedIn authentication
    const popup = window.open(authUrl, 'linkedin-popup', 'width=600,height=600');

    window.addEventListener('message', function (event) {
        /*if (event.origin === systemConfig.defaultURL) {*/
        if (event.origin === 'https://localhost:7295') {
            popup.close();
            const code = event.data;

            // Gửi yêu cầu để trao đổi mã xác thực lấy access token
            $.ajax({
                type: 'POST',
                url: 'https://www.linkedin.com/oauth/v2/accessToken',
                //contentType: "application/x-www-form-urlencoded",
                data: {
                    grant_type: 'authorization_code',
                    code: code,
                    redirect_uri: "https://jobi.dion.vn/",
                    client_id: clientId,
                    client_secret: clientSecret
                },
                success: function (data) {
                    console.log(data);
                    const accessToken = data.access_token;
                    const profileApiUrl = 'https://api.linkedin.com/v2/me';
                    const headers = {
                        Authorization: `Bearer ${accessToken}`,
                    };
                    $.ajax({
                        url: profileApiUrl,
                        headers: headers,
                        method: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            var obj = {
                                id: data.id,
                                fullName: data.firstName + data.lastName,
                                photo: data.profilePicture !== null ? data.profilePicture : '/uploads/candidates/avatars/avatar.jpg',
                                email: data.email,
                            };
                            console.log(obj)
                            loginWithLinkedIn(obj);
                        },
                        error: function (error) {
                            console.error('Error fetching LinkedIn profile:', error);
                        }
                    });
                },
                error: function (error) {
                    console.error('Error exchanging code for access token', error);
                }
            });
        }
    });
});

function loginWithLinkedIn(obj) {
    $.ajax({
        url: systemConfig.defaultAPIURL + 'api/candidate/sign-in-linkedIn',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj),
        success: function (responseData) {
            //debugger;
            if (responseData.isSucceeded) {
                localStorage.setItem("currentUser", JSON.stringify(responseData.resources));
                localStorage.setItem("token", responseData.resources.token);
                Swal.fire({
                    title: 'Đăng nhập thành công',
                    html: `Chào mừng <b>${responseData.resources.fullname ? responseData.resources.fullname : ""}</b> quay trở lại.`,
                    icon: 'success',
                    focusConfirm: true,
                    allowEnterKey: true,
                    showCancelButton: false,
                    confirmButtonText: 'OK'
                }).then(() => {
                    window.location.href = '/';
                })
            }
            else if (!responseData.isSucceeded) {
                if (responseData.status == 400) {
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: responseData.title,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                } else if (responseData.status == 403 && responseData.title === "BadRequest") {
                    var listError = responseData.errors;
                    var html = '</ul>';
                    $(listError).each(function (index, item) {
                        html += '<li>' + item + '</li>'
                    })
                    html += '</ul>'
                    Swal.fire({
                        title: 'Đăng nhập thất bại',
                        html: html,
                        icon: 'warning',
                        focusConfirm: true,
                        allowEnterKey: true,
                        showCancelButton: false,
                        confirmButtonText: 'OK'
                    })
                }
            }
        },
        error: function (e) {
            //console.log(e);
            Swal.fire(
                'Đăng nhập thất bại',
                'Vui lòng kiểm tra lại thông tin đăng nhập!',
                'error'
            );
        }
    })
}

function decodeJwtResponse(token) {
    var base64Url = token.split(".")[1];
    var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    var jsonPayload = decodeURIComponent(
        atob(base64)
            .split("")
            .map(function (c) {
                return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
            })
            .join("")
    );

    return JSON.parse(jsonPayload);
}
