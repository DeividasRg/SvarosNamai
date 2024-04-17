var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/GetAll" },
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.11.5/i18n/Lithuanian.json",
            "paginate": {
                "first": "Pirmas",
                "last": "Paskutinis",
                "previous": "Praeitas",
                "next": "Kitas"
            },
            "search": "Paieška",
            "info": "Rodo _START_ iki _END_ iš _TOTAL_ rezultatų",
            "infoEmpty": "Rodo 0 iki 0 iš 0 rezultatų",
            "infoFiltered": "(išfiltruota iš _MAX_ viso įrašų)",
            "lengthMenu": "Rodyti _MENU_ rezultatus" // Translation for "Show N entries"
        },
        "columns": [
            { data: 'orderId', width: "3%", title: "Užsakymo numeris" },
            {
                data: function (row) {
                    return row.name + ' ' + row.lastName;
                },
                width: "20%",
                title: "Vardas Pavardė"
            },
            {
                data: function (row) {
                    return row.street + ' ' + row.houseNo + (row.houseLetter != null ? row.houseLetter : "") + (row.apartmentNo != null ? " - " + row.apartmentNo : "") + ', ' + row.city;
                },
                width: "30%",
                title: "Adresas"
            },
            {
                data: 'hour',
                width: "10%",
                title: "Laikas",
                render: function (data) {
                    return data + ' valanda';
                }
            },
            {
                data: 'status',
                width: "30%",
                title: "Statusas",
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
            },
            {
                data: 'orderId',
                title: "Funkcijos",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/order/Details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                    </div>`
                },
                width: "10%"
            },
            { data: 'orderId', width: "3%", title: "Užsakymo numeris" },
        ]
    });
}
