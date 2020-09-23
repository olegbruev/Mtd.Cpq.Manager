/*
    document.addEventListener("DOMContentLoaded", () => {
        alert("DOM ready!");
    });
*/

function OnClickCheckBox(id) {

    const form = event.target.parentNode;
    var input = document.getElementById(`${id}-input`);
    const icon = document.getElementById(`${id}-icon`);

    input.checked = !input.checked;
    icon.className = '';
    icon.classList.add("fas", "fa-sync-alt", "fa-lg", "fa-spin");

    var container = document.getElementById("alert-message-container");
    container.innerHTML = "";

    const formData = CreateFormData(form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const jsonObj = JSON.parse(xmlHttp.response);
            UpdateAllIcons(jsonObj.items, jsonObj.configChangeRule);
            const iconD = document.getElementById(`${id}-icon`);
            iconD.classList.remove("fa-spin");

            if (input.checked && jsonObj.anchorNotice && jsonObj.anchorNotice.length > 1) {

                var message = document.getElementById("alert-message");
                var messageBody = message.innerHTML;
                var re = /tagInfo/gi;
                messageBody = messageBody.replace(re, jsonObj.anchorNotice);
                container.innerHTML = messageBody;
            }
        }
    }

    xmlHttp.send(formData);
}


function UpdateAllIcons(jsonObj, changeRule = 0) {
    const data = document.querySelectorAll("[mtd-checkbox]");
    data.forEach((input) => {

        const id = input.getAttribute("mtd-checkbox");
        for (var i = 0; i < jsonObj.length; i++) {
            if (jsonObj[i]['id'] == id) {

                var forbidden = (jsonObj[i]['forbidden'] === 1 && changeRule === 0) ? true : false;
                var required = ((jsonObj[i]['required'] === 1 || forbidden) && changeRule === 0) ? true : false;                
                var oneOf = (jsonObj[i]['oneOfId'] !== null && changeRule === 0) ? true : false;
                var button = document.getElementById(`${id}-button`);
                button.disabled = required;
                button.style.color = "black";
                if (required) { button.style.color = "brown"; }
                if (oneOf) { button.style.color = jsonObj[i]['color']; }

                var selected = jsonObj[i]['selected'] === 1 ? true : false;
                document.getElementById(`${id}-input`).checked = selected;

                const iconUP = document.getElementById(`${id}-icon`);
                iconUP.className = "";
                iconUP.classList.remove("fa-spin");

                if (selected) {
                    iconUP.classList.add('far', 'fa-check-circle');
                } else
                    if (forbidden) {
                        iconUP.classList.add('fas', 'fa-ban');
                    } else {
                        iconUP.classList.add('far', 'fa-circle');
                        info = document.querySelectorAll(`td[mtd-text='${id}]'`)
                        info.forEach((d) => {
                            d.style.color = "red";
                        })
                    }
            }
        }
    });
}

const Price = (id) => {
    window.location.href = `/Proposal/ItemsPrice?id=${id}`;
}

const ClickCatalog = (id) => {

    var icon = document.getElementById(`${id}-icon`);
    const isOpen = icon.classList.contains("fa-minus-square");
    const list = document.querySelectorAll(`[mtd-data-catalog="${id}"]`);
    if (isOpen) {
        icon.classList.toggle("fa-plus-square");
        list.forEach((item) => {
            item.style.display = "none";
        });
    } else {
        icon.classList.toggle("fa-minus-square");
        list.forEach((item) => {
            item.style.display = "";
        });
    }
}

const Delete = (id) => {
    window.location.href = `/Proposal/Delete?id=${id}`;
}

const ClickInclude = () => {
    const form = event.target.parentNode;
    const button = document.getElementById("button-include");

    const icon = button.querySelector("[data-icon]");
    icon.className = "";
    icon.classList.add("fas", "fa-spinner", "fa-spin");

    const formData = CreateFormData(form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const icon = button.querySelector("[data-icon]");
            const img = document.getElementById("include-img");
            icon.className = "";
            button.className = "";
            icon.classList.remove("fa-spin");
            if (xmlHttp.response === "1") {
                icon.classList.add("fas", "fa-toggle-on");
                button.classList.add("btn", "btn-outline-success");
                img.style.opacity = 1;
            } else {
                icon.classList.add("fas", "fa-toggle-off");
                button.classList.add("btn", "btn-outline-dark");
                img.style.opacity = 0.4;
            }
        }
    }

    xmlHttp.send(formData);
}


const ClickRule = () => {
    const form = event.target.parentNode;
    const button = document.getElementById("button-rule");
    const icon = button.querySelector("[data-icon]");

    icon.className = "";
    icon.classList.add("fas", "fa-spinner", "fa-spin");

    const formData = CreateFormData(form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const jsonObj = JSON.parse(xmlHttp.responseText);
            UpdateAllIcons(jsonObj.items, jsonObj.configChangeRule);

            const icon = button.querySelector("[data-icon]");
            icon.className = "";
            button.className = "";
            icon.classList.remove("fa-spin");
            if (jsonObj.configChangeRule === 0) {
                icon.classList.add("fas", "fa-toggle-on");
                button.classList.add("btn", "btn-outline-success");
            } else {
                icon.classList.add("fas", "fa-toggle-off");
                button.classList.add("btn", "btn-outline-dark");
            }
        }
    }

    xmlHttp.send(formData);
}
