// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#inputsearch').keypress(function (e) {
    var key = e.which;
    if (key == 13) {
        $('#btn-search-order').click();
    }
});

function OnClickSearchOrder(searchOrderUrl) {
    $.ajax({
        url: searchOrderUrl,
        type: "GET",
        data: { categoryName: $('#inputsearch').val() },
        success: function (result) {
            $("#orders-grid-view").html(result);
        }
    });
}
function OnClickCreateNewOrder(createNewOrderUrl) {
    $.ajax({
        url: createNewOrderUrl,
        type: "POST",
        data: $('#frm-order-infor').serialize(),
        success: function (result) {
            if (result.isValid == false) {
                alert(result.message);
                return;
            }

            $('#frm-order-infor').trigger("reset");
            $("#orders-grid-view").html(result);
            $('#createOrderModel').modal('toggle');
            alert("Order has been saved successfully!");
        }
    });
}
