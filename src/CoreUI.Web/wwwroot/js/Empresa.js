// creamos un objeto principal para almacenar datos en el navegador
var localStorage = window.localStorage;

class Empresa {

    // constructor 
    constructor(nombre, descripcion, estado, action) {
        // atributos
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action;

    }
}