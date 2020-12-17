function ConfirmDelete(uniqueId, isTrue) {
    var deleteSpan = 'DeleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'ConfirmDeleteSpan_' + uniqueId;

    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}