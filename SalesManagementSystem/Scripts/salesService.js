var x = 0;
var table = "gridSales";
var totalAmount = 0;
$(document).ready(function () {
    $("#btnAddRow").click(function () {
        x = x + 1;
        addRow(x);
        itemLoad("#code" + x);
    });
});
function itemLoad(id) {
    var url = "/Sale/getItemLoad";
   
    $.ajax({
        url: "/Sale/getItemLoad",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function saveWork(isNew, isEdit) {
    var autoId = "0";
    if (isEdit) {
        autoId = $("#autoId").val();
    }
    var year = $("#year").val();
    var month = $("#month").select2('data');
    var empType = $("#empType").select2('data');

    var code = document.querySelectorAll("#" + table + " tr td .code");
    
    var price = document.querySelectorAll("#" + table + " tr td .price");
    var quantity = document.querySelectorAll("#" + table + " tr td .quantity");
    var totalPrice = document.querySelectorAll("#" + table + " tr td .totalPrice");



    var objData = {
        addUpdate: isEdit,
        objList: []
    }
    for (var i = 0; i < code.length; i++) {
        if (parseFloat($("#" + totalPrice[i].id).val()) > 0 && $("#" + code[i].id).val() != "0"
            && $("#" + code[i].id).val() != "") {
            var empId = $("#" + code[i].id).val();
            var empName = $("#" + name[i].id).text();
            var designationName = $("#" + designation[i].id).val();
            var departmentName = $("#" + department[i].id).val();
            var sectionName = $("#" + section[i].id).text();
            var unitConsumeAmt = replaceAll($("#" + unit[i].id).val(), ",", "");
            var unitRateAmt = replaceAll($("#" + unitRate[i].id).val(), ",", "");
            var totalPriceAmt = replaceAll($("#" + totalPrice[i].id).val(), ",", "");
            var objInfo = setEmployeeInfoData(0, empId, year, month.id, empType.id);

            objData.objList.push(
                {
                    "vEmpId": empId,
                    "vDesgId": objInfo.vDesignationId,
                    "vSectionName": sectionName,
                    "vDeptId": objInfo.vDepartmentId,
                    "vEmpType": empType.id,
                    "vMonthName": month.text,
                    "vMonth": month.id,
                    "iYear": year,
                    "dIssueDate": getBdToDbFormat(objInfo.dIssueDate),
                    "nConsumeUnit": unitConsumeAmt,
                    "mUnitRate": unitRateAmt,
                    "mTotalAmount": totalPriceAmt
                }
            );
        }
    }
    console.log(objData);
    save(objData, isNew, isEdit);
}
function save(objData, isNew, isEdit) {
    hideButton(isEdit);
    $.ajax({
        url: "/Payroll/ElectricityBill/Save",
        data: objData,
        type: 'POST',
        async: false,
        success: function (res) {
            if (res.success) {
                showButton(isEdit);
                if (!isNew) {
                    $("#electricityBill").modal('hide');
                }
                successNotify(res.message);
                clear();
                tableClear(1);
                $('#tbElectricityBill').DataTable().ajax.reload();
            }
            else {
                errorNotify(res.message);
            }
        }
    });
}
function amountCalculate(id) {
    var quantity = $("#quantity" + id).val();
    var price = $("#price" + id).val();
    console.log(isNaN(quantity));
    console.log(isNaN(price));
    quantity = isNaN(quantity) || quantity=="" ? 1 : quantity;
    price = isNaN(price)||price=="" ? 0 : price;
    $("#totalPrice" + id).val((parseFloat(quantity) * parseFloat(price)).toFixed(0));
}
function calculateTotalAmount() {
    totalAmount = 0;
    
    var code = document.querySelectorAll("#" + table + " tr td .code");
    var totalPrice = document.querySelectorAll("#" + table + " tr td .totalPrice");
    for (var i = 0; i < code.length; i++) {
        if (parseFloat($("#" + totalPrice[i].id).val()) > 0) {
            totalAmount += parseFloat($("#" + totalPrice[i].id).val());
        }
    }
    $("#tlPrice").val(totalAmount.toFixed(2));
}
function addRow(x) {
    var tableArray = new Array();
    tableArray = ['Item Id', 'Price', 'Quantity', 'Total', ''];
    var grid = document.getElementById(table);
    var rowCount = grid.rows.length;
    var tr = grid.insertRow(rowCount);
    tr = grid.insertRow(rowCount);
    for (var c = 0; c < tableArray.length; c++) {
        var td = document.createElement('td');

        td = tr.insertCell(0);
        if (parseInt(x) % 2 == 0) {
            td.setAttribute('style', 'background-color:#fff;color:#000');
        }
        else {
            td.setAttribute('style', 'background-color:#eee;color:#000');
        }
        if (c == 4) {
            var ele = document.createElement('select');
            ele.setAttribute('class', 'form-control input-sm code');
           
            ele.setAttribute('id', 'code' + x);
            ele.setAttribute('type', 'text');
            td.appendChild(ele);
        }
        if (c == 3) {
            var ele = document.createElement('input');
            ele.setAttribute('class', 'form-control input-sm form-control input-sm amount price');
            ele.setAttribute('onkeyup', 'amountCalculate(' + x + ');calculateTotalAmount();');
            ele.setAttribute('maxLength', 8);
            ele.setAttribute('id', 'price' + x);
            ele.setAttribute('type', 'text');
            td.appendChild(ele);
        }
        if (c == 2) {
            var ele = document.createElement('input');
            ele.setAttribute('class', 'form-control input-sm form-control input-sm amount quantity');
            ele.setAttribute('onkeyup', 'amountCalculate(' + x + ');calculateTotalAmount();');
            ele.setAttribute('id', 'quantity' + x);
            ele.setAttribute('maxLength', 8);
            ele.setAttribute('type', 'text');
            td.appendChild(ele);
        }
        if (c == 1) {
            var ele = document.createElement('input');
            ele.setAttribute('class', 'form-control input-sm form-control input-sm amount totalPrice');
            ele.setAttribute('onkeyup', 'calculateTotalAmount(' + x + ');');
            ele.setAttribute('id', 'totalPrice' + x);
            ele.setAttribute('readonly', true);
            ele.setAttribute('type', 'text');
            td.appendChild(ele);
        }
        if (c == 0) {
            var button = document.createElement('a');
            button.innerHTML = '<i class="fa fa-trash-o" aria-hidden="true">Delete</i>';
            button.setAttribute('class', 'btn btn-default btn-sm');
            button.setAttribute('style', 'color:#fc0313');
            button.setAttribute('onclick', 'removeRow(this)');
            td.appendChild(button);
        }
        $("#code" + x).focus();
    }
}



function removeRow(oButton) {
    var tableId = table;
    var tab = document.getElementById(tableId);
    var elements = document.querySelectorAll("#" + tableId + " tr td .code");
    if (elements.length > 1) {
        tab.deleteRow(oButton.parentNode.parentNode.rowIndex);
    }
}


function tableClear(start) {
    var table = document.getElementById('gridSales');
    var rowCount = table.rows.length;
    for (var i = start; i < rowCount; i++) {
        table.deleteRow(start);
    }
}