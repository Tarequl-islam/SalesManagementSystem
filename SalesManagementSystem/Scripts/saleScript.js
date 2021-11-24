function calculateTotalAmount() {
    totalAmount = 0;

    var code = document.querySelectorAll("#" + table + " tr td .tlCode");
    var totalPrice = document.querySelectorAll("#" + table + " tr td .tlPrice");
    for (var i = 0; i < code.length; i++) {
        if (parseFloat($("#" + totalPrice[i].id).val()) > 0) {
            totalAmount += parseFloat($("#" + totalPrice[i].id).val());
        }
    }
    $("#TotalPrice").val(totalAmount.toFixed(0));
}