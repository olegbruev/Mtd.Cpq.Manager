
const ClickRequired = (id) => {

    const form = document.getElementById(`${id}-required-form`);
    var viewData = new ViewData(id, form.getAttribute("id"));

    viewData.RequiredOn.style.display = "none";
    viewData.RequiredOff.style.display = "none";
    viewData.RequiredSpin.style.display = "";
        
    viewData.RequiredCheckbox.checked = !viewData.RequiredCheckbox.checked;   
    const formData = CreateFormData(form);
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {

            if (viewData.RequiredCheckbox.checked) {                                
                viewData.RequiredOn.style.display = ""; 
                viewData.RequiredSpin.style.display = "none";
            } else {
                viewData.RequiredOff.style.display = "";
                viewData.RequiredSpin.style.display = "none";
            }

            if (viewData.NoLinkCheckbox.checked && viewData.RequiredCheckbox.checked) {
                viewData.NoLinkCheckbox.checked = false;
                viewData.IncludeCheckbox = true;

                viewData.IncludeOn.style.display = "";
                viewData.IncludeOff.style.display = "none";

                viewData.NoLinkOn.style.display = "none";
                viewData.NoLinkOff.style.display = "";
            }

        }
    }

    setTimeout(() => { xmlHttp.send(formData) }, 150);
}

const ClickSaveNotice = () => {
    const form = document.getElementById("text-notice-form");
    var stringWriter = document.getElementById("text-notice-writer");
    var stringReader = document.getElementById("text-notice-reader");   

    const formData = CreateFormData(form);

    var iconSave = document.getElementById("icon-save");
    var iconSpinner = document.getElementById("icon-spinner");

    iconSave.style.display = "none";
    iconSpinner.style.display = "";
    
    const xmlHttp = new XMLHttpRequest();
    xmlHttp.open("post", form.action, true);
    xmlHttp.onreadystatechange = function () {

        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
            stringReader.value = stringWriter.value;
            iconSave.style.display = "";
            iconSpinner.style.display = "none";
            $('#dialogNotice').modal('hide');
        }
    }

    setTimeout(() => { xmlHttp.send(formData); }, 150);
}

class ViewData {
    constructor(id,action) {

        this.NoLinkForm = document.getElementById(`${id}-nolink-form`);
        this.NoLinkOn = document.getElementById(`${id}-nolink-on`);
        this.NoLinkOff = document.getElementById(`${id}-nolink-off`);
        this.NoLinkSpin = document.getElementById(`${id}-nolink-spin`);
        this.NoLinkCheckbox = document.getElementById(`${id}-nolink-checkbox`);

        this.IncludeForm = document.getElementById(`${id}-include-form`);
        this.IncludeOn = document.getElementById(`${id}-include-on`);
        this.IncludeOff = document.getElementById(`${id}-include-off`);
        this.IncludeSpin = document.getElementById(`${id}-include-spin`);
        this.IncludeCheckbox = document.getElementById(`${id}-include-checkbox`);

        this.ExcludeForm = document.getElementById(`${id}-exclude-form`);
        this.ExcludeOn = document.getElementById(`${id}-exclude-on`);
        this.ExcludeOff = document.getElementById(`${id}-exclude-off`);
        this.ExcludeSpin = document.getElementById(`${id}-exclude-spin`);
        this.ExcludeCheckbox = document.getElementById(`${id}-exclude-checkbox`);

        this.RequeredForm = document.getElementById(`${id}-required-form`);
        this.RequiredOn = document.getElementById(`${id}-required-on`);
        this.RequiredOff = document.getElementById(`${id}-required-off`);
        this.RequiredSpin = document.getElementById(`${id}-required-spin`);
        this.RequiredCheckbox = document.getElementById(`${id}-required-checkbox`);

        this.nolink = action.includes("nolink");
        this.include = action.includes("include");
        this.exclude = action.includes("exclude");     

        this.type = "";

        if (this.nolink) { this.type = "nolink" }
        if (this.include) { this.type = "include" }
        if (this.exclude) { this.type = "exclude" }

    }
}

