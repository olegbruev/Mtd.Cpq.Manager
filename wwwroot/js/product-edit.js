
const input = document.getElementById("data-input");
const data = document.getElementById("data-editor");
data.innerHTML = input.value;

var editor = new Quill('#data-editor', {
    modules: {
        history: {
            'delay': 2000,
            'maxStack': 500,
            'userOnly': true
        },
        toolbar: [
            [{ header: [1, 2, 3, 4, false] }],
            ['bold', 'italic', 'underline'],
            [{ 'color': [] }, { 'background': [] }],
            [{ 'script': 'sub' }, { 'script': 'super' }],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            ['clean']
        ]
    },
    placeholder: 'Technical data information...',
    theme: 'snow'  // or 'bubble'
});

function SaveDatasheet() {
    const form = document.getElementById("form");
    if (editor.getLength() > 1) {
        input.value = document.querySelector(".ql-editor").innerHTML;
    } else {
        input.value = "";
    }
    form.submit();
}


function ClickArchive() {

    const checkbox = document.getElementById("archive-checkbox");
    const icon = document.getElementById("archive-icon");
    const div = document.getElementById("archive-div");

    icon.className = '';
    if (checkbox.checked) {
        div.style.color = "navy";
        icon.classList.add("fas", "fa-toggle-off");
    } else {
        div.style.color = "red";
        icon.classList.add("fas", "fa-toggle-on");
    }

    checkbox.checked = !checkbox.checked;
}