
const url = '/login';

ExistValidToken()


function getUserDetails(){
    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();
    Login(name,password)
}


async function Login(name,password) {
    localStorage.clear();
    const header = new Headers();

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
                const token = result;
                localStorage.setItem("token",token)
                location.href = "Tasks.html";
            }
        })
        .catch((error) => console.log("error", error));
}


function ExistValidToken(){

    const myHeaders = headerWithtoken()
    const requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch( '/Task', requestOptions)
        .then(response=>response.json())
        .then(data=> {
            if(data){
                location.href="Tasks.html"
            }
        })
        .catch(error => {
            console.log('Unable to get items. token not available')
        });
}



function handleCredentialResponse(response) {
    if (response.credential) {
        const idToken = response.credential;
        const decodedToken = parseJwt(idToken);
        // var userId = decodedToken.sub;
        const userPassword =decodedToken.email;
        const userName = decodedToken.name;
        console.log(userPassword , "  ", userName);
        Login(userName, userPassword);
    } else {
        console.log('Google Sign-In was cancelled.');
    }
}

function parseJwt(token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}