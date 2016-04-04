jQuery("#clcontrcard").jqGrid({
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
jQuery("#clcontrcard").jqGrid('addRowData', 1, { atr: "Наименование", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 2, { atr: "ИНН", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 3, { atr: "КПП", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 4, { atr: "р/с", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 5, { atr: "Банк", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 6, { atr: "город банка", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 7, { atr: "к/с", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 8, { atr: "БИК", val: "" });
jQuery("#clcontrcard").jqGrid('addRowData', 9, { atr: "Комментарии", val: "" });