
/*
* 改变显示页面
*/
function ChangePage(flag)
{
    //固定工资录入
    if (flag == "1")
    {
        document.getElementById("salaryPage").src = "InputSalaryFixed.aspx?ModuleID=2011702";
    }
    //浮动工资录入
    else if (flag == "2")
    {
        document.getElementById("salaryPage").src = "InputCompanyRoyalty.aspx?ModuleID=2011702";
    }
    //社会保险录入
    else if (flag == "3")
    {
        document.getElementById("salaryPage").src = "InputInsuEmployee.aspx?ModuleID=2011702";
    }
    //个人所得税
    else if (flag == "4")
    {
        document.getElementById("salaryPage").src = "InputPersonTrueIncomeTax.aspx?ModuleID=2011702";
    }
    //公司提成录入
    else if(flag=="11")
    {
        document.getElementById("salaryPage").src = "InputCompanyRoyalty.aspx?ModuleID=2011702";
    }
    else if(flag=="13")
    {
        document.getElementById("salaryPage").src = "InputPerformanceRoyalty.aspx?ModuleID=2011702";
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