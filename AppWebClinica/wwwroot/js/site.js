$(document).ready(function () {
    // Método de validación personalizado para disponibilidad
    $.validator.addMethod("checkAvailability", function (value, element) {
        var isValid = false;
        var currentId = $("#Id").val(); // Obtiene el ID de la cita actual

        $.ajax({
            url: $("#Edit-form").data("availability-url"),
            type: "GET",
            data: {
                fechaHora: value,
                currentId: currentId // Pasa el ID actual
            },
            async: false,
            success: function (response) {
                isValid = response === true;
            }
        });

        return isValid;
    }, "Ya existe una cita en la fecha y hora seleccionada.");

    // Configurar validación
    $("#Edit-form").validate({
        rules: {
            Nombre: {
                required: true,
                minlength: 2
            },
            Email: {
                required: true,
                email: true
            },
            FechaHora: {
                required: true,
                checkAvailability: true
            }
        },
        messages: {
            Nombre: {
                required: "Por favor, ingrese su nombre.",
                minlength: "El nombre debe tener al menos 2 caracteres."
            },
            Email: {
                required: "Por favor, ingrese su correo electrónico.",
                email: "Por favor, ingrese un correo electrónico válido."
            },
            FechaHora: {
                required: "Por favor, seleccione una fecha y hora."
            }
        }
    });

    // Script para actualizar precios
    document.getElementById('Procedimiento').addEventListener('change', function () {
        var procedimiento = this.value;

        var precios = {
            'Limpieza': 15000,
            'Calzas': 25000,
            'Extracciones': 10000,
            'Blanqueamiento': 35000,
            'Ortodoncia': 355000
        };

        var precio = precios[procedimiento] || 0;
        var impuesto = Math.round(precio * 0.13);
        var total = Math.round(precio + impuesto);
        var adelanto = Math.round(total * 0.42);

        document.getElementById('Precio').value = precio;
        document.getElementById('Impuesto').value = impuesto;
        document.getElementById('Total').value = total;
        document.getElementById('Adelanto').value = adelanto;
    });
});
