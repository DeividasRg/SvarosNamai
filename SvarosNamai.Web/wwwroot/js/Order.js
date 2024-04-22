var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/GetAll" },
        "language": {
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
                    if (row.isCompany) {
                        return row.companyName;
                    } else {
                        return row.name + ' ' + row.lastName;
                    }
                },
                width: "20%",
                title: "Vardas Pavardė / Įmonės pavadinimas"
            },
            {
                data: function (row) {
                    if (row.isCompany) {
                        return row.companyNumber;
                    } else {
                        return '';
                    }
                },
                width: "20%",
                title: "Įmonės kodas"
            },
            {
                data: function (row) {
                    return row.street + ' ' + row.houseNo + (row.houseLetter != null ? row.houseLetter : "") + (row.apartmentNo != null ? " - " + row.apartmentNo : "") + ', ' + row.city;
                },
                width: "30%",
                title: "Adresas"
            },
            { data: 'date', width: "10%", title: "Data" },
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
                width: "15%",
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
            { data: 'creationDate', width: "3%", title: "Pateikimo laikas" },
            {
                data: 'isCompany',
                width: "3%",
                title: "Ar įmonė?",
                render: function (data) {
                    return data ? 'Taip' : 'Ne';
                },
                orderable: false
            },
            {
                data: 'orderId',
                title: "Funkcijos",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/order/Details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                            </div>`
                },
                width: "7%",
                orderable: false
            }
        ],
        "order": [[5, "desc"]] // Sort by the 'creationDate' column in descending order (column index is zero-based)
    });

    // Add filter to "Ar Įmonė?" column
    addFilter();
}

function addFilter() {
    var table = $('#tblData').DataTable();

    // Create select element for filtering
    var select = $('<select><option value="">Visi</option><option value="Taip">Taip</option><option value="Ne">Ne</option></select>')
        .appendTo($('#tblData').find('thead tr:eq(0) th:eq(8)'))
        .on('change', function () {
            var val = $.fn.dataTable.util.escapeRegex(
                $(this).val()
            );

            table.columns(8).search(val ? '^' + val + '$' : '', true, false).draw();
        });
}