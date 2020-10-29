// Write your JavaScript code. victor
// ejecutar eventos por ventanas
$().ready(() => {
    var URLactual = window.location;

    switch (URLactual.pathname) {
        case "/TiposHabitacions":
            filtrarDatosTipoHabitaciones(1);
            break;
        case "/Habitaciones":
            filtrarDatosHabitaciones(1);
            break;
        case "/Sucursales":
            filtrarDatosSucursales(1);
            break;
        case "/Paises":
            filtrarPaises(1);
            break;
    }
});
////////////////////////////


/////////////////////
//////modals/////////
/////////////////////

//modal usuario
$('#modalEditarUsuario').on('shown.bs.modal', function () {
    //$('#myInput')action.trigger('focus');
});
//modal tipohabitacion
$('#modalTipoHabitacion').on('shown.bs.modal', function () {
    //$('#myInput')action.trigger('focus')
});

//  modal habitacion 
$('#modalHabitacion').on('shown.bs.modal', function () {
    //$('#myInput')action.trigger('focus')
});

//  modal sucursal
$('#modalSucursal').on('shown.bs.modal', function () {
    //$('#myInput')action.trigger('focus')
});


///////////////////////////////////
//////////Usuarios////////////////
/////////////////////////////////


function getUsuario(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            mostrarUsuario(response);
        }
    });
};

// variable global 
var items;
var j = 0;

// variables globales con propiedades de cada una de nuestras clases
var id;
var userName;
var email;
var phoneNumber;
var role;
var selectRole;

// otras variables donde almacenar los datos de registro, pero estos datos no seran modificados
var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;
var pag;

//// seccion para ejecutar acciones 
//$().ready(() => {
//    var URLactual = window.location;

//    switch (URLactual.pathname) {
//        case "/TiposHabitacions":
//            //filtrarDatosTipoHabitaciones(1);
//            break;
//    }
//});



function mostrarUsuario(response) {
    items = response;
    j = 0;
   
    for (var i = 0; i < 3; i++) {
        var x = document.getElementById("Select");
        x.remove(i);
    }

    $.each(items, function (i, val) {

        //alert('prueba' + i + ' segunda ' + val.id);
        $('input[name=id]').val(val.id)
        $('input[name = UserName]').val(val.userName);
        $('input[name = Email]').val(val.email);
        $('input[name = PhoneNumber]').val(val.phoneNumber);
        document.getElementById('Select').options[0] = new Option(val.role, val.roleId);


        //Mostrar los detalles del usuario
        $("#dEmail").text(val.email);
        $("#dUserName").text(val.userName);
        $("#dPhoneNumber").text(val.phoneNumber);
        $("#dRole").text(val.role);

        // mostrar usuario que deseo eliminar 
        $("#eUsuario").text(val.email);
        $('input[name=EIdUsuario]').val(val.id);

    });
};

function getRoles(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: {},
        success: function (response) {
            if (j == 0) {
                for (var i = 0; i < response.length; i++) {
                    document.getElementById('Select').options[i] = new Option(response[i].text, response[i].value);
                    document.getElementById('SelectNuevo').options[i] = new Option(response[i].text, response[i].value);
                    //document.getElementById('SelectNuevo').options[i] = new Option(response[i].text, response[i].value);
                }
                j = 1;
            }
        }
    });
};

function editarUsuario(action) {
    // obtener  los datos de los input con datos para editar
    id = $('input[name=id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    role = document.getElementById('Select');
    selectRole = role.options[role.selectedIndex].text;

    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: {
            id, userName, email, phoneNumber, accessFailedCount,
            concurrencyStamp, emailConfirmed, lockoutEnabled, lockoutEnd,
            normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed,
            securityStamp, twoFactorEnabled, selectRole
        },
        success: function (response) {
            if (response == "Save") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede editar los datos del usuario");
            }
        }
    });

};

function ocultarDetalleUsuario() {
    $("#modalDetalleUsuario").modal("hide");
};


function eliminarUsuario(action) {
    var id = $('input[name=EIdUsuario]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {  
            if (response == "Delete") {
                window.location.href = "Usuarios";
            }
            else
                alert("no se puede eliminar el registro")
        }
    })
};

function crearUsuario(action) {
    // obtener los datos ingresados en los imput
    email = $('input[name = EmailNuevo]')[0].value;
    phoneNumber = $('input[name = PhoneNumberNuevo]')[0].value;
    passwordHash = $('input[name = PasswordHashNuevo]')[0].value;

    role = document.getElementById('SelectNuevo');
    selectRole = role.options[role.selectedIndex].text;

    if (email == "") {
        $("#EmailNuevo").focus();
        alert("Ingrese email del usuario");
    }
    else {
        if (passwordHash == "") {
            $("#PasswordHashNuevo").focus;
            alert("Ingrese contraseña del usuario");
        }
        else {
            $.ajax({
                type: "POST",
                url: action,
                data: {
                    email, phoneNumber, passwordHash, selectRole
                },
                success: function (response) {
                    if (response == "Save") {
                        window.location.href = "Usuarios";
                    }
                    else {
                        $('#mensajenuevo').html("No se puede guardar el usuario <br/> Seleccione un rol <br/> Ingrese un email. <br/> La contraseña debe tener de 6 a 100 caracteres, almenos un caracter especial, una letra mayuscula y un número");
                    }
                }
            });
        }
    }
};

////////////////////////////////////
//////////Tipo habitacion//////////
//////////////////////////////////
function filtrarDatosTipoHabitaciones(pag) {
    var pag = pag;
    var valor = "";
    var order = "";

    var action = 'TiposHabitacions/RecuperaTipoHabitacion';
    var tipohabitacion = new Administracion(pag, valor, order, "", "", "", "", "", "", "", "", "", "", "","", action);
    tipohabitacion.getTipoHabitacion();
};

