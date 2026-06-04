// Haal het canvas element op waar de grafiek in komt
var ctx = document.getElementById('ordersChart').getContext('2d');

// Maak een nieuwe Chart.js lijndiagram
new Chart(ctx, {
    type: 'line',  // Type grafiek: lijn
    data: {
        // X-as labels: maanden
        labels: ['Jan', 'Feb', 'Mrt', 'Apr', 'Mei', 'Jun'],
        datasets: [
            {
                // Dataset 1: Blauwe lijn voor bestellingen
                label: 'Bestellingen',
                data: [2, 4, 3, 5, 4, 6],
                borderColor: '#4285f4',                  // Lijnkleur: blauw
                backgroundColor: 'rgba(66, 133, 244, 0.1)', // Vulkleur: lichtblauw
                fill: true,      // Vul het gebied onder de lijn
                tension: 0.4     // Maak de lijn vloeiend (0 = recht, 1 = heel rond)
            },
            {
                // Dataset 2: Rode lijn voor afgeleverde orders
                label: 'Afgeleverd',
                data: [1, 3, 2, 4, 3, 5],
                borderColor: '#ea4335',
                backgroundColor: 'rgba(234, 67, 53, 0.1)',
                fill: true,
                tension: 0.4
            }
        ]
    },
    options: {
        responsive: true,  // Past zich aan aan schermgrootte
        plugins: {
            legend: {
                position: 'bottom'  // Legenda onder de grafiek
            }
        },
        scales: {
            y: {
                beginAtZero: true  // Y-as begint altijd bij 0
            }
        }
    }
});