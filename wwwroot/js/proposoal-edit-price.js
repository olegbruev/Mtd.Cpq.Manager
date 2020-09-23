const PressQtyPrice = (id, master = false) => {    
    var form = document.getElementById('form-table');
    var qty = document.getElementById(`${id}-qty`);
    var price = document.getElementById(`${id}-price`);
    var sum = document.getElementById(`${id}-sum`);
    sum.innerHTML = '<i class="fas fa-sync-alt fa-spin fa-lg"></i>';

    const formData = CreateFormData(form);

    const xmlHttp = new XMLHttpRequest();

    setTimeout(
        xmlHttp.open("post", `?handler=UpdateItem&id=${id}&ismaster=${master}`, true), 1500
    )
    
    xmlHttp.send(formData);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            sum.innerText = `${this.response}`;
            if (qty.value === "" || qty.value === "0") {
                qty.value = 1;
            }

            if (price.value === "" || price.value === "0") {
                price.value = 1;
            }
        }
    }
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
