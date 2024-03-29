﻿$(document).ready(function () {
    $('.js-basic-example').DataTable({
        responsive: true
    });

    //$(".searchData").select2({
    //    dropdownParent: $("#myModal"),
    //    width: '100%'
    //});
    $('#dataTable-AllData').dataTable({
        searching: false
    });
    loadData();
    fillState();

});
function fillState()
{
    $('#stList').html('');    
    var html = "";
    html+='<option value="" selected>--- Select Employee ---</option>';
    $.ajax(
        {
            url: "/ProductCompany/GetState",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result)
            {
                //var s = '<option value="-1">-Select-</option>';
                for (var i = 0; i < result.length; i++)
                {
                    //console.log(result[i]["EmployeeID"]);
                    html += '<option value="' + result[i]["EmployeeID"] + '">' + result[i]["Name"] + '</option>';
                }
               // console.log(html);
                $("#stList").html(html);
            }

        });
}
function loadData() {

        if ($.fn.DataTable.isDataTable('#dataTable-AllData')) {
            $('#dataTable-AllData').DataTable().destroy();
        }
        $.ajax({
            url: "/ProductCompany/List",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                var html = '';
                var rs = result;
                console.log(rs);
                $.each(result, function (key, item) {

                    


                    html += '<tr>';
                    //html += '<td>' + item.EmployeeID + '</td>';
                    html += '<td id=' + item.EmployeeID+'_trName>' + item.Name + '</td>';
                    html += '<td id=' + item.EmployeeID +'_trAge>' + item.Age + '</td>';
                    html += '<td id=' + item.EmployeeID +'_trState>' + item.State + '</td>';
                    html += '<td id=' + item.EmployeeID +'_trCountry>' + item.Country + '</td>';
                    html += '<td><img id=' + item.EmployeeID +'_trImg src="' + location.protocol + "//" + location.host + '/Images/' + item.ImagePath +'" height="30" width="30"></td>';
                    html += '<td><a href="#" class="btn btn-info" id="' + item.EmployeeID + '_emp" onclick="getData(this)"><span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Edit</a> <a href="#" class="btn btn-danger" onclick="Delele(' + item.EmployeeID + ')"><span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;Delete</a></td>';
                    html += '</tr>';
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
    $("#EmployeeID").val(id);
    $("#Name").val($("#" + id + "_trName").html());
    $("#Age").val($("#" + id + "_trAge").html());
    $("#State").val($("#" + id + "_trState").html());
    $("#Country").val($("#" + id + "_trCountry").html());
    $("#dimg").attr('src', $("#" + id + "_trImg").attr('src'));
    $('#myModal').modal('show');
    $('#btnUpdate').show();
    $('#btnAdd').hide();
}

function Add()
{
    var res = validate();
    if (res == false)
    {
        return false;
    }
    else {
        //var empObj = {

        //    EmployeeID: $('#EmployeeID').val(),
        //    Name: $('#Name').val(),
        //    Age: $('#Age').val(),
        //    State: $('#State').val(),
        //    Country: $('#Country').val(),
        //    ImagePath: $('#img').get(0).files

        //};

        var file = $("#img").get(0).files;
        var data = new FormData;
        data.append("EmployeeID", $('#EmployeeID').val());
        data.append("Name", $("#Name").val());
        data.append("Age", $("#Age").val());
        data.append("State", $('#State').val());
        data.append("Country", $('#Country').val());
        data.append("ImageFile", file[0]);
      
        $.ajax(
            {
                async: true,
                type: "POST",
                dataType: "json",
                url: "/ProductCompany/Add",
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
        var empObj = {

            EmployeeID: $('#EmployeeID').val(),
            Name: $('#Name').val(),
            Age: $('#Age').val(),
            State: $('#State').val(),
            Country: $('#Country').val()
        };

        $.ajax(
            {
                url: "/ProductCompany/Update",
                data: JSON.stringify(empObj),
                type: "POST",
                contentType: "application/json;charset=utf-8",
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
            url: "/ProductCompany/Delete/" + ID,
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
function validate()
{
    var isValid = true;
    if ($('#Name').val().trim()=="")
    {
        $('#Name').css('border-color', 'red');
        isValid = false;
       // alert('Name is Required');
    }
    else {
        $('#Name').css('border-color','lightgray')
    }

    if ($('#Age').val().trim() == "")
    {
        $('#Age').css('border-color', 'red');
        isValid = false;
       // alert('Age is Required..!!');

    } else
    {
        $('#Age').css('border-color', 'lightgray')
    }

    if ($('#State').val().trim()=="")
    {
        $('#State').css('border-color', 'red');
        isValid = false;
       // alert('State is Required..!!');

    } else {
        $('#State').css('border-color','lightgray')

    }

    if ($('#Country').val().trim()=="")
    {
        $('#Country').css('border-color', 'red');
        isValid = false;
    }
    else {
        $('#Country').css('border-color', 'lightgray')

    }
}