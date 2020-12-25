$(document).ready(function () {
    $("#usertable").DataTable({
        select: true,
        pageLength: 5,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Persian.json"
        },
        ajax: {
            url: '/Panel/UserManage/GetAllUsers',
            dataSrc: '',
            type: "post"
        },
        columns: [
            { title: 'نام کاربری', data: 'userName', "width": "50px" },
            { title: 'شماره تماس', data: 'phoneNumber', "width": "30px" },
            { title: 'ایمیل', data: 'email', "width": "30px" },
            { title: 'وضعیت', data: 'roles', "width": "30px" },
            {
                title: 'ابزار',
                data: 'id',
                render: function (data) {
                    return `<div class="txt-center">
                                    <a href = "/Admin/UserManage/EditUser/${data}" data-toggle="tooltip"  title="ویرایش" class="btn btn-outline-primary btn-sm" style="cursor:pointer;width:35px;" >
                                        <i class="fa fa-pencil-square-o"></i>
                                    </a>
                                      &nbsp
                                    <a onclick =Delete("/Admin/UserManage/DeleteUser/${data}") data-toggle="tooltip" title="حذف" class="btn btn-outline-danger btn-sm" style="cursor:pointer;width:35px;" >
                                        <i class="fa fa-trash"></i>
                                    </a>
                                </div>
                                `;
                }, "width": "30px"
            }
        ]
    });
});
function Delete(v) {
    $.ajax({
        type: "Post",
        cashe: false,
        url: v,
        success: function (result) {
            if (result) {
                refreshTable();
            }
        }
    });
}
function refreshTable() {
    $('#usertable').DataTable().clear();
    $('#usertable').DataTable().ajax.reload();
}