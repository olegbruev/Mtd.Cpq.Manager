
const OpenMessage = (id) => {

    document.getElementById('message-id').value = id;

    document.getElementById(`${id}-message`).style.display = "";
    document.getElementById(`${id}-icon-up`).style.display = "";
    document.getElementById(`${id}-icon-down`).style.display = "none";
    document.getElementById(`${id}-title-up`).style.display = "";
    document.getElementById(`${id}-title-down`).style.display = "none";

    const qtyDiv = document.getElementById('qty-message');
    const qtyString = qtyDiv.innerText;
    let qtyInt = parseInt(qtyString);
    qtyDiv.innerText = --qtyInt;
    if (qtyInt === 0) { qtyDiv.style.display = "none" }

    const form = document.getElementById('form-message');
    const formData = CreateFormData(form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.status != 200) {
            alert("Error!");

        }
    }

    xmlHttp.send(formData);
}