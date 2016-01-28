function DoModify(){
    submitFlag = false;
    var select = GetSelectValue();
    if (select == null || select == "")
    {
        alert("请选择要修改的记录。");
        return;
    }
    row = select.split(",");
    if (row.length > 1)
    {
        alert("您选择的修改记录多于一条。");
        return;
    }
    var table = document.getElementById("xgoss_tb");
    rowNo = parseInt(row) + 1;
    CompanyNo = table.rows(rowNo).cells(2).innerText;
    window.open("Company_Edit.aspx?CompanyNo=" + CompanyNo,"_mainFrame");
}