
var submitFlag = true;
function DoAdd(){
    submitFlag = false;
    window.location = "Company_Edit.aspx?Action=Add";
    
    
}
function DoSearch()
{
  submitFlag = true;
}
function DoLicense()
{
    submitFlag = false;
    var select = GetSelectValue();
    if (select == null || select == "")
    {
        alert("请选择需要授权的记录。");

        return;
    }
    row = select.split(",");
    if (row.length > 1)
    {
        alert("请选择一条记录进行授权。");
        return;
    }
    CompanyCD = document.getElementById("CompanyCD_" + row).value;
    

   // window.open("RoleInfo_Edit.aspx?action=Edit&RoleID=" + RoleID,"_mainFrame");
   window.location = "CompanyModule_Modify.aspx?CompanyCD="+CompanyCD;
}

function DoDelete(){
    submitFlag = false;
    var select = GetSelectValue();
    if (select == null || select == "")
    {
        alert("请选择要删除的记录。");
        return;
    }
    row = select.split(",");
    var table = document.getElementById("xgoss_tb");
    selectCount = row.length;
    var CompanyCD = "";
    
    for (var i = 0; i < selectCount; i++)
    {
        CompanyCD += document.getElementById("CompanyCD_" + row[i]).value + ",";
    }    
    document.getElementById("hidDelete").value = CompanyCD;    
    submitFlag = confirm("确认删除！");
}


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
        alert("您选择的修改记录多一条。");
        return;
    }
    CompanyCD = document.getElementById("CompanyCD_" + row).value;
    

   // window.open("RoleInfo_Edit.aspx?action=Edit&RoleID=" + RoleID,"_mainFrame");
   window.location = "Company_Edit.aspx?Action=Edit&CompanyCD="+CompanyCD;
    
}


