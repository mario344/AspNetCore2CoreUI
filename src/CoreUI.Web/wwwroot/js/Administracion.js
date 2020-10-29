class Administracion {

    constructor(var1, var2, var3, var4, var5, var6, var7, var8, var9, var10, var11, var12, var13, var14, var15, action) {
        this.var1 = var1;
        this.var2 = var2;
        this.var3 = var3;
        this.var4 = var4;
        this.var5 = var5;
        this.var6 = var6;
        this.var7 = var7;
        this.var8 = var8;
        this.var9 = var9;
        this.var10 = var10;
        this.var11 = var11;
        this.var12 = var12;
        this.var13 = var13;
        this.var14 = var14;
        this.var15 = var15;
        this.action = action;
    }

    ////////////////////////////////
    /////// tipo de habitacion/////
    ///////////////////////////////

    getTipoHabitacion() {
        var action = this.action;
        var numPagina = this.var1;
        var valor = this.var2;
        var order = this.var3;

        $.ajax({
            type: "POST",
            url: action,
            data: { numPagina, valor, order },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#idTipoHabitacion").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });

    };

    modalTipoDatos() {
        var funcion = this.var1;
        var id = this.var2;
        var claveSucursal = this.var3;
        var action = this.action;



        $.ajax({
            type: "POST",
            url: action,
            data: { funcion, id, claveSucursal },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#contentModalTipoHabitacion").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });
    };

    adminTipoHabitacion() {
        var action = this.action;
        var funcion = this.var1;
        var id = this.var2;
        var idSucursal = this.var3;
        var nombre = this.var4;
        var nombreCorto = this.var5;
        var NoPersonas = this.var6;
        var precioHabitacion = this.var7;
        var precioNinio = this.var8;
        var precioExtra = this.var9;
        var observaciones = this.var10;        
        var estatus = this.var11;       

        if (idSucursal == 0) {
            document.getElementById("mensaje").innerHTML = "Favor de seleccionar una sucursal";

        } else {
            if (nombre == "") {
                document.getElementById("mensaje").innerHTML = "Favor de ingresar un nombre";
            }
            else {
                if (nombreCorto == "") {
                    document.getElementById("mensaje").innerHTML = "Favor de ingresar un nombre corto";
                }
                else {
                    if (NoPersonas <= 0) {
                        document.getElementById("mensaje").innerHTML = "Favor de ingresar el numero de personas en una habitación";
                    }
                    else {
                        if (precioHabitacion <= 0) {
                            document.getElementById("mensaje").innerHTML = "Favor de ingresar el precio de habitación";
                        } else {
                            if (precioNinio <= 0) {
                                document.getElementById("mensaje").innerHTML = "Favor de ingresar el precio de niño extra";
                            } else {
                                if (precioExtra <= 0) {
                                    document.getElementById("mensaje").innerHTML = "Favor de ingresar el precio de persona extra";
                                } else {

                                    $.ajax({
                                        type: "POST",
                                        url: action,
                                        data: { funcion, id, idSucursal, nombre, nombreCorto, NoPersonas, precioHabitacion, precioNinio, precioExtra, observaciones, estatus },
                                        success: (response) => {
                                            if ("save" == response[0].code) {
                                                this.restablecerTipohabitacion();

                                            } else {
                                                document.getElementById("mensaje").innerHTML = "No se puede tipo de habitación";
                                            }
                                        }
                                    });

                                };
                            };
                        };
                    };
                };
            };
        };
    };

    restablecerTipohabitacion() {       

        document.getElementById("nombre").value = "";
        document.getElementById("nombre_c").value = "";
        document.getElementById("nPersonas").value = 0;
        document.getElementById("precioHabitacion").value = 0;
        document.getElementById("precioNinio").value = 0;
        document.getElementById("precioAdulto").value = 0;
        document.getElementById("sucursalId").selectedIndex = 0;
        document.getElementById("estatus").selectedIndex = 0;
        filtrarDatosTipoHabitaciones(1);
        $('#modalTipohabitacion').modal('hide');

    };

    //////////////////////////////////////////////
    /////habitaciones///////////////////////
    ///////////////////////////////////

    getHabitacion() {
        var pagina = this.var1;
        var busqueda = this.var2;
        var order = this.var3;
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action,
            data: { pagina, busqueda},
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#respuestaHabitaciones").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });



    };

    CrearModalHabitaciones() {
        var funcion = this.var1;
        var id = this.var2;
        var action = this.action;


        $.ajax({
            type: "POST",
            url: action,
            data: { funcion, id },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#modalHabitacionCuerpo").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });

    }


    GuardarHabitacion() {

    }


   

    getSucursal()
    {
        var pagina = this.var1;
        var busqueda = this.busqueda;
        var oder = this.var3;
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action,
            data: { pagina, busqueda},
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#respuestaSucursales").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });


    };

    /////////////////modal sucursal
    creaModalSucursales() {
        var funcion = this.var1;
        var id = this.var2;
        var action = this.action;


        $.ajax({
            type: "POST",
            url: action,
            data: { funcion, id },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#modalSucursalesCuerpo").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });

    }


    ///////////////////////////PAISES

    getPaises() {
        var pagina = this.var1;
        var busqueda = this.busqueda;
        var oder = this.var3;
        var action = this.action;

        $.ajax({
            type: "POST",
            url: action,
            data: { pagina, busqueda },
            success: (response) => {
                $.each(response, (index, val) => {
                    $("#respuestaPaises").html(response[0]);
                    // $("#paginado").html(response[1]);
                });

            }
        });


    };


};