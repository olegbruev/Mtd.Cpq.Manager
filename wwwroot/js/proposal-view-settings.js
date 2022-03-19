class ViewData {
    constructor() {
        this.Form = document.getElementById("view-menu-form");
        this.NoteInput = document.getElementById("view-note-input");
        this.NoteIcon = document.getElementById("view-note-icon");
        this.GPriceInput = document.getElementById("view-gprice-input");
        this.GPriceIcon = document.getElementById("view-gprice-icon");
        this.CPriceInput = document.getElementById("view-cprice-input");
        this.CPriceIcon = document.getElementById("view-cprice-icon");
        this.NPriceInput = document.getElementById("view-nprice-input");
        this.NPriceIcon = document.getElementById("view-nprice-icon");
        this.DeliveryInput = document.getElementById("view-delivery-input");
        this.DeliveryIcon = document.getElementById("view-delivery-icon");
        this.QtyInput = document.getElementById("view-qty-input");
        this.QtyIcon = document.getElementById("view-qty-icon");
        this.BlockProposal = document.getElementById("view-proposal-params");
        this.DSIcon = document.getElementById("view-datasheet-icon");
        this.DSInput = document.getElementById("view-datasheet-input");
        this.PLIcon = document.getElementById("view-proposal-icon");
        this.PLInput = document.getElementById("view-proposal-input");

        this.ImagesInput = document.getElementById("view-images-input");
        this.ImagesIcon = document.getElementById("view-images-icon");

        this.AFactorInput = document.getElementById("view-afactor-input");
        this.AFactorIcon = document.getElementById("view-afactor-icon");

        this.IconSpinner = ["fas", "fa-sync-alt", "fa-spin"];
        this.IconCheckCircleOn = ["fas", "fa-check-circle"];
        this.IconCheckSquareOn = ["fas", "fa-check-square"];
        this.IconCheckCircleOff = ["fas", "fa-circle"];
        this.IconCheckSquareOff = ["fas", "fa-square"];
    }
}

