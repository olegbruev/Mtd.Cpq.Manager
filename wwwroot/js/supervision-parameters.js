
logoWidth.disabled = !logoFlexible.checked;
logoHeight.disabled = !logoFlexible.checked;

logoFlexible.addEventListener("change", (e) => {
    logoWidth.disabled = !e.target.checked;
    logoHeight.disabled = !e.target.checked;

    if (!e.target.checked) {
        logoWidth.value = 250;
        logoHeight.value = 100;
    }

});
