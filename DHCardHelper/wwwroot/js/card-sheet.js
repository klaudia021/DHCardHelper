document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("#add-to-sheet-icon").forEach(btn => {
        btn.addEventListener("click", async () => {
            const select = document.getElementById('character-sheet-list');
            const selectedSheetId = select.value;

            const cardId = btn.dataset.cardid;
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            const response = await fetch(`/Characters/${selectedSheetId}/Cards/${cardId}`, {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "RequestVerificationToken": token
                }
            });

            const data = await response.json();

            if (data.success)
                toastr.success(data.message);
            else
                toastr.error(data.message);
        })
    })
})