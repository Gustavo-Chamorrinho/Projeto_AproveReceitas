var nameDigt = document.getElementById("name");
var emailDigt = document.getElementById("email");
var passwordDigt = document.getElementById("password");
var confirmPasswordDigt = document.getElementById("confirmPassword");


nameDigt.value = sessionStorage.getItem("name");
emailDigt.value = sessionStorage.getItem("email");
passwordDigt.value = sessionStorage.getItem("password");
confirmPasswordDigt.value = sessionStorage.getItem("confirmPassword");


document.getElementById("login_form").addEventListener("submit", function () {
    sessionStorage.setItem("name", nameDigt.value);
    sessionStorage.setItem("email", emailDigt.value);
    sessionStorage.setItem("password", passwordDigt.value);
    sessionStorage.setItem("confirmPassword", confirmPasswordDigt.value);
});