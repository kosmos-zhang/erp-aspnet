
var submitFlag = false;

function DoSearch(){
    var openDate = document.getElementById("txtOpenDate").value;
    if (!IsRightDate(openDate))
    {
        alert("请输入正确的生效日期。");
        return;
    }
    var closeDate = document.getElementById("txtCloseDate").value;
    if (!IsRightDate(closeDate))
    {
        alert("请输入正确的失效日期。");
        return;
    }
    if (openDate != "" && closeDate != "")
    {
        if (CompareDate(openDate, closeDate) == "1")
        {
            alert("您输入的生效日期晚于失效日期。");
            return;
        }
    }
    submitFlag = true;
}

function DoModify(flag){
    submitFlag = false;
    if ("1" == flag) 
    {
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
        companyCD = table.rows(rowNo).cells(1).innerText;
        window.open("CompanyOpenServ_Modify.aspx?CompanyCD=" + companyCD,"_mainFrame");
    }
    else 
    window.open("CompanyOpenServ_Modify.aspx","_mainFrame");
}

function DoDelete(){
    submitFlag = false;
    var select = GetSelectValue();
    if (select == null || select == "")
    {
        alert("请选择要修改的记录。");
        return;
    }
    row = select.split(",");
    var table = document.getElementById("xgoss_tb");
    selectCount = row.length;
    var companyCD = "";
    
    for (var i = 0; i < selectCount; i++)
    {
        rowNo = parseInt(row[i]) + 1;
        companyCD += "'" + table.rows(rowNo).cells(1).innerText + "',";
    }    
    document.getElementById("hidDelete").value = companyCD;
    
    submitFlag = confirm("确认删除！");
}