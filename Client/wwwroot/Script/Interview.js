﻿var datenow = new Date();
var Company = [];
$(document).ready(function () {
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });

    $('#Interview').dataTable({
        "ajax": {
            url: "/Interview/LoadInterview",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },
        "columnDefs": [
            { "orderable": false, "targets": 4 },
            { "searchable": false, "targets": 4 }
        ],
        "columns": [
            { data: "title" },
            { data: "companyName" },
            { data: "description" },
            {
                data: "interviewDate", render: function (data) {
                    return moment(data).format('DD/MM/YYYY, h:mm a');
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<td><div class='btn-group'><button type='button' class='btn btn-warning' id='BtnEdit' data-toggle='tooltip' data-placement='top' title='Edit' data-original-title='Edit' onclick=GetById('" + row.id + "');><i class='fa fa-pencil'></i></button> <button type='button' class='btn btn-danger' id='BtnDelete' data-toggle='tooltip' data-placement='top' data-original-title='Delete' onclick=Delete('" + row.id + "');><i class='fa fa-trash'></i></button></div></td>";
                }
            },
        ]
    });
    LoadCompany($('#CompanyOption'));
}); //load table Interview
/*--------------------------------------------------------------------------------------------------*/
function LoadCompany(element) {
    if (Company.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Company/LoadCompany",
            success: function (data) {
                Company = data.data;
                renderCompany(element);
            }
        });
    }
    else {
        renderCompany(element);
    }
} //load company
function renderCompany(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Company').hide());
    $.each(Company, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
} // Memasukan LoadCompany ke dropdown
/*--------------------------------------------------------------------------------------------------*/
document.getElementById("BtnAdd").addEventListener("click", function () {
    clearscreen();
    $('#Gender').val('selected');
    $('#Experience').val('selected');
    $('#LastEducation').val('selected');
    $('#SaveBtn').show();
    $('#UpdateBtn').hide();
    LoadCompany($('#CompanyOption'));
}); //fungsi btn add
/*--------------------------------------------------------------------------------------------------*/
function clearscreen() {
    $('#Id').val('');
    $('#Title').val('');
    $('#CompanyOption').val('');
    $('#Division').val('');
    $('#Gender').val('');
    $('#Address').val('');
    $('#JobDesk').val('');
    $('#Experience').val('');
    $('#LastEducation').val('');
    $('#Description').val('');
    $('#AddressInterview').val('');
    $('#InterviewDate').val('');
    LoadCompany($('#CompanyOption'));
} //clear field
/*--------------------------------------------------------------------------------------------------*/
function GetById(Id) {
    debugger;
    $.ajax({
        url: "/Interview/GetById/" + Id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            debugger;
            $('#Id').val(result.id);
            $('#Title').val(result.title);
            $('#CompanyOption').val(result.companyId);
            $('#Division').val(result.division);
            $('#Gender').val(result.gender);
            $('#Address').val(result.address);
            $('#JobDesk').val(result.jobDesk);
            $('#Experience').val(result.experience);
            $('#LastEducation').val(result.lastEducation);
            $('#Description').val(result.description);
            $('#AddressInterview').val(result.addressInterview);
            $('#InterviewDate').val(moment(result.interviewDate).format('YYYY-MM-DD'));
            $('#myModal').modal('show');
            $('#UpdateBtn').show();
            $('#SaveBtn').hide();
            //$('#divemail').hide();
            //$('#divpass').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responsText);
        }
    })
} //get id to edit
/*--------------------------------------------------------------------------------------------------*/
function Save() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Interview').DataTable({
        "ajax": {
            url: "/Interview/LoadInterview"
        }
    });
    var Interview = new Object();
    Interview.Id = $('#Id').val();
    Interview.Title = $('#Title').val();
    Interview.CompanyId = $('#CompanyOption').val();
    Interview.Division = $('#Division').val();
    Interview.JobDesk = $('#JobDesk').val();
    Interview.Address = $('#Address').val();
    Interview.Gender = $('#Gender').val();
    Interview.Experience = $('#Experience').val();
    Interview.LastEducation = $('#LastEducation').val();
    Interview.InterviewDate = $('#InterviewDate').val();
    Interview.Description = $('#Description').val();
    Interview.AddressInterview = $('#Description').val();
    $.ajax({
        type: 'POST',
        url: '/Interview/InsertOrUpdate',
        data: Interview
    }).then((result) => {
        if (result.statusCode === 200) {
            Swal.fire({
                icon: 'success',
                potition: 'center',
                title: 'Interview Add Successfully',
                timer: 2500
            }).then(function () {
                table.ajax.reload();
                $('#myModal').modal('hide');
                clearscreen();
            });
        }
        else {
            Swal.fire('Error', 'Failed to Input', 'error');
        }
    })
} //function save
/*--------------------------------------------------------------------------------------------------*/
function Edit() {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Interview').DataTable({
        "ajax": {
            url: "/Interview/LoadInterview"
        }
    });
    var Interview = new Object();
    Interview.Id = $('#Id').val();
    Interview.Title = $('#Title').val();
    Interview.CompanyId = $('#CompanyOption').val();
    Interview.Division = $('#Division').val();
    Interview.JobDesk = $('#JobDesk').val();
    Interview.Address = $('#Address').val();
    Interview.Gender = $('#Gender').val();
    Interview.Experience = $('#Experience').val();
    Interview.LastEducation = $('#LastEducation').val();
    Interview.InterviewDate = $('#InterviewDate').val();
    Interview.Description = $('#Description').val();
    Interview.AddressInterview = $('#AddressInterview').val();
    $.ajax({
        type: 'POST',
        url: '/Interview/InsertOrUpdate',
        data: Interview
    }).then((result) => {
        debugger;
        if (result.statusCode === 200) {
            Swal.fire({
                icon: 'success',
                potition: 'center',
                title: 'Interview Update Successfully',
                timer: 2500
            }).then(function () {
                table.ajax.reload();
                $('#myModal').modal('hide');
                clearscreen();
            });
        } else {
            Swal.fire('Error', 'Failed to Edit', 'error');
        }
    })
}//function edit
/*--------------------------------------------------------------------------------------------------*/
function Delete(Id) {
    $.fn.dataTable.ext.errMode = 'none';
    var table = $('#Interview').DataTable({
        "ajax": {
            url: "/Interview/LoadInterview"
        }
    });
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.value) {
            //debugger;
            $.ajax({
                url: "/Interview/Delete/",
                data: { Id: Id }
            }).then((result) => {
                debugger;
                if (result.statusCode == 200) {
                    Swal.fire({
                        icon: 'success',
                        position: 'center',
                        title: 'Delete Successfully',
                        timer: 2000
                    }).then(function () {
                        table.ajax.reload();
                        cls();
                        $('#Interview').modal('hide');
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'error',
                        text: 'Failed to Delete',
                    })
                    ClearScreen();
                }
            })
        }
    });
} //function delete
