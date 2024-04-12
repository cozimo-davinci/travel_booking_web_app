$(document).ready(function () {
    // Function to handle form submission for search
    $('#search-form').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        // Get form data
        var formData = $(this).serialize();

        // Show loader or spinner
        $('#loader').show();

        // Make AJAX request
        $.ajax({
            url: $(this).attr('action'), // URL to submit the form
            type: 'GET',
            data: formData,
            success: function (data) {
                // Update search results with response data
                $('#search-results').html(data);
            },
            error: function () {
                // Handle error
                $('#search-results').html('<div class="alert alert-danger">An error occurred.</div>');
            },
            complete: function () {
                // Hide loader or spinner
                $('#loader').hide();
            }
        });
    });
});
