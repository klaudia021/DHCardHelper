document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('#delete-character').forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

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
                form.submit();
            }
        });
    });
});