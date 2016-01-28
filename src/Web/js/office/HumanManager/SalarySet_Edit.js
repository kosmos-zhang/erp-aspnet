/*
* 改变显示页面
*/
function ChangePage(flag)
{
    //工资项设置
    if (flag == "1")
    {
        document.getElementById("salaryPage").src = "SalaryItem.aspx?ModuleID=2011701";
    }
    //浮动工资设置
    else if (flag == "2")
    {
        document.getElementById("salaryPage").src = "SalaryCompanyRoyaltySet.aspx?ModuleID=2011701";
    }
    //计时工资设置
    else if (flag == "3")
    {
        document.getElementById("salaryPage").src = "SalaryTime.aspx?ModuleID=2011701";
    }
    //提成工资设置
    else if (flag == "4")
    {
        document.getElementById("salaryPage").src = "SalaryCommission.aspx?ModuleID=2011701";
    }
    //社会保险设置
    else if (flag == "5")
    {
        document.getElementById("salaryPage").src = "SalaryInsuSocial.aspx?ModuleID=2011701";
    }
    //工资标准设置
    else if (flag == "6")
    {
        document.getElementById("salaryPage").src = "SalaryStandard.aspx?ModuleID=2011701";
    }
        //个人所得税设置
    else if (flag == "7")
    {
        document.getElementById("salaryPage").src = "SalaryInsuPersonaIncomeTax.aspx?ModuleID=2011701";
    }
    else if(flag=="8")
    {
        document.getElementById("salaryPage").src = "SalaryCompanyRoyaltySet.aspx?ModuleID=2011701";
    }
    else if(flag=="9")
    {
        document.getElementById("salaryPage").src = "SalaryPerformanceRoyaltySet.aspx?ModuleID=2011701";
    } 
    else if(flag=="10")
    {
        document.getElementById("salaryPage").src = "SalaryEmployeeStructureSet.aspx?ModuleID=2011701";
    }
    else if(flag=="11")
    {
        document.getElementById("salaryPage").src = "InputCompanyRoyalty.aspx?ModuleID=2011701";
    }
    else if(flag=="12")
    {
        document.getElementById("salaryPage").src = "PerformanceRoyaltyBase.aspx?ModuleID=2011701";
    }
    else if(flag=="13")
    {
        document.getElementById("salaryPage").src = "InputPerformanceRoyalty.aspx?ModuleID=2011701";
    }
}

/*
* 
*/
function ReSetIFrameHeight()
{
    availHeight = window.screen.availHeight;
    document.all("salaryPage").style.height = availHeight - 164;
    
}