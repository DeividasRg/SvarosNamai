var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/GetAll" },
        "columns": [
            { data: 'orderId', width: "5%" },
            {
                data: function (row) {
                    return row.name + ' ' + row.lastName;
                },
                width: "10%"
            },
            {
                data: function (row) {
                    return row.street + ' ' + row.houseNo + (row.houseLetter != null ? row.houseLetter : "") + (row.apartmentNo != null ? " - " + row.apartmentNo : "") + ', ' + row.city;
                },
                width: "20%"
            },
            { data: 'hour', width: "5%" },
            {
                data: 'status',
                width: "10%",
                render: function (data) {
                    switch (data) {
                        case -1:
                            return 'Atšauktas';
                        case 0:
                            return 'Laukiama patvirtinimo';
                        case 1:
                            return 'Patvirtintas';
                        case 2:
                            return 'Užbaigtas';
                        default:
                            return '';
                    }
                }
            }
        ]
    });
}
