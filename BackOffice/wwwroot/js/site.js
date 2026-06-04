// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Submit the bulk form with the chosen action
function submitBulk(action) {
    const checked = document.querySelectorAll('.bulk-checkbox:checked');
    if (checked.length === 0) {
        alert('Select at least one product.');
        return;
    }
    document.getElementById('bulkActionType').value = action;
    document.getElementById('bulkForm').submit();
}

// Individual remove — posts a tiny separate form dynamically
function submitRemove(id) {
    if (!confirm('Remove this product?')) return;
    const f = document.createElement('form');
    f.method = 'post';
    f.action = '/Producten/Remove';
    const input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'id';
    input.value = id;
    f.appendChild(input);
    document.body.appendChild(f);
    f.submit();
}