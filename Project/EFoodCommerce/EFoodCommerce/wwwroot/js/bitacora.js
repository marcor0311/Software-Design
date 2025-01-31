var datatable;

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
            "infoFiltered": "(filtrado de _MAX_ registros)",
            "search": "Buscar por contenido",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/Bitacora/ObtenerTodos"
        },
        "columns": [
            { "data": "codigo" },
            { "data": "nombreUsuario" },
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
            { "data": "codigoRegistro" },
            { "data": "descripcion" }
        ]

    });

    $.fn.dataTable.ext.search.push(
        function( settings, data, dataIndex ) {
            var min = fechaInicioJS;
            var max = fechaFinJS;
            var createdAt = data[2];
            if (createdAt == null) return false;
            createdAt = new Date(createdAt);
            if (min == null && max == null) {
                return true;
            }
            if (min == null && createdAt <= max) {
                return true;
            }
            if(max == null && createdAt >= min) {
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