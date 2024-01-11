$(document).ready(function () {
    let i = 0;
    var t = $("#tb_saleproduct").DataTable({
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
            url: "https://localhost:7041/api/Saleproduct",
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
            { "data": "saleProductId" },
            { "data": "productName" },
            { "data": "categoryName" },
            { "data": "dateSale" },
            { "data": "qty" },
            { "data": "price" },
            { "data": "totalPrice" },
            {
                "data": null, render: function (data, type, row) {
                    return '<button class="btn btn-warning"  data-tooltip="tooltip" data-placement="bottom" title="Edit Product" onclick="return GetById(\'' + row.saleProductId + '\')"><i class="fas fa-edit"></i></button>'
                        + '&nbsp;' +
                        '<button class="btn btn-danger" data-tooltip="tooltip" data-placement="bottom" title="Delete Product" onclick="return Delete(\'' + row.saleProductId + '\')"><i class="fas fa-trash"></i></button>'
                }
            }], "order": [],
    });
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
})

var productSelect = document.getElementById('productID');
$.ajax({
    type: "GET",
    url: "https://localhost:7041/api/Product",
    dataType: "json",
    success: function (result) {
        $.each(result.data, function (i, p) {
            var select = `<option value="${p.productId}">${p.name}</option>`;
            $(select).appendTo('#productID');
        });
    }
});

function save() {
    if ($('#dateSale').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Date sale is required!',
        })
    }
    if ($('#qty').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'qty is required!',
        })
    }
    if ($('#dateSale').val() == "" || $('#qty').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'The field is required!',
        })
    } else {
        var saleProduct = new Object();
        saleProduct.saleProductId = "";
        saleProduct.product_id = $('#productID').val();
        saleProduct.dateSale = $('#dateSale').val();
        saleProduct.qty = $('#qty').val();
        $.ajax({
            type: 'POST', //API Method
            url: 'https://localhost:7041/api/Saleproduct',//name of url get
            data: JSON.stringify(saleProduct), //mengubah data department yang diinputkan ke dalam json
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
}

function add() {
    $('#productID').val('');
    $('#dateSale').val('');
    $('#qty').val('');
    $('#save').show();
    $('#update').hide();
}

function GetById(saleProductId) {
    $.ajax({
        url: 'https://localhost:7041/api/Saleproduct/' + saleProductId,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        data: 'json',
        success: function (result) {
            $('#optionProduct').remove();

            var saleProduct = result.data; //adjust with whats your API return
            $('#SaleproductId').val(saleProductId);
            $('#productID').val(saleProduct.product_id);
            $('#dateSale').val(saleProduct.dateSale);
            $('#qty').val(saleProduct.qty);

            var selectProduct = `<option id="optionProduct" value="${saleProduct.product.productId}" selected>${saleProduct.product.name}</option>`;
            $(selectProduct).appendTo('#productID');

            //enable form edit
            $('#exampleModalLabel').text("Edit Data Sale Product");
            $('#save').hide();
            $('#update').show();
            $('#exampleAdd').modal('show'); //untuk menampilkan modal show form tambah-edit
        }
    });
}

//function Update() {
//    var saleProduct = new Object();
//    saleProduct.saleProductId = $('#saleProductId').val();;
//    saleProduct.product_id = $('#productID').val();
//    saleProduct.dateSale = $('#dateSale').val();
//    saleProduct.qty = $('#qty').val();
//    if (saleProduct.dateSale == "") {
//        Swal.fire({
//            icon: 'error',
//            title: 'Error',
//            text: 'Date sale is required!',
//        })
//    }
//    else if (saleProduct.qty == "") {
//        Swal.fire({
//            icon: 'error',
//            title: 'Error',
//            text: 'qty is required!',
//        })
//    }
//    else {
//        Swal.fire({
//            title: 'Update Data?',
//            text: "Yakin data ini diupdate?",
//            icon: 'success',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ya, data diupdate!'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({
//                    url: 'https://localhost:7041/api/Saleproduct/' + saleProduct.saleProductId,//name of url
//                    type: 'PUT', //API Method
//                    data: JSON.stringify(saleProduct), 
//                    contentType: "application/json; charset=utf-8"
//                }).then((result) => {
//                    if (result.status == 200) {
//                        Swal.fire('Data berhasil diupdate', result.message, 'success');
//                        $("#tb_saleproduct").DataTable().ajax.reload();
//                        $("#exampleAdd").modal("hide");
//                    }
//                    else {
//                        Swal.fire('Data gagal di update', result.message, 'error');
//                    }
//                }).catch((error) => {
//                    Swal.fire('Data gagal diupdate', error.responseJSON.message, 'error');

//                })
//            }
//        })
//    }
//}

function Update() {
    var saleProduct = new Object();
    saleProduct.saleProductId = $('#SaleproductId').val();
    console.log($('#SaleproductId').val())
    saleProduct.product_id = $('#productID').val();
    saleProduct.dateSale = $('#dateSale').val();
    saleProduct.qty = $('#qty').val();
    if (saleProduct.qty == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'qty is required!',
        })
    }
    else if (saleProduct.dateSale == "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Date sale is required!',
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
                    url: 'https://localhost:7041/api/Saleproduct/' + saleProduct.saleProductId,//name of url
                    type: 'PUT', //API Method
                    data: JSON.stringify(saleProduct), //mengubah data department yang diinputkan ke dalam json
                    contentType: "application/json; charset=utf-8"
                }).then((result) => {
                    if (result.status == 200) {
                        Swal.fire('Data berhasil diupdate', result.message, 'success');
                        $("#tb_saleproduct").DataTable().ajax.reload();
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

function Delete(saleProductId) {
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
                url: 'https://localhost:7041/api/Saleproduct/' + saleProductId,
                type: 'DELETE',
                data: 'json',
            }).then((result) => {
                if (result.status == 200) {
                    Swal.fire('berhasil dihapus', result.message, 'success');
                    $("#tb_saleproduct").DataTable().ajax.reload();
                } else {
                    Swal.fire('Data gagal dihapus', result.message, 'error');
                }
            });
        }
    })
}