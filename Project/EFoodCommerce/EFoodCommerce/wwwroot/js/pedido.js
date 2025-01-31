let datatable;
let fechaInicioJS = null;
let fechaFinJS = null;
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
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
            "url": "/Admin/Pedido/ObtenerTodos"
        },
        "columns": [
            { "data": "codigo" },
            {
                "data": "fechaHora",
                "render": function (data) {
                    var fecha = new Date(data);
                    var fechaFormateada = fecha.getFullYear() + '-' +
                        ('0' + (fecha.getMonth() + 1)).slice(-2) + '-' +
                        ('0' + fecha.getDate()).slice(-2) + ' ' +
                        ('0' + fecha.getHours()).slice(-2) + ':' +
                        ('0' + fecha.getMinutes()).slice(-2) + ':' +
                        ('0' + fecha.getSeconds()).slice(-2);
                    return fechaFormateada;
                }
            },
            { "data": "monto" },
            { "data": "estado" },
            {
                "data": {
                    codigo: "codigo", estado: "estado"
                },
                "render": function (data) {
                    if (data.estado == "En curso") {
                        return `
                                    <div class="text-center">
                                        <a onclick="Delete('/Admin/Pedido/Delete/${data.codigo}')" class="btn btn-danger text-white"  style="cursor:pointer"> 
                                            <i class="bi bi-trash3-fill"></i>
                                        </a>
                                    </div>
                                `;
                    } else {
                        return '';
                    }
                }
            }
        ]

    });

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var min = fechaInicioJS;
            var max = fechaFinJS;
            var createdAt = data[1];
            if (createdAt == null) return false;
            createdAt = new Date(createdAt);
            if (min == null && max == null) {
                return true;
            }
            if (min == null && createdAt <= max) {
                return true;
            }
            if (max == null && createdAt >= min) {
                return true;
            }
            if (createdAt <= max && createdAt >= min) {
                return true;
            }

            return false;
        }
    );

    $('#btnBuscar').click(function () {
        var fechaInicio = $('#fechaInicio').val();
        var fechaFin = $('#fechaFin').val();
        fechaInicioJS = fechaInicio ? new Date(fechaInicio) : null;
        fechaFinJS = fechaFin ? new Date(fechaFin) : null;
        datatable.draw();
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