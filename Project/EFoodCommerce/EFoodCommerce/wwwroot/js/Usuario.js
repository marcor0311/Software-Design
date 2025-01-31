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
            "search": "Buscar por contenido",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/UsuarioAplicacion/ObtenerTodos"
        },
        "columns": [
            { "data": "nombre" },
            {
                "data": {
                    id: "id", role: "role"
                },
                "render": function (data) {
                    return_string = `<form class="fishingMagic row g-3" action="/Admin/UsuarioAplicacion/SetRole" method="POST"><input id="Id" type="hidden" name="Id" value=${data.id}></input><div class="col-sm">
                    <select id="Code" name="Code" class="form-select">`;
                    return_string += `<option `
                    if (data.role == null) return_string += `selected`;
                    return_string += ` value="0">Rol Vacío</option>`;
                    return_string += `<option `
                    if (data.role == "Administrador") return_string += `selected`;
                    return_string += ` value="1">Administrador</option>`;
                    return_string += `<option `
                    if (data.role == "Mantenimiento") return_string += `selected`;
                    return_string += ` value="2">Mantenimiento</option>`;
                    return_string += `<option `
                    if (data.role == "Seguridad") return_string += `selected`;
                    return_string += ` value="3">Seguridad</option>`;
                    return_string += `<option `
                    if (data.role == "Consultas") return_string += `selected`;
                    return_string += ` value="4">Consultas</option>`;
                    return_string += `<option `
                    if (data.role == "Tester") return_string += `selected`;
                    return_string += ` value="5">Tester</option>`;
                    return_string += `</select></div>`;
                    return_string += `
                    <div class="col-sm">
                    <button type="submit" class="btn btn-primary">Guardar</button>
                    </div>
                    </form>
                    `;
                    return return_string;
                }
            },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        // Usuario esta Bloqueado
                        return `
                            <div class="text-center">
                               <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-danger text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-unlock-fill"></i> Desbloquear
                               </a> 
                            </div>
                        `;
                    }
                    else {
                        return `
                            <div class="text-center">
                               <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-success text-white" style="cursor:pointer", width:150px >
                                    <i class="bi bi-lock-fill"></i> Bloquear
                               </a> 
                            </div>
                        `;
                    }

                }
            },
            {
                "data": {
                    id: "id"
                },
                "render": function (data) {
                    return `
                    
                        <div class="text-center">
                            <a onclick=Delete('${data.id}') class="btn btn-danger text-white"  style="cursor:pointer"> 
                                <i class="bi bi-trash3-fill"></i>
                            </a>
                            
                        </div>
                    `;
                }, "width": "30%"
            }
        ]

    });
}

function BloquearDesbloquear(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/UsuarioAplicacion/BloquearDesbloquear',
        data: JSON.stringify(id),
        contentType: "application/json",
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

document.addEventListener("submit", function(event) {
    if (event.target.classList.contains("fishingMagic")) {
        event.preventDefault();
       
        var formData = new FormData(event.target);
        formData.keys();

        var formDataObject = {};
        formData.forEach(function(value, key) {
            formDataObject[key] = value;
        });
        
        $.ajax({
            type: "POST",
            url: '/Admin/UsuarioAplicacion/SetRole',
            data: formDataObject,
            
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

function Delete(id) {
    console.log(id);
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
                url: '/Admin/UsuarioAplicacion/Delete',
                data: JSON.stringify(id),
                contentType: "application/json",
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
