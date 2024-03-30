
const url = '/login';
ExistValidToken()

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
                const token = result;
                localStorage.setItem("token",token)
                location.href = "Tasks.html";
            }
        })
        .catch((error) => console.log("error", error));
}



function ExistValidToken(){
    const exist=true

    var myHeaders = headerWithtoken()
    var requestOptions = {
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

