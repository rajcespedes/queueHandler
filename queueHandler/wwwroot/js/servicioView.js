var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#DT_load2').DataTable(
        {
            "ajax": {
                "url": "/servicio/getall/",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "numero", "width": "20%" },
                { "data": "tipoServicio", "width": "20%" },
                { "data": "prioridad", "width": "20%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `<div clas="text-center">
                                    <form action="/servicio/estado" method="post">
                                        <input hidden name="id" value="${data}" />
                                        <input hidden name="estado" value="Trabajado" />
                                        <button class="btn btn-success" type="submit">
                                            Trabajado
                                        </button>
                                    </form>
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