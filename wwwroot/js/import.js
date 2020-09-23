
function fireEvent(node, eventName) {

    var doc;
    if (node.ownerDocument) {
        doc = node.ownerDocument;
    } else if (node.nodeType == 9) {
        doc = node;
    } else {
        throw new Error("Invalid node passed to fireEvent: " + node.id);
    }

    if (node.dispatchEvent) {
         var eventClass = "";
        switch (eventName) {
            case "click": 
            case "mousedown":
            case "mouseup":
                eventClass = "MouseEvents";
                break;

            case "focus":
            case "change":
            case "blur":
            case "select":
                eventClass = "HTMLEvents";
                break;

            default:
                throw "fireEvent: Couldn't find an event class for event '" + eventName + "'.";
                break;
        }
        var event = doc.createEvent(eventClass);

        var bubbles = eventName == "change" ? false : true;
        event.initEvent(eventName, bubbles, true); 

        event.synthetic = true;     
        node.dispatchEvent(event, true);
    } else if (node.fireEvent) {

        var event = doc.createEventObject();
        event.synthetic = true; 
        node.fireEvent("on" + eventName, event);
    }
};


const GetStatusImport = (id) => {
    const form = document.getElementById("import-form-status");
    const inputListener = document.getElementById("import-listener");
    const textStaus = document.getElementById("import-status");
    var formData = CreateFormData(form);

    $.ajax({
        type: "POST",
        url: `/Goods/Import/Index?handler=Status&id=${id}`,
        data: formData,
        contentType: false,
        processData: false,       
        success: function (response) {
            if (response === "complete") {
                document.location.reload();
            }            
            textStaus.innerText = response;
            inputListener.value = response;            
            fireEvent(inputListener, "change");
        }
    });
}

const StopImport = () => {
    const form = document.getElementById("import-form-status");
    const inputListener = document.getElementById("import-listener");
    const textStaus = document.getElementById("import-status");
    var formData = CreateFormData(form);

    $.ajax({
        type: "POST",
        url: `/Goods/Import/Index?handler=Stop`,
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === "complete") {
                document.location.reload();
            }
            textStaus.innerText = response;
            inputListener.value = response;
            fireEvent(inputListener, "change");
        }
    });
}

$(document).ready(function () {

    const importListener = document.getElementById("import-listener");
    const importActive = document.getElementById("active-import-id");

   
    importListener.addEventListener("change", (e) => {
        setTimeout(() => GetStatusImport(importActive.value), 300);
    });

    if (importActive.value.length > 0) {
        const il = document.getElementById("import-listener");
        il.value = "LoadStatus...";
        fireEvent(il, "change");        
    }

    $('#btnUpload').on('click', function () {
        var fileExtension = ['xls', 'xlsx'];
        var filename = $('#fileUpload').val();
        //--- Validation for excel file---  
        if (filename.length == 0) {
            alert("Please select a file.");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only excel files.");
                return false;
            }
        }

        const form = document.getElementById("form");
        const importId = document.getElementById("form-import-id");
        const importActive = document.getElementById("active-import-id");
        const importListener = document.getElementById("import-listener");

        importId.value = newGuid();
        importActive.value = importId.value;
      
        var formData = CreateFormData(form)

        const modal = document.getElementById("import-modal");
        modal.style.display = "flex";
        importListener.value = "start";
        form.style.display = "none";

        $.ajax({
            type: "POST",
            url: "/Goods/Import/Index?handler=Import",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                document.location.reload();
            }
        });

        fireEvent(importListener, "change");
    })
});  