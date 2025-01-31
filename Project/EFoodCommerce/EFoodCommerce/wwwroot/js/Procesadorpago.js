let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Página",
            "zeroRecords": "Ningún Registro",
            "info": "Mostrar página _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros",
            "infoFiltered": "(filtrado de _MAX_ total registros)",
            "search": "Buscar por contenido",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/ProcesadorPago/ObtenerTodos"
        },
        "columns": [
            { "data": "codigo"},
            { "data": "nombreProcesador", "width": "15%" },
            { "data": "tipo", "width": "10%" },
            {
                "data": "estado",
                "width": "10%",
                "render": function (data) {
                    return data ? 'Activo' : 'Inactivo';
                }
            },
            {
                "data": "codigo",
                "render": function (data) {
                    return `
                    
                        <div class="text-center">
                            <a href="/Admin/ProcesadorPago/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/ProcesadorPago/Delete/${data}") class="btn btn-danger text-white"  style="cursor:pointer"> 
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                            
                        </div>
                    `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de eliminar?",
        text: "Este registro no se puede recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}