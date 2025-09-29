document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('#remove-card-icon').forEach(btn => {
        btn.addEventListener('click', async () => {
            const cardId = btn.dataset.cardid;
            const characterSheetId = btn.dataset.charactersheetid;
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            if (!cardId || cardId == 0 || !characterSheetId || characterSheetId == 0) {
                toastr.error("Card or character not found!");

                return;
            }

            const result = await Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#21cf35",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            });

            if (result.isConfirmed) {
                const response = await fetch(`/Characters/${characterSheetId}/Delete/${cardId}`, {
                    method: "DELETE",
                    headers: {
                        "Accept": "application/json",
                        "RequestVerificationToken": token
                    }
                });

                const data = await response.json();

                if (data.success) {
                    toastr.success(data.message);
                    const card = document.getElementById(`card-${cardId}`)
                    if (card)
                        card.remove();
                }
                else
                    toastr.error(data.message);
            }
        });
    });
});