const newGuid = () => {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

var menus = new Array("menu-admin", "menu-superviser", "menu-goods", "menu-sales");

function closeMenu() {

    menus.forEach((menu) => {
        var temp = document.getElementById(menu);
        if (temp) {
            temp.classList.remove("show")
        }
    });
}

function openMenu(name) {
    this.event.stopPropagation();
    closeApps();
    var current = document.getElementById(name);
    var isOpen = current.classList.contains('show');
    menus.forEach((menu) => {
        var temp = document.getElementById(menu);
        if (temp) {
            temp.classList.remove("show")
        }
    });

    if (!isOpen) {
        current.classList.toggle("show");
    }
}

const CreateFormData = (form) => {

    var formData = new FormData();

    for (var i = 0; i < form.length; i++) {

        if (form[i].getAttribute("type") == 'checkbox') {
            formData.append(form[i].name, form[i].checked);
        } else {
            formData.append(form[i].name, form[i].value);
        }

        if (form[i].files && form[i].files.length > 0) {
            formData.append(form[i].name, form[i].files[0], form[i].files[0].name);
        }
    }

    return formData;
}

const Cover = (status, fade) => {
    if (status === true) {
        $("#cover").fadeIn(fade);
    } else {
        $("#cover").fadeOut(fade);
    }
}

function scrollFunction() {
    const mybutton = document.getElementById("btnUp");
    if (mybutton) {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            mybutton.style.display = "block";
        } else {
            mybutton.style.display = "none";
        }
    }

}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
function ClickApps(e) {
    e.stopPropagation();
    document.getElementById("app-solutions").classList.toggle("hidden");
    closeMenu();
    $('[data-toggle="tooltip"]').tooltip('hide');
}

function closeApps() {
    var d = document.getElementById("app-solutions");
    if (d) {
        if (!d.classList.contains("hidden")) { d.classList.toggle("hidden"); }
    }
}

(() => {
    window.onscroll = function () { scrollFunction() };

    document.addEventListener("keyup", (e) => {
        if (e.which === 27) {
            closeApps();
            closeMenu();
        }
    });

    document.addEventListener("click", (e) => {
        closeApps();
        closeMenu();
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    })
})();