(() => {
    const anchors = document.querySelectorAll("[anchor-click]");
    
    anchors.forEach((anchor) => {        
        anchor.addEventListener("click", () => {            
            const id = anchor.getAttribute("anchor-click");
            const action = anchor.getAttribute("id");
            const viewData = new ViewData(id, action);
            let form;            

            const checkbox = document.getElementById(`${id}-${viewData.type}-checkbox`);
            if (checkbox.checked) { return; }

            if (viewData.nolink) {

                form = viewData.NoLinkForm;
                viewData.IncludeOn.style.display = 'none';
                viewData.IncludeOff.style.display = '';
                viewData.IncludeSpin.style.display = 'none';
                viewData.IncludeCheckbox.checked = false;

                viewData.ExcludeOn.style.display = 'none';
                viewData.ExcludeOff.style.display = '';
                viewData.ExcludeSpin.style.display = 'none';
                viewData.ExcludeCheckbox.checked = false;

                viewData.RequiredOn.style.display = 'none';
                viewData.RequiredOff.style.display = '';
                viewData.RequiredSpin.style.display = 'none';
                viewData.RequiredCheckbox.checked = false;

                viewData.NoLinkOn.style.display = 'none';
                viewData.NoLinkOff.style.display = 'none';
                viewData.NoLinkSpin.style.display = '';
                viewData.NoLinkCheckbox.checked = true;
            }

            if (viewData.include) {

                form = viewData.IncludeForm;
                viewData.IncludeOn.style.display = 'none';
                viewData.IncludeOff.style.display = 'none';
                viewData.IncludeSpin.style.display = '';
                viewData.IncludeCheckbox.checked = true;

                viewData.ExcludeOn.style.display = 'none';
                viewData.ExcludeOff.style.display = '';
                viewData.ExcludeSpin.style.display = 'none';
                viewData.ExcludeCheckbox.checked = false;

                viewData.NoLinkOn.style.display = 'none';
                viewData.NoLinkOff.style.display = '';
                viewData.NoLinkSpin.style.display = 'none';
                viewData.NoLinkCheckbox.checked = false;
            }

            if (viewData.exclude) {
                form = viewData.ExcludeForm;
                viewData.IncludeOn.style.display = 'none';
                viewData.IncludeOff.style.display = '';
                viewData.IncludeSpin.style.display = 'none';
                viewData.IncludeCheckbox.checked = false;

                viewData.ExcludeOn.style.display = 'none';
                viewData.ExcludeOff.style.display = 'none';
                viewData.ExcludeSpin.style.display = '';
                viewData.ExcludeCheckbox.checked = true;

                viewData.NoLinkOn.style.display = 'none';
                viewData.NoLinkOff.style.display = '';
                viewData.NoLinkSpin.style.display = 'none';
                viewData.NoLinkCheckbox.checked = false;
            }


            const formData = CreateFormData(form);

            const xmlHttp = new XMLHttpRequest();
            xmlHttp.open("post", form.action, true);
            xmlHttp.onreadystatechange = function () {

                if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                    if (viewData.nolink) {                        
                        viewData.IncludeOn.style.display = 'none';
                        viewData.IncludeOff.style.display = '';
                        viewData.IncludeSpin.style.display = 'none';
                        viewData.IncludeCheckbox.checked = false;

                        viewData.ExcludeOn.style.display = 'none';
                        viewData.ExcludeOff.style.display = '';
                        viewData.ExcludeSpin.style.display = 'none';
                        viewData.ExcludeCheckbox.checked = false;

                        viewData.RequiredOn.style.display = 'none';
                        viewData.RequiredOff.style.display = '';
                        viewData.RequiredSpin.style.display = 'none';
                        viewData.RequiredCheckbox.checked = false;

                        viewData.NoLinkOn.style.display = '';
                        viewData.NoLinkOff.style.display = 'none';
                        viewData.NoLinkSpin.style.display = 'none';
                        viewData.NoLinkCheckbox.checked = true;
                    }

                    if (viewData.include) {

                        viewData.IncludeOn.style.display = '';
                        viewData.IncludeOff.style.display = 'none';
                        viewData.IncludeSpin.style.display = 'none';
                        viewData.IncludeCheckbox.checked = true;

                        viewData.ExcludeOn.style.display = 'none';
                        viewData.ExcludeOff.style.display = '';
                        viewData.ExcludeSpin.style.display = 'none';
                        viewData.ExcludeCheckbox.checked = false;

                        viewData.NoLinkOn.style.display = 'none';
                        viewData.NoLinkOff.style.display = '';
                        viewData.NoLinkSpin.style.display = 'none';
                        viewData.NoLinkCheckbox.checked = false;
                    }

                    if (viewData.exclude) {                       
                        viewData.IncludeOn.style.display = 'none';
                        viewData.IncludeOff.style.display = '';
                        viewData.IncludeSpin.style.display = 'none';
                        viewData.IncludeCheckbox.checked = false;

                        viewData.ExcludeOn.style.display = '';
                        viewData.ExcludeOff.style.display = 'none';
                        viewData.ExcludeSpin.style.display = 'none';
                        viewData.ExcludeCheckbox.checked = true;

                        viewData.NoLinkOn.style.display = 'none';
                        viewData.NoLinkOff.style.display = '';
                        viewData.NoLinkSpin.style.display = 'none';
                        viewData.NoLinkCheckbox.checked = false;
                    }
                }
            }

            setTimeout(() => { xmlHttp.send(formData); }, 150);
        });
    });

})();

