var dataTable;

$(document).ready(function () {
    loadTable();
})

function loadTable() {
    dataTable = $('#datatblForUserFormPage').DataTable({
        searching: true, paging: false, info: false,
        "ajax": {
            "url": "/QuestionSheet/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "nameOftheForm", "width": "15%" },
            //{
            //    "data": "id", "render": function (data) {
            //        return `
            //            <a onClick=Delete('/Home/Delete/${data}')
            //            class="btn btn-danger mx-2">Delete</a>
            //        `
            //    },
            //    "width": "15%"
            //}
        ],
    });

}

//function Delete(url) {
//    $.ajax({
//        url: url,
//        type: 'DELETE',
//        success: function (data) {
//            if (data.success) {
//                $('#datatbl').DataTable().ajax.reload()
//                Text: "successful"
//            }
//            else {
//                $('#datatbl').DataTable().ajax.reload()
//            }
//        }
//    })
//}