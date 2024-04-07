
const uri = '/Task';
let tasks = [];
const addNameTextbox = document.getElementById('add-name');

linkToUsers()

function addTask(){
    const item = {
        isDone: false,
        name: addNameTextbox.value.trim()
    };
    addItem(uri,'Tasks',item)
}


function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';
    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}


function updateTask(){
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isDone: document.getElementById('edit-isDone').checked,
        name: document.getElementById('edit-name').value.trim()
    };
    updateItem(uri, 'Tasks', itemId,item)

}


function displayEditFormTask(id) {
    
    const item = tasks.find(item => item.id === id);
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isDone').checked = item.isDone;
    document.getElementById('editForm').style.display = 'block';

}


function _displayItems(data,id) {
    const tBody = document.getElementById(id);
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isDone;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditFormTask(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem("/Task",${item.id},'Tasks')`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);

        tasks=data;
    });
}


function linkToUsers(){
    const linkToUser = document.getElementById('link-to-users');
    const link = document.createElement('a');
    link.href = "./users.html";
    link.innerHTML="link to users"

    const myHeaders = headerWithtoken();

    const requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("/user", requestOptions)
        .then(response => response.json())
        .then(data => linkToUser.appendChild(link))
        .catch(error =>  console.log(error));
}


function showUpdateThisUser(){
    const myHeaders = headerWithtoken()

    const requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch( `user/Get`, requestOptions)
        .then(response => response.json())
        .then(data => writeDetailsinInputs(data,"editFormthisUser" ))
        .catch(error => {
            console.log('Unable to get items.', error)
        });
}


function updateThisUser(){ 
    const ifIsADmin = false;
    const myHeaders = headerWithtoken()

    const requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch( `user/Get`, requestOptions)
        .then(response => response.json())
        .then(data =>ifIsADmin = data.isAdmin )
        .catch(error => {
            console.log('Unable to get items.', error)
        });

    const item = {
        id: 0,
        isAdmin: ifIsADmin,
        name: document.getElementById('edit-name-user').value.trim(),
        password: document.getElementById('edit-password').value.trim(),
    };

    fetch(`user/PutThisUser`, {
        method: 'PUT',
        headers: myHeaders,
        redirect: 'follow',
        body: JSON.stringify(item),
       
    })
    .then(() => closeInput('editFormthisUser'))
    .catch(error => {
        location.href="index.html";
        console.error('Unable to update item.', error)
    });   
}