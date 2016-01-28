var jsondata1;
var jsondata2;
var jsondata3;
var jsondata4;
var ProductAlarmCount = 0;
var ContactDeferCount = 0;
var ProviderContactCount = 0;
var ContractAlarmCount = 0;
var BeiWangLuCount = 0;
var CanHuiTongZhiCount = 0;

var feiyongCount = 0;
var returnResult = 0;
var sessionSection = "";
$(document).ready(function() {

//GetCustTel();//来电显示

    var url = document.location.href.toLowerCase();
    if (url.indexOf("/(s(") != -1) {
        var sidx = url.indexOf("/(s(") + 1;
        var eidx = url.indexOf("))") + 2;
        //alert(sidx+":"+eidx);
        url = document.location.href;

        sessionSection = url.substring(sidx, eidx);
        sessionSection += "/";
    }


});

function SearchTaskList() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Personal/Task/TaskList.ashx?TaskType=1', //目标地址
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            jsondata1 = eval(msg.data);
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}


function SearchDeskFlow() {

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/DeskTop.ashx', //目标地址
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            jsondata2 = msg.data.list;
            returnResult++;
            isAllRetrun();

        },
        error: function(res) {

        }
    });
}


function getBillTypeItem(i, j) {
    for (var mem in billTypes) {
        if (billTypes[mem].v == j && billTypes[mem].p == i) {
            return billTypes[mem];
            break;
        }
    }

    return null;
}

function SearchUnreadMessage() {

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Personal/MessageBox/InputBox.ashx', //目标地址
        data: "action=desktoploaddata",
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            jsondata4 = msg.data.list;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}

function SearchStorageProductAlarm() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Office/StorageManager/StorageProductAlarm.ashx?orderby=&pageCount=10000&pageIndex=1&BarCode=&sltAlarmType=0', //目标地址
        data: "action=desktoploaddata",
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            ProductAlarmCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}


function SearchContactDefer() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Office/CustManager/ContactDeferInfo.ashx', //目标地址
        data: "action=desktoploaddata&orderby=&pageCount=10000&pageIndex=1",
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            ContactDeferCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}


function SearchProviderContactCount() {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Office/PurchaseManager/ProviderContactHistoryWarning.ashx', //目标地址
        data: "action=desktoploaddata&orderby=&pageCount=10000&pageIndex=1",
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            ProviderContactCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}




function SearchBeiWangLu() {
    var sign = 'sign=GetBeiWangLuCount';
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Main.ashx', //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            BeiWangLuCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}


function SearchCanHuiTongZhi() {
    var sign = 'sign=SearchCanHuiTongZhi';
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Main.ashx', //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            CanHuiTongZhiCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}
function SearchFeiyongShezhi() {
    var sign = 'sign=SearchFeiyongShezhi';
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Main.ashx', //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
        feiyongCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}





function SearchContractAlarm() {
    var sign = 'sign=GetContractCount';
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: 'Handler/Main.ashx', //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前 
        success: function(msg) {
            ContractAlarmCount = msg.totalCount;
            returnResult++;
            isAllRetrun();
        },
        error: function(res) {

        }
    });
}


function getBillTypeItem(i, j) {
    for (var mem in billTypes) {
        if (billTypes[mem].v == j && billTypes[mem].p == i) {
            return billTypes[mem];
            break;
        }
    }

    return null;
}


function getStatusName(i) {
    if (i == "1")
        return "待我审批";
    if (i == "2")
        return "审批中";
    if (i == "3")
        return "审批通过";
    if (i == "4")
        return "审批不通过";
    if (i == "5")
        return "撤销审批";
    return "未知";
}


function isAllRetrun() {
    if (returnResult == 10) {
        ShowStorageProductAlarm();
        ShowContactDefer();
        ShowProviderContact();
        ShowSearchTaskList();
        ShowSearchDeskFlow();
        ShowSearchContractAlarm();
        ShowSearchUnreadMessage();
        ShowBeiWangLuCount();
        ShowCanHuiTongZhi();
        ShowFeiyong();
      

    } else
        return;
}

