jQuery("#contrcard").jqGrid({
    datatype: "local",
    height: 250,
    colNames: ['Атрибут', 'Значение'],
    colModel: [
        { name: 'atr', index: 'atr', width: 120, sorttype: "string" },
        { name: 'val', index: 'val', width: 250, sorttype: "string" },
    ],
    multiselect: true,
    caption: "Карточка контрагента",
});
jQuery("#contrcard").jqGrid('addRowData', 1, { atr: "Наименование", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 2, { atr: "ИНН", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 3, { atr: "КПП", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 4, { atr: "р/с", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 5, { atr: "Банк", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 6, { atr: "город банка", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 7, { atr: "к/с", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 8, { atr: "БИК", val: "" });
jQuery("#contrcard").jqGrid('addRowData', 9, { atr: "Комментарии", val: "" });