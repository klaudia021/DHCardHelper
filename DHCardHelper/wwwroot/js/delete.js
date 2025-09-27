document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('#delete-icon').forEach(btn => {
        btn.addEventListener('click', async () => {
            const id = btn.dataset.id;
            const type = btn.dataset.type;
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            if (!id || !type)
                toastr.error("Card not found!");

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
                const response = await fetch(`/GameMaster/Cards/${type}/Delete/${id}`, {
                    method: "DELETE",
                    headers: {
                        "Accept": "application/json",
                        "RequestVerificationToken": token
                    }
                });

                const data = await response.json();

                if (data.success) {
                    toastr.success(data.message);
                    const card = document.getElementById(`card-${id}`)
                    if (card)
                        card.remove();
                }
                else
                    toastr.error(data.message);
            }
        });
    });
});