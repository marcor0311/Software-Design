let datatable;
const urlParams = new URLSearchParams(window.location.search);
const codigo_fuente = urlParams.get('codigoProducto');

$(document).ready(function () {
    
    loadDataTable();
});

function loadDataTable() {
    if (codigo_fuente == null) {
        real_url = "/Admin/PrecioProducto/ObtenerTodos";
    }
    else {
        real_url = "/Admin/PrecioProducto/ObtenerTodos?codigoProducto=" + codigo_fuente;
    }
    
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": real_url,
        },
        "columns": [
            { "data": "tipoPrecio.nombre" },
            { "data": "precio" },
            {
                "data": "codigo",
                "render": function (data) {
                    return `
                    
                        <div class="text-center">
                            <a href="/Admin/PrecioProducto/Upsert/${data}?codigoProducto=${codigo_fuente}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>

                            </a>
                            <a onclick=Delete("/Admin/PrecioProducto/Delete/${data}") class="btn btn-danger text-white"  style="cursor:pointer"> 
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