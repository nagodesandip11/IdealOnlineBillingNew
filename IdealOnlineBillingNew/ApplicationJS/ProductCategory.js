

$(document).ready(function () {
    $('.js-basic-example').DataTable({
        responsive: true
    });

    $(".searchData").select2({
        dropdownParent: $("#myModal"),
        width: '100%'
    });

    loadData();
    

});
function loadData()
{
    $.ajax(  
        {
            url: "/ProductType/List",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //console.log(result);
                //var data = jQuery.parseJSON(result);
                //console.log(data);
                var html = '';
                $.each(result, function (key, item) {
                    console.log(item.categoryId);
                    html += '<tr>';
                    //html += '<td>' + item.categoryId + '</td>';
                    html += '<td id="' + item.categoryId+'_catName">' + item.categoryName + '</td>';
                    html += '<td><a href="#" class="btn btn-info" id="' + item.categoryId + '_emp" onclick="getData(this)"><span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Edit</a> <a href="#" class="btn btn-danger" onclick="Delele(' + item.categoryId + ')"><span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;Delete</a></td>';
                    html += '</tr>';
                });
                $('#tdata').html(html);
                $('#dataTable-AllData').DataTable({
                    responsive: true
                });
            }
        });
}
function Update()
{
    var res = validate();
    if (res == false)
    {
        return false;
    }
    else
    {
        var catObj =
        {
            categoryId: $('#CategoryId').val(),
            categoryName: $('#Name').val(),
            isActive: true
        };
        $.ajax({
            url: "/ProductType/Update",
            data: JSON.stringify(catObj),
            type: "POST",
            contentType:"application/json;charset=utf-8",
            dataType: "JSON",
            success: function (result)
            {
                loadData();
                alert('Record Updated Successfully..!!');
                $('#myModal').modal('hide');
            },
        });


    }
}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    else
    {
        var empObj =
        {

            categoryId: $('#categoryId').val(),
            categoryName: $('#Name').val(),
            isActive: true
        };

        $.ajax(
            {
                url: "/ProductType/Add",
                data: JSON.stringify(empObj),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    loadData();
                    alert('Record Inserted Successfully..!!');
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
            url: "/ProductType/Delete/" + ID,
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
function getData(ele) {
    var code = $(ele).attr('id');
    var id = parseInt(code);
    console.log(id);
    $("#CategoryId").val(id);
    $("#Name").val($("#" + id + "_catName").html());
    $('#myModal').modal('show');
    $('#btnUpdate').show();
    $('#btnAdd').hide();
}
function validate()
{
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'red');
        isValid = false;
        alert('Name is Required');
    }
    else
    {
        $('#Name').css('border-color', 'lightgray')
    }
    return isValid;
}
function clearTextBox()
{
    $('#categoryId').val("");
    $('#Name').val("");
    
}