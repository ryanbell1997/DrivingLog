// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('.deleteButton').on('click', (e) => {
    $('#thisModal').modal('toggle');
    let myRow = e.target.closest('tr');
    const dateToDelete = myRow.children[0];
    const dateText = dateToDelete.innerText;
    let idToDelete = myRow.id.substring(6);

    PopulateModalForm(idToDelete, dateText);
})

$('.close').on('click', () => {
    $('#thisModal').modal('toggle');
});

$('.btn').on('click', () => {
    $('.modal').hide();
});

$('#deleteLogEntryButton').on('click', () => {
    DeleteEntry();
})

document.getElementById('downloadExcel').addEventListener('click', () => {
    exportTableToExcel('downloadExcel');
})

const PopulateModalForm = (id, date) => {

    document.getElementById('deleteModalText').textContent = `Are you sure you want to delete the entry on ${date}?`;

    let deleteForm = document.getElementById('deleteLogEntryForm');

    deleteForm.setAttribute("action", "/LogEntry/DeleteLogEntry/" + id);
}
