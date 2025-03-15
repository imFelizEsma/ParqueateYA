AOS.init({
    duration: 1000,
    once: true
});

let tarifas = {
    "Motocicleta": 10,
    "Carro/Jeepeta": 15,
    "Camión": 20
};

let capacidad = {
    "Motocicleta": 10,
    "Carro/Jeepeta": 15,
    "Camión": 5
};

let estacionamientos = [];

function inicializarEstacionamientos() {
    estacionamientos = [];
    for (let tipo in capacidad) {
        for (let i = 1; i <= capacidad[tipo]; i++) {
            estacionamientos.push({
                id: `${tipo}-${i}`,
                tipoVehiculo: tipo,
                ocupado: false,
                codigoTicket: null,
                horaIngreso: null
            });
        }
    }
}

inicializarEstacionamientos();

function generarTicket(event) {
    event.preventDefault();
    const tipoVehiculo = document.getElementById('tipoVehiculo').value;

    console.log('Tipo de Vehículo seleccionado:', tipoVehiculo);

    if (!tipoVehiculo) {
        alert('Por favor, selecciona un tipo de vehículo.');
        return;
    }

    const data = {
        tipoVehiculo: tipoVehiculo
    };

    fetch('https://localhost:7205/api/vehicles/entry', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(error => {
                throw new Error(error.message || 'Error al generar el ticket.');
            });
        }
        return response.json();
    })
    .then(ticket => {
        document.getElementById('codigoTicketGenerado').innerText = ticket.codigo;
        document.getElementById('horaIngreso').innerText = new Date(ticket.horaIngreso).toLocaleString();
        document.getElementById('ticketInfo').style.display = 'block';

        // Limpiar el campo
        document.getElementById('tipoVehiculo').value = '';
    })
    .catch(error => {
        console.error('Error:', error);
        alert(`Hubo un error al generar el ticket: ${error.message}`);
    });
}

function calcularPago(event) {
    event.preventDefault();
    const codigoTicket = document.getElementById('codigoTicketSalida').value;

    if (!codigoTicket) {
        alert('Por favor, ingresa el código del ticket.');
        return;
    }

    const data = {
        codigoTicket: codigoTicket
    };

    fetch('https://localhost:7205/api/vehicles/exit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(error => {
                throw new Error(error.message || 'Error al registrar la salida.');
            });
        }
        return response.json();
    })
    .then(result => {
        document.getElementById('pagoInfo').innerHTML = `
            <div class="alert alert-info">
                <h4 class="alert-heading">${result.message}</h4>
                <p><strong>Total a Pagar:</strong> $${parseFloat(result.totalPago).toFixed(2)}</p>
            </div>
        `;
        document.getElementById('pagoInfo').style.display = 'block';

        // Limpiar el campo
        document.getElementById('codigoTicketSalida').value = '';
    })
    .catch(error => {
        console.error('Error:', error);
        alert(`Hubo un error: ${error.message}`);
    });
}
