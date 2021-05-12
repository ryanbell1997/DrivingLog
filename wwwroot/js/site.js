// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#test').on('click', (e) => {
    document.getElementById('modalBodyText').innerHTML = "Are you sure you want to delete the entry on X?";
    $('.modal').show();
})

$('.close').on('click', () => {
    $('.modal').hide();
});

$('.btn').on('click', () => {
    $('.modal').hide();
});

document.getElementById('downloadExcel').addEventListener('click', () => {
    exportTableToExcel('downloadExcel');
})

$("#logEntryTable").table2excel({
    filename: "excel_sheet-name.xls"
});