const ViewNoteClick = () => {

    viewData = new ViewData();
    viewData.NoteIcon.className = "";
    viewData.NoteIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.NoteInput.value = viewData.NoteInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.NoteIcon.className = "";
            viewData.NoteIcon.classList.remove("fa-spin");
            if (viewData.NoteInput.value === '1') {
                viewData.NoteIcon.classList.add(...viewData.IconCheckSquareOn.map((c) => c));
            } else {
                viewData.NoteIcon.classList.add(...viewData.IconCheckSquareOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewGPriceClick = () => {

    viewData = new ViewData();
    if (viewData.GPriceInput.value === '1') return;
    
    viewData.GPriceIcon.className = "";
    viewData.GPriceIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.GPriceInput.value = '1';
    viewData.CPriceInput.value = '0';
    viewData.CPriceIcon.className = "";
    viewData.CPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    viewData.NPriceInput.value = '0';
    viewData.NPriceIcon.className = "";
    viewData.NPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.GPriceIcon.className = "";
            viewData.GPriceIcon.classList.remove("fa-spin");
            if (viewData.GPriceInput.value === '1') {
                viewData.GPriceIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
            } else {
                viewData.GPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewCPriceClick = () => {
    
    viewData = new ViewData();
    if (viewData.CPriceInput.value === '1') { return };

    viewData.CPriceIcon.className = "";
    viewData.CPriceIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.CPriceInput.value = '1';
    viewData.GPriceInput.value = '0';
    viewData.GPriceIcon.className = "";
    viewData.GPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    viewData.NPriceInput.value = '0';
    viewData.NPriceIcon.className = "";
    viewData.NPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.CPriceIcon.className = "";
            viewData.CPriceIcon.classList.remove("fa-spin");
            if (viewData.CPriceInput.value === '1') {
                viewData.CPriceIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
            } else {
                viewData.CPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewNPriceClick = () => {

    viewData = new ViewData();
    if (viewData.NPriceInput.value === '1') { return };

    viewData.NPriceIcon.className = "";
    viewData.NPriceIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.NPriceInput.value = '1';
    viewData.GPriceInput.value = '0';
    viewData.GPriceIcon.className = "";
    viewData.GPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    viewData.CPriceInput.value = '0';
    viewData.CPriceIcon.className = "";
    viewData.CPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.NPriceIcon.className = "";
            viewData.NPriceIcon.classList.remove("fa-spin");
            if (viewData.NPriceInput.value === '1') {
                viewData.NPriceIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
            } else {
                viewData.NPriceIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewDeliveryClick = () => {
    viewData = new ViewData();
    viewData.DeliveryIcon.className = "";
    viewData.DeliveryIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.DeliveryInput.value = viewData.DeliveryInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.DeliveryIcon.className = "";
            viewData.DeliveryIcon.classList.remove("fa-spin");
            if (viewData.DeliveryInput.value === '1') {
                viewData.DeliveryIcon.classList.add(...viewData.IconCheckSquareOn.map((c) => c));
            } else {
                viewData.DeliveryIcon.classList.add(...viewData.IconCheckSquareOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewQtyClick = () => {

    viewData = new ViewData();
    viewData.QtyIcon.className = "";
    viewData.QtyIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.QtyInput.value = viewData.QtyInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.QtyIcon.className = "";
            viewData.QtyIcon.classList.remove("fa-spin");
            if (viewData.QtyInput.value === '1') {
                viewData.QtyIcon.classList.add(...viewData.IconCheckSquareOn.map((c) => c));
            } else {
                viewData.QtyIcon.classList.add(...viewData.IconCheckSquareOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewImagesClick = () => {
    viewData = new ViewData();
    viewData.ImagesIcon.className = "";
    viewData.ImagesIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.ImagesInput.value = viewData.ImagesInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.ImagesIcon.className = "";
            viewData.ImagesIcon.classList.remove("fa-spin");
            if (viewData.ImagesInput.value === '1') {
                viewData.ImagesIcon.classList.add(...viewData.IconCheckSquareOn.map((c) => c));
            } else {
                viewData.ImagesIcon.classList.add(...viewData.IconCheckSquareOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}

const ViewAfactorClick = () => {
    viewData = new ViewData();
    viewData.AFactorIcon.className = "";
    viewData.AFactorIcon.classList.add(...viewData.IconSpinner.map((c) => c));

    viewData.AFactorInput.value = viewData.AFactorInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);

    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", "/proposal/details?handler=viewset", true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            viewData.AFactorIcon.className = "";
            viewData.AFactorIcon.classList.remove("fa-spin");
            if (viewData.AFactorInput.value === '1') {
                viewData.AFactorIcon.classList.add(...viewData.IconCheckSquareOn.map((c) => c));
            } else {
                viewData.AFactorIcon.classList.add(...viewData.IconCheckSquareOff.map((c) => c));
            }
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}


function settingOpen() {
    document.getElementById("view-menu").style.width = "200px";
    document.getElementById("view-content").style.marginLeft = "200px";
    document.getElementById("view-cover").style.display = "block";
}

function settingClose() {
    document.getElementById("view-menu").style.width = "0";
    document.getElementById("view-content").style.marginLeft = "0";
    document.getElementById("view-cover-spinner").style.display = "block";
    setTimeout(function () { location.reload() }, 500);
}

const ViewProposalClick = () => {
    viewData = new ViewData();
    viewData.BlockProposal.style.display = "block";
    ClearIcons(viewData);
    viewData.DSIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
    viewData.DSInput.value = '0';
    viewData.PLIcon.classList.add(...viewData.IconSpinner.map((c) => c));    
    viewData.PLInput.value = viewData.PLInput.value === '1' ? '0' : '1';
    const formData = CreateFormData(viewData.Form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", viewData.Form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            ClearIcons(viewData);
            viewData.PLIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);

}

const ViewDatasheetClick = () => {
    viewData = new ViewData();
    viewData.BlockProposal.style.display = "none";
    ClearIcons(viewData);
    viewData.PLIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
    viewData.PLInput.value = '0';
    viewData.DSIcon.classList.add(...viewData.IconSpinner.map((c) => c));
    viewData.DSInput.value = viewData.DSInput.value === '1' ? '0' : '1';

    const formData = CreateFormData(viewData.Form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", viewData.Form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            viewData = new ViewData();
            ClearIcons(viewData);
            viewData.DSIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
        }
    }

    setTimeout(function () { xmlHttp.send(formData) }, 150);
}


const ClearIcons = (viewData) => {

    viewData.DSIcon.className = "";
    viewData.DSIcon.classList.remove("fa-spin");
    viewData.PLIcon.className = "";
    viewData.PLIcon.classList.remove("fa-spin");
}

(() => {
    const viewData = new ViewData();
    ClearIcons(viewData);
    if (viewData.PLInput.value === '1') {
        viewData.PLIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
    } else {
        viewData.PLIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
    }

    if (viewData.DSInput.value === '1') {
        viewData.BlockProposal.style.display = "none";
        viewData.DSIcon.classList.add(...viewData.IconCheckCircleOn.map((c) => c));
    } else {
        viewData.DSIcon.classList.add(...viewData.IconCheckCircleOff.map((c) => c));
    }

})();