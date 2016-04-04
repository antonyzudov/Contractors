jQuery("#mainlist").jqGrid({
    url: "api/contractors",
    datatype: "json",
    colNames: ['№', 'Наименование', 'ИНН'],
    colModel: [
        { name: 'id', index: 'id', width: 20, sorttype: "int" },
        { name: 'name', index: 'name', width: 350, sorttype: "string" },
        { name: 'INN', index: 'INN', width: 350, align: "right", sorttype: "string" }
    ],
    rowNum: 0,
    multiselect: false,
    height: 'auto',
    caption: "Текущий список контрагентов",
    ondblClickRow: function (id) {
        $(document).ready(function () {
            $.getJSON("api/contractors/" + id)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        jQuery("#contrcard").jqGrid('setCell', 1, 2, item.name);
                        jQuery("#contrcard").jqGrid('setCell', 2, 2, item.INN);
                        jQuery("#contrcard").jqGrid('setCell', 3, 2, item.KPP);
                        jQuery("#contrcard").jqGrid('setCell', 4, 2, item.settlement_account);
                        jQuery("#contrcard").jqGrid('setCell', 5, 2, item.bank);
                        jQuery("#contrcard").jqGrid('setCell', 6, 2, item.city);
                        jQuery("#contrcard").jqGrid('setCell', 7, 2, item.corr_account);
                        jQuery("#contrcard").jqGrid('setCell', 8, 2, item.BIK);
                        jQuery("#contrcard").jqGrid('setCell', 9, 2, item.full_name);
                    });
                });
        });
    }
});