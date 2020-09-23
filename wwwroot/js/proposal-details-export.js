const ExportExcel = (idform) => {

    const form = document.getElementById(idform);
    form.submit();

}

const ExportWord = (idform) => {

    const form = document.getElementById(idform);
    const input = form.elements[0];
    const source = document.getElementById(input.getAttribute("mtd-source"));
    input.value = source.innerHTML;

    form.submit();

}