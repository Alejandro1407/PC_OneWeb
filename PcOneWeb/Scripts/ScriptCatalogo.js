$(document).ready(function () {

    $("#Data").load('/Home/MostrarDatos/', { Categoria: 0, Parametro: "" }); //Al cargar el documento mostrara el Catalogo

    $('.custom-select').on("change", function (e) {
        $('#Data').html('<img src="/Content/img/Loading.gif" class="ml-auto mr-auto d-block"/><h4 class= "d-flex justify-content-center text-muted"> Cargando Productos </h4>');
        $('#txtBuscar').text = "";
        $("#Data").load('/Home/MostrarDatos/',{Categoria:e.target.value});
    });
    $('#txtBuscar').on("keyup", function (e) {
        $('#Data').html('<img src="/Content/img/Loading.gif" class="ml-auto mr-auto d-block"/><h4 class= "d-flex justify-content-center text-muted"> Cargando Productos </h4 >');
        let Categoria = $('.custom-select').value;
        console.log(Categoria);
        $("#Data").load('/Home/MostrarDatos/',{ Categoria: this.Categoria,Parametro:e.target.value });
    });
});

function Mostrar_Info(id) {
    $('#Modal').load('/Home/ModalProducto/', { param: id });
}

function AddtoCart() {
    let Sesion_Iniciated = "";
    Sesion_Iniciated = sessionStorage.getItem("Sesion");
    if (Sesion_Iniciated == null) {
        $('#ConfirmAddToCart').modal('hide');
        $("#modalLoginForm").modal('show');
        return;
    }
    let contador = localStorage.getItem("contador");
    if (contador === null) {
        contador = 0;
    }

    let TempArticulos = JSON.parse(localStorage.getItem("Articulos"));
    if (TempArticulos === null) {
        TempArticulos = [];
    }
    var Current = JSON.parse(localStorage.getItem("Current"));
    var Exists = 0;
    for (let i = 0; i < TempArticulos.length; i++) {
        if (Current.Nombre === TempArticulos[i].Nombre) {
            Exists = 1;
            break;
        }
    }
    if (Exists === 0) {
        TempArticulos[contador] = Current;
        localStorage.setItem("Articulos", JSON.stringify(TempArticulos));
        contador++;
        $('#ConfirmAddToCart').modal('hide');
        localStorage.setItem("contador", contador);
        toastr.success("Articulo Agregado", "Carrito", {
            "progressBar": true,
            "closeButton": true
        });
        localStorage.setItem("contador", contador);
        document.getElementById('Carrito_Conta').innerHTML = contador;
    } else {
        $('#ConfirmAddToCart').modal('hide');
        toastr.error("Articulo Ya en Carrito ", "Carrito", {
            "progressBar": true,
            "closeButton": true
        });
    }
    MostrarCarrito();
}