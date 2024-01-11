$(document).ready(function () {
    let i = 0;
    var t = $("#tb_product").DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "serverSide": false, // Aktifkan mode server-side
        "processing": false, // Tampilkan pesan pemrosesan
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "ajax": {
            url: "https://localhost:7041/api/Product",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            {
                "data": null, orderable: false, ordering: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": null, render: function (data, type, row) {
                    return '<button class="btn btn-warning"  data-tooltip="tooltip" data-placement="bottom" title="Edit Product" onclick="return GetById(\'' + row.productId + '\')"><i class="fas fa-edit"></i></button>'
                        + '&nbsp;' +
                        '<button class="btn btn-danger" data-tooltip="tooltip" data-placement="bottom" title="Delete Product" onclick="return Delete(\'' + row.productId + '\')"><i class="fas fa-trash"></i></button>'
                }
            }], "order": [],
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
})

var categorySelect = document.getElementById('categoryId');
$.ajax({
    type: "GET",
    url: "https://localhost:7041/api/Category",
    dataType: "json",
    success: function (result) {
        $.each(result.data, function (i, d) {
            var select = `<option value="${d.id}">${d.name}</option>`;
            $(select).appendTo('#categoryId');
        });
    }
});

function save() {
    var product = new Object();
    product.productId = "";
    product.name = $('#name').val();
    product.description = $('#description').val();
    product.price = $('#price').val();
    product.categoryId = $('#categoryId').val();
    $.ajax({
        type: 'POST', //API Method
        url: 'https://localhost:7041/api/Product',//name of url get
        data: JSON.stringify(product), //mengubah data department yang diinputkan ke dalam json
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            Swal.fire('Data berhasil disimpan', result.message, 'success');
            $('#tb_product').DataTable().ajax.reload();
        }
        else {
            Swal.fire('Data Gagal Ditambahkan', result.message, 'error');
        }
    });
}

function add() {
    $('#name').val('');
    $('#name').focus();
    $('#description').val('');
    $('#price').val('');
    $('#categoryId').val('');
    $('#save').show();
    $('#update').hide();
}

function GetById(productId) {
    $.ajax({
        url: 'https://localhost:7041/api/Product/' + productId,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        data: 'json',
        success: function (result) {
            $('#optionCategory').remove();

            var product = result.data; //adjust with whats your API return
            $('#productId').val(product.productId);
            $('#name').val(product.name);
            $('#description').val(product.description);
            $('#price').val(product.price);
            $('#categoryId').val(product.categoryId);

            var selectCategory = `<option id="optionCategory" value="${product.category.id}" selected>${product.category.name}</option>`;
            $(selectCategory).appendTo('#categoryId');

            //enable form edit
            $('#exampleModalLabel').text("Edit Data Product");
            $('#save').hide();
            $('#update').show();
            $('#exampleAdd').modal('show'); //untuk menampilkan modal show form tambah-edit
        }
    });
}

function Update() {
    var product = new Object();
    product.productId = $('#productId').val();;
    product.name = $('#name').val();
    product.description = $('#description').val();
    product.price = $('#price').val();
    product.categoryId = $('#categoryId').val();
    if (product.name == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'name tidak boleh kosong!',
        })
    }
    else if (product.price == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'price tidak boleh kosong!',
        })
    }
    else {
        Swal.fire({
            title: 'Update Data?',
            text: "Yakin data ini diupdate?",
            icon: 'success',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, data diupdate!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:7041/api/Product/' + product.productId,//name of url
                    type: 'PUT', //API Method
                    data: JSON.stringify(product), //mengubah data department yang diinputkan ke dalam json
                    contentType: "application/json; charset=utf-8"
                }).then((result) => {
                    if (result.status == 200) {
                        Swal.fire('Data berhasil diupdate', result.message, 'success');
                        $("#tb_product").DataTable().ajax.reload();
                        $("#exampleAdd").modal("hide");
                    }
                    else {
                        Swal.fire('Data gagal di update', result.message, 'error');
                    }
                }).catch((error) => {
                    Swal.fire('Data gagal diupdate', error.responseJSON.message, 'error');

                })
            }
        })
    }
}


function Delete(productId) {
    $("exampleAdd").modal("hide");
    Swal.fire({
        title: 'Hapus Data?',
        text: "Yakin data ini dihapus?",
        icon: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, data dihapus!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: 'https://localhost:7041/api/Product/' + productId,
                type: 'DELETE',
                data: 'json',
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire('berhasil dihapus', result.message, 'success');
                    $("#tb_product").DataTable().ajax.reload();
                }
                else if (result.status == 400) {
                    Swal.fire('Produk ini memiliki relasi dengan tabel SaleProduct. Anda tidak dapat menghapus produk ini.', result.message, 'error');
                }
                else {
                    Swal.fire('Data gagal dihapus', result.message, 'error');
                }
            }).catch((error) => {
            Swal.fire('Data gagal diupdate', error.responseJSON.message, 'error');

        })
        }
    })
}


