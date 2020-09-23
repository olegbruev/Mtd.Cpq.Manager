const ClickStartOnMaster = () => {
    const checkbox = document.getElementById("start-master-checkbox");
    const form = document.getElementById("start-master-form");
    const icon = document.getElementById("start-master-icon");
    const div = document.getElementById("start-master-div");

    checkbox.checked = !checkbox.checked;

    icon.className = '';
    icon.classList.add("fas", "fa-sync-alt", "fa-lg", "fa-spin");

    const formData = CreateFormData(form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const iconD = document.getElementById("start-master-icon");
            iconD.classList.remove("fa-spin");
            iconD.className = '';
            if (checkbox.checked) {
                iconD.classList.add("fas", "fa-toggle-on", "fa-lg");
                div.style.color = "navy";
            } else {
                iconD.classList.add("fas", "fa-toggle-off", "fa-lg");
                div.style.color = "gray";
            }
        }
    }

    xmlHttp.send(formData);
}

const ClickAvailables = (id) => {

    const form = document.getElementById(`${id}-available-form`);
    const div = document.getElementById(`${id}-available-div`);
    const checkbox = document.getElementById(`${id}-available-checkbox`);
    const icon = document.getElementById(`${id}-available-icon`);

    checkbox.checked = !checkbox.checked;

    icon.className = '';
    icon.classList.add("fas", "fa-sync-alt", "fa-lg", "fa-spin");    

    const formData = CreateFormData(form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const icon = document.getElementById(`${id}-available-icon`);
            icon.classList.remove("fa-spin");
            icon.className = '';
            if (checkbox.checked) {
                icon.classList.add("far", "fa-check-circle", "fa-lg");
                div.style.color = "navy";
            } else {
                icon.classList.add("far", "fa-circle", "fa-lg");
                div.style.color = "gray";

                const iconR = document.getElementById(`${id}-required-icon`);
                const divR = document.getElementById(`${id}-required-div`);
                const checkboxR = document.getElementById(`${id}-required-checkbox`);
                const select = document.getElementById(`${id}-oneof-select`);
                select.value = '';
                checkboxR.checked = false;

                iconR.classList.add("fas", "fa-toggle-off", "fa-lg");
                divR.style.color = "gray";
            }
        }
    }

    setTimeout(()=>{xmlHttp.send(formData);}, 150);
}

const ClickRequired = (id) => {

    const form = document.getElementById(`${id}-required-form`);
    const div = document.getElementById(`${id}-required-div`);
    const checkbox = document.getElementById(`${id}-required-checkbox`);
    const icon = document.getElementById(`${id}-required-icon`);

    checkbox.checked = !checkbox.checked;

    icon.className = '';
    icon.classList.add("fas", "fa-sync-alt","fa-lg","fa-spin");    

    const formData = CreateFormData(form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            const icon = document.getElementById(`${id}-required-icon`);
            icon.classList.remove("fa-spin");
            icon.className = '';
            if (checkbox.checked) {

                icon.classList.add("fas", "fa-toggle-on","fa-lg");                
                div.style.color = "red";

                const iconA = document.getElementById(`${id}-available-icon`);
                const divA = document.getElementById(`${id}-available-div`);
                const checkboxA = document.getElementById(`${id}-available-checkbox`);
                checkboxA.checked = true;
                iconA.classList.add("far", "fa-check-circle", "fa-lg");
                divA.style.color = "navy";

            } else {
                icon.classList.add("fas", "fa-toggle-off", "fa-lg");
                div.style.color = "gray";
            }
        }
    }

    setTimeout(() => { xmlHttp.send(formData) }, 150);  
}

const SelectOneOf = (id) => {
    const iconWait = document.getElementById(`${id}-oneof-icon-wait`);
    const select = event.target;
    const form = select.parentNode;

    select.style.display = 'none';
    iconWait.style.display = '';

    const formData = CreateFormData(form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            select.style.display = '';
            iconWait.style.display = 'none';
            
            const iconA = document.getElementById(`${id}-available-icon`);
            const divA = document.getElementById(`${id}-available-div`);
            const checkboxA = document.getElementById(`${id}-available-checkbox`);
            checkboxA.checked = true;
            iconA.classList.add("far", "fa-check-circle", "fa-lg");
            divA.style.color = "navy";

        } 
    }

    setTimeout(() => { xmlHttp.send(formData); }, 150);
}

