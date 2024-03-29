
const url = '/login';

function Login() {
    localStorage.clear();
    var header = new Headers();
    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();

    header.append("Content-Type", "application/json");
    const forBody = JSON.stringify({
        Name: name,
        Password: password
    })
    const request= {
        method: "POST",
        headers: header,
        body: forBody,
        redirect: "follow",
    };

    fetch(url, request)
        .then((response) => response.text())
        .then((result) => {
            if (result.includes("401")) {
                name.value = "";
                password.value = "";
                alert("not exist!!")
            } else {
                token = result;
                localStorage.setItem("token",token)
                location.href = "Tasks.html";
            }
        }).catch((error) => alert("error", error));


        // document.getElementById('btn-google-connect').addEventListener('click', function() {
        //     window.location.href = 'https://localhost:5200/';
        // });
}