function adminModalTipoHabitacion(funcion, id, idSucursal) {
    var funcion = funcion;
    var id = id;
    var idSucursal = idSucursal;
    var action = 'TiposHabitacions/ModalTipohabitacion';
    var tipohabitacion = new Administracion(funcion, id, idSucursal, "", "", "", "", "", "", "", "", "", "", "", "", action);
    tipohabitacion.modalTipoDatos();
};

function altaTipoHabitacion(fun, id) {
    var action = 'TiposHabitacions/AdministraTipoHab';
    var funcion = fun;
    var id = id;
    var nombre = document.getElementById("nombre").value;
    var nombreCorto = document.getElementById("nombre_c").value;
    var nPersonas = document.getElementById("nPersonas").value;
    var precioHabitacion = document.getElementById("precioHabitacion").value;
    var precioNinio = document.getElementById("precioNinio").value;
    var precioAdulto = document.getElementById("precioAdulto").value;
    var observaciones = document.getElementById("observaciones").value;
    var grupoSucursalId = 0;
    var estatus = 0;
    var grupoSucursal = document.getElementById('sucursalId');
    grupoSucursalId = grupoSucursal.options[grupoSucursal.selectedIndex].value;
 
    if (funcion == 1) {
        estatus = 0;
    }
    else {
       
        var grupoEstatus = document.getElementById('estatus');
        estatus = grupoEstatus.options[grupoEstatus.selectedIndex].value;
       
    };
    var tipohabitacion = new Administracion(funcion, id, grupoSucursalId, nombre, nombreCorto, nPersonas, precioHabitacion,
        precioNinio, precioAdulto, observaciones, estatus, "", "", "", "", action);
    tipohabitacion.adminTipoHabitacion();

};

////// habitaciones /////////
/////////////////////////////

function filtrarDatosHabitaciones(pag) {
    var pag = pag;
    var valor = "";
    var order = "";

    var action = 'Habitaciones/ListarHabitaciones';
    var habitacion = new Administracion(pag, valor, order, "", "", "", "", "", "", "", "", "", "", "", "", action);
    habitacion.getHabitacion();
};

function creaModalHabitaciones(funcion, id) {
    var funcion = funcion;
    var id = id;
    var action = 'Habitaciones/CreaModalHabitaciones';

        
        var habitacion = new Administracion(funcion, id, "", "", "", "", "", "", "", "", "", "", "", "", "", action);
        habitacion.CrearModalHabitaciones();  

}




function guardarHabitacion(funcion, id){
    var funcion = funcion;
    var id = id;
    var action = 'Habitaciones/GuardarHabitacion';

    if (funcion == 1) { // guardar nuevo registro 

        var sucursalId = document.getElementById("SucursalesId").value;
        var nombre = document.getElementById("Nombre").value;
        var nombreCorto = document.getElementById("NombreCorto").value;
        var numeroHabitacion = document.getElementById("NoHabitacion").value;
        var precioNinio = document.getElementById(" precioNinio").value
        var claveHabitacion = document.getElementById("ClaveHabitacion").value;
        var estatusAdministrador = document.getElementById("EstatusAdminitrador").value;
        var pisoId = document.getElementById("PisoId").value;
            
        if (sucursalId == 0) {
            alert("Favor de seleccionar una sucursal ");
            
        } else {
            if (nombre == "") {
                alert("Favor de escribir un nombre ");
                return;
            } else {
                if (nombreCorto == "") {
                    alert("Favor de escribir un nombre ");
                    return;
                } else {
                    if (numeroHabitacion == "") {
                        alert("Favor de escribir un Número de Habitación");
                        return;
                    } else {
                        if (claveHabitacion == "") {
                            alert("Favor de escribir una clave de Habitación");
                            return;
                        } else {
                            if (estatusAdministrador == "") {
                                alert("Favor de escribir el estatus");
                                return;
                            } else {
                                if (pisoId == "") {
                                    alert("Favor de escribir el estatus");
                                    return;
                                }
                                else {

                                    if (precioNinio == "") {
                                        alert("Favor de escribir el precio de niño");
                                        return;
                                    }
                                    else {
                                        var habitacion = new Administracion(sucursalId, nombre, nombreCorto, "", "", "", "", "", "", "", "", "", "", "", "", action);
                                        habitacion.GuardarHabitacion();

                                    }
                                }
                           
                            }

                                

                        }
                    }
                }
            }
        }    


    }



}



///////////////////////////Sucursales

function filtrarDatosSucursales(pag) {
    var pag = pag;
    var valor = "";
    var order = "";

    var action = 'Sucursales/ListarSucursales';
    var estado = new Administracion(pag, valor, order, "", "", "", "", "", "", "", "", "", "", "", "", action);
    estado.getSucursal();
};


function creaModalSucursales(funcion, id) {
    var funcion = funcion;
    var id = id;
    var action = 'Sucursales/CreaModalSucursales';

    if (funcion == 1) {
        var sucursal = new Administracion(funcion, id, "", "", "", "", "", "", "", "", "", "", "", "", "", action);
        sucursal.creaModalSucursales();
    } else if (funcion == 2) {

    } else if (funcion == 3) {

    } else if (funcion == 4) {

    }


}



///////////////////////////PAISES

function filtrarPaises(pag) {
    var pag = pag;
    var valor = "";
    var order = "";

    var action = 'Paises/ListarPaises';
    var estado = new Administracion(pag, valor, order, "", "", "", "", "", "", "", "", "", "", "", "", action);
    estado.getPaises();
};


            