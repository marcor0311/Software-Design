let datatable;

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
            "url": "/Admin/VerProductos/ObtenerTodos"
        },
        "columns": [
            { "data": "producto.codigo"},
            { "data": "producto.nombre" },
            {
                "data": "precios",
                "render": function (data) {
                    return `
                        <select class="form-control" id="precio" name="precio">
                            <option disabled selected hidden>Ver precios</option>
                            ${data.map(price => `<optgroup label="${price}" disabled/>`).join('')}
                        </select>
                    `;
                }, "width": "40%"
            },
            {
                "data": "producto.codigoLineaComida",
                "visible" : false
            }
        ]
    });

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var select = document.getElementById("lineaComidaSelect");
            var selectedValue = select.options[select.selectedIndex].value;
            var hiddenValue = data[3];
            if (selectedValue == hiddenValue || selectedValue == "-1" || selectedValue == -1) {
                return true;
            }
            return false;
        }
    );

    $('#filtrarLineaComida').click(function () {
        datatable.draw();
    });
}