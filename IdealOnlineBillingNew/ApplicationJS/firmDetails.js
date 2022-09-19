$(document).ready(function () {
    $('.js-basic-example').DataTable({
        responsive: true
    });

    loadData();
    fillState();
    var data;

});

function imgpathUrl(input) {
    $('#dimg')[0].src = (window.URL ? URL : webkitURL).createObjectURL(input.files[0]);
}

function fillState() {
    $('#stList').html('');
    var html = "";
    html += '<option value="" selected>--- Select Employee ---</option>';
    $.ajax(
        {
            url: "/ProductCompany/GetState",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //var s = '<option value="-1">-Select-</option>';
                for (var i = 0; i < result.length; i++) {
                    //console.log(result[i]["EmployeeID"]);
                    html += '<option value="' + result[i]["EmployeeID"] + '">' + result[i]["Name"] + '</option>';
                }
                // console.log(html);
                $("#stList").html(html);
            }

        });
}
function loadData()
{
    if ($.fn.DataTable.isDataTable('#dataTable-AllData')) {
        $('#dataTable-AllData').DataTable().destroy();
    }
    $.ajax({
        url: "/Firm/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var i = 0;
            //var rs = result;
            //console.log(rs);
            $.each(result, function (key, item) {
                
                html += '<tr>';
                //html += '<td>' + item.EmployeeID + '</td>';
                html += '<td id=' + item.firmId + '_trSrNo>' +(i+1)+ '</td>';
                html += '<td id=' + item.firmId + '_trName>' + item.firmName + '</td>';
                html += '<td id=' + item.firmId + '_trfirmAddress>' + item.firmAddress + '</td>';
                html += '<td id=' + item.firmId + '_trfirmcontact>' + item.firmcontact + '</td>';
                html += '<td id=' + item.firmId + '_trgstNo>' + item.gstNo + '</td>';
                html += '<td id=' + item.firmId + '_trsubjectTo>' + item.subjectTo + '</td>';
                html += '<td id=' + item.firmId + '_trfirmFor>' + item.firmFor + '</td>';
                html += '<td id=' + item.firmId + '_trServices>' + item.Services + '</td>';
                html += '<td id=' + item.firmId + '_trbankName>' + item.bankName + '</td>';
                html += '<td id=' + item.firmId + '_tracNo>' + item.acNo + '</td>';
                html += '<td id=' + item.firmId + '_trbranchname>' + item.branchname + '</td>';
                html += '<td id=' + item.firmId + '_trifscCode>' + item.ifscCode + '</td>';
                html += '<td><img id=' + item.firmId + '_trImg src="' + location.protocol + "//" + location.host + '/Images/' + item.logoPath + '" height="30" width="30"></td>';
                html += '<td><a href="#" class="btn btn-info" id="' + item.firmId + '_emp" onclick="getData(this)"><span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Edit</a> <a href="#" class="btn btn-danger" onclick="Delele(' + item.firmId + ')"><span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;Delete</a></td>';
                html += '</tr>';
                i++;
            });
            $('#tdata').html(html);
            $('#dataTable-AllData').DataTable({
                responsive: true
            });
        }
    });
}

function getData(ele) {
    var code = $(ele).attr('id');
    var id = parseInt(code);
    $("#firmId").val(id);
    $("#firmName").val($("#" + id + "_trName").html());
    $("#firmAddress").val($("#" + id + "_trfirmAddress").html());
    $("#firmcontact").val($("#" + id + "_trfirmcontact").html());
    $("#gstNo").val($("#" + id + "_trgstNo").html());
    $("#subjectTo").val($("#" + id + "_trsubjectTo").html());
    $("#firmFor").val($("#" + id + "_trfirmFor").html());
    $("#Services").val($("#" + id + "_trServices").html());
    $("#bankName").val($("#" + id + "_trbankName").html());
    $("#acNo").val($("#" + id + "_tracNo").html());
    $("#branchname").val($("#" + id + "_trbranchname").html());
    $("#ifscCode").val($("#" + id + "_trifscCode").html());


    $("#dimg").attr('src', $("#" + id + "_trImg").attr('src'));
    $('#myModal').modal('show');
    $('#btnUpdate').show();
    $('#btnAdd').hide();

   



}

function loadDetails()
{
    var file = $("#img").get(0).files;
     data = new FormData;
    data.append("firmId", $('#firmId').val());
    data.append("firmName", $("#firmName").val());
    data.append("firmAddress", $("#firmAddress").val());
    data.append("firmcontact", $('#firmcontact').val());
    data.append("gstNo", $('#gstNo').val());
    data.append("subjectTo", $('#subjectTo').val());
    data.append("firmFor", $('#firmFor').val());
    data.append("Services", $('#Services').val());
    data.append("bankName", $('#bankName').val());
    data.append("acNo", $('#acNo').val());
    data.append("branchname", $('#branchname').val());
    data.append("ifscCode", $('#ifscCode').val());
    data.append("ImageFile", file[0]);
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    else {

        loadDetails();

        $.ajax(
            {
                async: true,
                type: "POST",
                dataType: "json",
                url: "/Firm/Add",
                data: data, /*JSON.stringify(data),*/
                processData: false,
                contentType: false, /*"application/json;charset=utf-8",*/

                success: function (result) {
                    if (result) {
                        loadData();
                        alert('Record Inserted Successfully..!!');
                        $('#myModal').modal('hide');
                    } else {
                        alert('Unable to  Insert Record');
                    }

                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            }
        );
    }
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    else {
        loadDetails(); 
        $.ajax(
            {
                url: "/Firm/Update",
                data: data,//JSON.stringify(empObj),
                type: "POST",
                processData: false,
                contentType: false, /*"application/json;charset=utf-8",*/
                dataType: "json",
                success: function (result) {
                    loadData();
                    alert('Record Updated Successfully..!!');
                    $('#myModal').modal('hide');
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            }
        );
    }
}

function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Firm/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#EmployeeID').val("");
    $('#Name').val("");
    $('#Age').val("");
    $('#State').val("");
    $('#Country').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#State').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    //if ($('#Name').val().trim() == "") {
    //    $('#Name').css('border-color', 'red');
    //    isValid = false;
    //    // alert('Name is Required');
    //}
    //else {
    //    $('#Name').css('border-color', 'lightgray')
    //}

    //if ($('#Age').val().trim() == "") {
    //    $('#Age').css('border-color', 'red');
    //    isValid = false;
    //    // alert('Age is Required..!!');

    //} else {
    //    $('#Age').css('border-color', 'lightgray')
    //}

    //if ($('#State').val().trim() == "") {
    //    $('#State').css('border-color', 'red');
    //    isValid = false;
    //    // alert('State is Required..!!');

    //} else {
    //    $('#State').css('border-color', 'lightgray')

    //}

    //if ($('#Country').val().trim() == "") {
    //    $('#Country').css('border-color', 'red');
    //    isValid = false;
    //}
    //else {
    //    $('#Country').css('border-color', 'lightgray')

    //}
}