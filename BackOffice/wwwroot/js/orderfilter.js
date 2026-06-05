document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("searchInput");
    const statusFilter = document.getElementById("statusFilter");
    const orderRows = document.querySelectorAll(".order-data-row");
    const resultsCount = document.getElementById("resultsCount");

    // Central function that evaluates BOTH filters at the same time
    function filterOrders() {
        const searchText = searchInput.value.toLowerCase().trim();
        const selectedStatus = statusFilter.value.toLowerCase();
        let visibleCount = 0;

        orderRows.forEach(row => {
            // 1. Get Order Name (First column or .order-name)
            const orderNameElement = row.querySelector(".order-name");
            const orderName = orderNameElement ? orderNameElement.textContent.toLowerCase() : "";

            // 2. Get Status (5th column -> index 4)
            const statusElement = row.children[4];
            const orderStatus = statusElement ? statusElement.innerText.toLowerCase().trim() : "";

            // 3. Evaluate Match Conditions
            const matchesSearch = orderName.includes(searchText);
            const matchesStatus = (selectedStatus === "all" || orderStatus === selectedStatus);

            // Row is visible ONLY if it passes both tests
            if (matchesSearch && matchesStatus) {
                row.style.removeProperty("display"); // Cleanest way to restore your CSS grid layout
                visibleCount++;
            } else {
                row.style.setProperty("display", "none", "important");
            }
        });

        // Update the total live count element text securely
        if (resultsCount) {
            resultsCount.textContent = visibleCount;
        }
    }

    // Bind the same filtering function to both event listeners
    searchInput.addEventListener("input", filterOrders);
    statusFilter.addEventListener("change", filterOrders);
});