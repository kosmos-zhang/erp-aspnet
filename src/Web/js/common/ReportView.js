// JScript 文件

// JScript 文件
//要打印ReportView报表的内容，只需要引用本文件，然后调用PrintReportView()函数即可。
//例如：在某按钮的点击事件中包括代码，onclick="PrintReportView(window,'ReportViewerYsqd');"

//得到ReportView控件生成的客户端代码的报表内容区的FRAME对象
//参数：objWindow——包含ReportView控件的window对象
//      strReportViewerId——需要被打印的ReportViewer控件的ID
//返回：得到的报表内容区FRAME对象；如果获取失败，返回null。
function GetReportViewContentFrame(objWindow,strReportViewerId)
{
    var frmContent=null;    //报表内容区对象的FRAME对象
    var strFrameId="ReportFrame" + strReportViewerId ;  //asp.net自动生成的iframe 的id为：ReportFrame+报表控件id
    try
    {
        frmContent=window.frames[strFrameId].frames["report"];  //报表内容框架的id为report
    }
    catch(e)
    {
    }
    return frmContent;
}

//打印ReportView控件中的报表内容
//参数：objWindow——包含ReportView控件的window对象
//      strReportViewerId——需要被打印的ReportViewer控件的ID
//返回：（无）
function PrintReportView(objWindow,strReportViewerId)
{
    var frmContent=GetReportViewContentFrame(objWindow,strReportViewerId);
    if(frmContent!=null && frmContent!=undefined)
    {
        frmContent.focus();
        frmContent.print();
    }
    else
    {
        alert("在获取报表内容时失败，无法通过程序打印。如果要手工打印，请鼠标右键点击报表内容区域，然后选择菜单中的打印项。");
    }
}