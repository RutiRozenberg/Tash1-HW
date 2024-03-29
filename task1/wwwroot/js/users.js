const uri = '/user'
let users = []


getItems(uri,'Users')


const addNameTextbox = document.getElementById('add-name');
const addPasswordTextbox = document.getElementById('add-password');

 
function addUser(){
    const addIsAdmin = document.getElementById('add-isAdmin').checked;
    document.getElementById('add-isAdmin').checked=false;
    
    const item = {
        isAdmin: addIsAdmin ,
        name: addNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim(),
    };
    console.log("item:  " ,item);
    addItem(uri,'Users',item)
}

function updateUser(){
    const itemId = document.getElementById('edit-id-user').value;
    const item = {
        id: parseInt(itemId, 10),
        isAdmin: document.getElementById('edit-isAdmin').checked,
        name: document.getElementById('edit-name-user').value.trim(),
        password: document.getElementById('edit-password').value.trim(),
    };
    updateItem(uri, 'Users', itemId,item)
}


function displayEditFormUser(id ) {
    const item = users.find(item => item.id === id);
    writeDetailsinInputs(item, "editForm")
}




function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data,id) {
    console.log(data);
    const tBody = document.getElementById(id);
    tBody.innerHTML = '';

    console.log("data ", data)
    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        console.log(item);
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isAdmin;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditFormUser(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem("/User",${item.id},'Users')`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textNodePassword = document.createTextNode(item.password);
        td3.appendChild(textNodePassword);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    users = data;
}