var TableStr = "<table   id=\"mainindex\" style=\" height:100%;width:90%;background-color:White\"  >";

function GetAllDestTopList() {
    TableStr = "<table   id=\"mainindex\" style=\" height:100%;width:90%;background-color:White\"  >";
    SearchTaskList();
    SearchDeskFlow();
    SearchUnreadMessage();
    SearchStorageProductAlarm();
    SearchContactDefer();
    SearchProviderContactCount();
    SearchContractAlarm();

    SearchBeiWangLu();
    SearchCanHuiTongZhi();
    SearchFeiyongShezhi();
    
}


function ShowStorageProductAlarm() {
    try {
        if (ProductAlarmCount + "" != "undefined") {
            TableStr += "<tr><td style='FONT-SIZE: 12px;' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;库存限量报警：<a target='Main' href='Pages/Office/StorageManager/StorageProductAlarm.aspx?ModuleID=2051902'><font  color='red'>" + ProductAlarmCount + "</font>条 </a></td></tr>";
        }
    } catch (ee) { }
    //     for(var listindex = 0 ;listindex<jsondata2.length;listindex++){
    //                               if(listindex == 1)
    //                                      break;
    //                               TableStr += "<tr><td width='30%' style='FONT-SIZE: 12px;overflow:hidden;' ><a href='#' >"+ jsondata2[listindex].FlowName +"</a></td>";
    //                            //   TableStr += "<td  style='overflow:hidden'>"+jsondata2[listindex].Title+"</td></tr>";   
    //                    }  
}


function ShowContactDefer() {
    try {
        TableStr += "<tr><td style='FONT-SIZE: 12px;' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;客户联络延期告警：<a target='Main' href='Pages/Office/CustManager/ContactDefer_Info.aspx?ModuleID=2021303'>  <font  color='red'>" + ContactDeferCount + "</font>条 </a></td></tr>";
    } catch (ee) { }
    //     for(var listindex = 0 ;listindex<jsondata2.length;listindex++){
    //                               if(listindex == 1)
    //                                      break;
    //                               TableStr += "<tr><td width='30%' style='FONT-SIZE: 12px;overflow:hidden;' ><a href='#' >"+ jsondata2[listindex].FlowName +"</a></td>";
    //                            //   TableStr += "<td  style='overflow:hidden'>"+jsondata2[listindex].Title+"</td></tr>";   
    //                    }  
}


function ShowProviderContact() {
    try {
        TableStr += "<tr><td style='FONT-SIZE: 12px;' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;供应商联络延期告警：<a target='Main' href='Pages/Office/PurchaseManager/ProviderContactHistoryWarning.aspx?ModuleID=2041203'><font  color='red'>" + ProviderContactCount + "</font>条</a></td></tr>";
    } catch (ee) { }
    //     for(var listindex = 0 ;listindex<jsondata2.length;listindex++){
    //                               if(listindex == 1)
    //                                      break;
    //                               TableStr += "<tr><td width='30%' style='FONT-SIZE: 12px;overflow:hidden;' ><a href='#' >"+ jsondata2[listindex].FlowName +"</a></td>";
    //                            //   TableStr += "<td  style='overflow:hidden'>"+jsondata2[listindex].Title+"</td></tr>";   
    //                    }  
}

function ShowSearchDeskFlow() {
    try {
        TableStr += "<tr><td style='FONT-SIZE: 12px;' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;待我审批的流程：<a target='Main' href='Pages/Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=10713'><font  color='red'>" + jsondata2.length + "</font>条</a></td></tr>";
    } catch (ee) { }

}

function ShowSearchTaskList() {
    try {
        TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的待办任务：<a target='Main' href='Pages/Personal/Task/TaskList.aspx?ModuleID=10112'><font  color='red'>" + jsondata1.length + "</font>条</a></td></tr>";
    } catch (ee) { }

}

