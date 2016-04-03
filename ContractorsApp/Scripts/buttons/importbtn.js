function importcontractors() {
    $.ajax({
        type: "POST",
        url: "api/Contractors"
    });
    location.reload();
}