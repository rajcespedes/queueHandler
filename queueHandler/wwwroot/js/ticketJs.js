var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#DT_load').DataTable(
        {
            "ajax": {
                "url": "/ticket/getall/",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "numero", "width": "15%" },
                { "data": "tipoServicio", "width": "15%" },
                { "data": "prioridad", "width": "15%" },
                { "data": "estado", "width": "15%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div clas="text-center">
                                    <a class="btn btn-success text-white" href="/Ticket/Upsert?id=${data}">
                                        Edit
                                    </a>
                                    &nbsp;
                                    <a class="btn btn-danger text-white"
                                        onclick=Delete('/ticket/Delete?id='+${data}) >
                                        Delete
                                    </a>
                                </div>`;
                    }, "width": "40%"
                }
            ],
            "language": {
                "emptyTable": "No hay tickets disponibles."
            }, "width": "100%"
        });
}

function Delete(url) {
    swal({
        title: "¿Está seguro?",
        text: "Una vez borrado, no se podrá recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);

                    }

                }
            });
        }
    });

}