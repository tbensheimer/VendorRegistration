var blazorInterop = {};

blazorInterop.DataTablesAdd = function (table) {
    $(function () {
        $(table).DataTable().destroy();
    })

    $(function () {
        $(table).DataTable();

    });
}

blazorInterop.DataTablesAddOrderByDisabledCompany = function (table) {
    $(function () {
        $(table).DataTable().destroy();
    })

    $(function () {
        $(table).DataTable({
            order: [[2, "asc"]]
        });

    });
}

blazorInterop.DataTablesAddOrderByDisabled = function (table) {
    $(function () {
        $(table).DataTable().destroy();
    })

    $(function () {
        $(table).DataTable({
            order: [[1, "asc"]]
        });

    });
}

blazorInterop.DataTablesAddTypesOrderByDisabled = function (table) {
    $(function () {
        $(table).DataTable().destroy();
    })

    $(function () {
        $(table).DataTable({
            order: [[2, "asc"]]
        });

    });
}

blazorInterop.DataTablesRemoveRow = function (rowId) {
    $(function () {
        $(rowId).remove();
    })
}


blazorInterop.DataTablesAddOrderbyDate = function (table) {

    $(function () {
        $(table).DataTable().destroy();
    })


    $(function () {
        $(table).DataTable({
            columnDefs: [{ type: 'date', 'targets': [4] }],
            "order": [[4, 'desc']]
        });
    });

}

blazorInterop.DataTablesRemove = function (table) {

    $(function () {
        $(table).DataTable().destroy();
    })
}

blazorInterop.FocusOnInput = function (inputId) {

    document.querySelector(inputId).focus();
}