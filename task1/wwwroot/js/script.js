
const token=localStorage.getItem("token")

showUserName()

function headerWithtoken()
{
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + token);
    myHeaders.append("Content-Type", "application/json");
    return myHeaders;
}


function getItems(url,type) {
    var myHeaders = headerWithtoken()
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };
    fetch( url, requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data,type))
        .catch(error => {
            location.href="index.html";
            console.log('Unable to get items.', error)
        });
}


function addItem(url,type,itemToAdd) {
  
    let myHeaders = headerWithtoken()

    fetch(url, {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(itemToAdd)
        })
        .then(response => response.json())
        .then(() => {
            getItems(url,type);
            addNameTextbox.value = '';
        })
        .catch( error => {
            location.href = "index.html";
            console.log('Unable to add item.', error)
        });
}


function deleteItem(url,id,type) {
    let myHeaders =headerWithtoken();
    fetch(`${url}/${id}`, {
            method: 'DELETE',
            headers:myHeaders,
            redirect: 'follow'
        })
        .then(() => getItems(url,type))
        .catch(error => console.log('Unable to delete item.', error));
}


function closeInput(formId) {
    document.getElementById(formId).style.display = 'none';
}


function updateItem(url,type,itemId , item) {
    
    myHeaders = headerWithtoken()
    fetch(`${url}/${itemId}`, {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(item),
           
        })
        .then(() => getItems(url,type))
        .catch(error => {
            location.href="index.html";
            console.error('Unable to update item.', error)
        });

    closeInput('editForm');

    return false;
}


function writeDetailsinInputs(item , formId){
    document.getElementById('edit-name-user').value = item.name;
    document.getElementById('edit-id-user').value = item.id;
    try{
        document.getElementById('edit-isAdmin').checked = item.isAdmin;
    }
    catch(error){

    }
    document.getElementById('edit-password').value=item.password
    document.getElementById(formId).style.display = 'block';
}


function showUserName(){
    const h1Name = document.getElementById('title-name');
    var myHeaders = headerWithtoken()
    
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    }; 

    fetch( `user/Get`, requestOptions)
        .then(response => response.json())
        .then(data =>h1Name.innerHTML+=' '+data.name)
        .catch(error => {
            console.log('Unable to get items.', error)
        });
} 


function signOut(){
    localStorage.setItem("token","")
    location.href="index.html" 
}