function ShowSearchContractAlarm() {
    try {
        TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;即将到期的劳动合同：<a target='Main' href='Pages/Office/HumanManager/EmployeeContract_Info.aspx?ModuleID=2011208'><font  color='red'>" + ContractAlarmCount + "</font>条</a></td></tr>";
    } catch (ee) { }
}

function ShowBeiWangLuCount() {
    try {
        TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的备忘录：<a target='Main' href='pages/personal/memo/memoList.aspx?ModuleID=10312'><font  color='red'>" + BeiWangLuCount + "</font>条</a></td></tr>";
    } catch (ee) { }
}

function ShowCanHuiTongZhi() {
    try {
        TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的参会通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2001504'><font  color='red'>" + CanHuiTongZhiCount + "</font>条</a></td></tr>";
    } catch (ee) { }
}



function ShowFeiyong() {
   
    try {
        TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;费用报销延期预警：<a target='Main' href='Pages/Personal/Expenses/ExpensesList.aspx?ModuleID=10811'><font  color='red'>" + feiyongCount + "</font>条</a></td></tr>";
    } catch (ee) { }
    TableStr += "</table>";

    var alllistcount = 0;
    try {
        if (jsondata1.length != null)
            alllistcount += jsondata1.length;
    } catch (ee) { }
    try {
        if (jsondata2.length != null)
            alllistcount += jsondata2.length;
    } catch (ee) { }
    try {
        if (jsondata4.length != null)
            alllistcount += jsondata4.length;
    } catch (ee) { }

    try {
        if (ProductAlarmCount != null)
            alllistcount += ProductAlarmCount;
    } catch (ee) { }
    try {
        if (ContactDeferCount != null)
            alllistcount += ContactDeferCount;
    } catch (ee) { }
    try {
        if (ProviderContactCount != null)
            alllistcount += ProviderContactCount;
    } catch (ee) { }

    try {
        if (ContractAlarmCount != null)
            alllistcount += ContractAlarmCount;
    } catch (ee) { }


    try {
        if (BeiWangLuCount != null)
            alllistcount += BeiWangLuCount;
    } catch (ee) { }

    try {
        if (CanHuiTongZhiCount != null)
            alllistcount += CanHuiTongZhiCount;
    } catch (ee) { }

    try {
        if (feiyongCount != null)
            alllistcount += feiyongCount;
    } catch (ee) { }


    
  
    if (alllistcount > 0) {

        document.getElementById("checkTask").src = "images/light.gif";
        document.getElementById("checkTask").title = "您有新的待办任务，请尽快解决";
      //  document.getElementById("checkTask").attachEvent("onclick", showMe);
        $("#checkTask").bind("click", showMe);
        
        //             MSG.show();
        $.messager.lays(300, 230);

        //   var SDDF = TableStr;
        $.messager.show(' 您有新的待办事项,请尽快处理！ ', TableStr);
    }
    else {
        document.getElementById("checkTask").title = "暂无待办事项！";

        document.getElementById("checkTask").src = "images/light_un.gif";
      //  document.getElementById("checkTask").attachEvent("onclick", fnEmty);
        $("#checkTask").bind("click", fnEmty);


    }
    returnResult = 0;
}

var MSG;
function ShowSearchUnreadMessage() {
    try {
        if (typeof (jsondata4) != undefined) {
            //            if (jsondata4.length>0)
            TableStr += "<tr><td style='FONT-SIZE: 12px;' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的未读短信：<a target='Main' href='pages/personal/messagebox/UnReadedInfo.aspx?ModuleID=10613'><font  color='red'>" + jsondata4.length + "</font>条</a></td></tr>";
        }


    } catch (ee) { }



}

function showMe() {


    $.messager.lays(300, 220);
    $.messager.show(' 您有新的待办事项,请尽快处理！ ', TableStr, 0);
}

function fnEmty()
{ }
     





