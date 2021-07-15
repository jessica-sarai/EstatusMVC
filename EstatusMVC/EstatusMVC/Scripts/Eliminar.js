function eliminar() {
    var nombre = $("#<%=txtId.ClientID %>").text() + ' ' +
                 $("#<%=txtClave.ClientID %>").text() + ' ' +
                 $("#<%=txtNombre.ClientID %>").text()
    $("#Modal").text("Desea eliminar al usuario: " + nombre);
    $("#ventanaModal").modal();
}

function eliminarConfirmar() {
    var json = $("#<%=txtId.ClientID %>").text();
    var parametro = JSON.stringify(json);
    /*var json = eliminar();*/
    var urlws = '<%= ResolveUrl("~/Views/RazoADO/Eliminar.cshtml/Eliminar")%>';
    $.ajax({
        type: 'POST',
        url: urlws,                
        data: json,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: exito,
        error: errorGenerico
    });
}

function exito(data) {
            alert("Transacción con exito");
}

function errorGenerico(jqXHR, exeception) {
    var mensaje = '';
    if (jqXHR.status === 0) {
        mensaje = 'No está conectado';
    }
    else if (jqXHR.status === 404) {
        mensaje = 'Página no encontrada [404]';
    }
    else if (jqXHR.status === 500) {
        mensaje = 'Error no hay conexión al servidor [500]';
    }
    else if (jqXHR.status === 'parseerror') {
        mensaje = 'El parseo del JSON es erroneo';
    }
    else if (jqXHR.status === 'timeout') {
        $('body').addClass('loaded');
    }
    else if (jqXHR.status === 'abort') {
        mensaje = 'La petición Ajax fue afortada';
    }
    else {
        mensaje = 'Error no conocido';
        console.log(exeception);
    }
    alert(mensaje);
}