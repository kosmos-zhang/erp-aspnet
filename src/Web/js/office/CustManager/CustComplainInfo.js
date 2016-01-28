    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    var ifdel="0";//是否删除
    
$(document).ready(function()
{
    requestobj = GetRequest(); 
    var CustName = requestobj['CustName'];
    var CustID = requestobj['CustID'];
    var ComplainType = requestobj['ComplainType'];
    var Critical = requestobj['Critical'];
    var ComplainBegin = requestobj['ComplainBegin'];
    var ComplainEnd = requestobj['ComplainEnd'];
    var Title = requestobj['Title'];
    var CustLinkMan = requestobj['CustLinkMan'];
    var EmplNameL = requestobj['EmplNameL'];
    var State = requestobj['State'];

    if(typeof(CustName)!="undefined")
    { 
       $("#txtUcCustName").attr("value",CustName);//客户简称
       $("#hfCustID").attr("value",CustID);
       $("#ddlComplainType").attr("value",ComplainType);
       $("#seleCritical").attr("value",Critical);
       $("#txtComplainBegin").attr("value",ComplainBegin);
       $("#txtComplainEnd").attr("value",ComplainEnd);
       $("#txtTitle").attr("value",Title);
       $("#txtCustLinkMan").attr("value",CustLinkMan);
       $("#txtEmplNameL").attr("value",EmplNameL);
       $("#seleState").attr("value",State);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       SearchComplainData(currentPageIndex);
    }
    
});

function TurnToPage(pageIndex)
{   
    document.getElementById("checkall").checked = false;
    ifdel="0";
       currentPageIndex = pageIndex;
       var CustName =document.getElementById("hfCustID").value; //检索条件 客户简称
       var ComplainType =document.getElementById("ddlComplainType").value; //检索条件 投诉类型
       var Critical =document.getElementById("seleCritical").value;//紧急程度
       var ComplainBegin = document.getElementById("txtComplainBegin").value;//服务开始时间
       var ComplainEnd = document.getElementById("txtComplainEnd").value;//服务结束时间
       var Title = document.getElementById("txtTitle").value;//服务主题
       var CustLinkMan = document.getElementById("txtCustLinkMan").value;//客户联系人
       var DestClerk = document.getElementById("txtEmplNameL").value;//接待人
       var State = document.getElementById("seleState").value;//状态
       //*******************************修改1 王乾睿*******************************************************
 
       var ComplainTypeW = "";
       try{
          ComplainTypeW =   document.getElementById("ComplainType").value;
       }catch(ee){
      
       }
      var ComplainPerson = "";
       try{
          ComplainPerson =   document.getElementById("ComplainPerson").value;
       }catch(ee){ }
     var CustNameW ="";
     try{
          CustNameW =   document.getElementById("CustNameW").value;
       }catch(ee){ }
      var TimeIndex ="";
     try{
          TimeIndex =   document.getElementById("TimeIndex").value;
       }catch(ee){ }
     var GroupBy ="";
     try{
          GroupBy =   document.getElementById("GroupBy").value;
       }catch(ee){ }
       //**************************************end1  修改*****************************************************************
       $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/ComplainInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+
                "&CustName="+reescape(CustName)+
                "&ComplainType="+reescape(ComplainType)+
                "&Critical="+reescape(Critical)+
                "&ComplainBegin="+reescape(ComplainBegin)+
                "&ComplainEnd="+reescape(ComplainEnd)+
                "&Title="+reescape(Title)+
                "&CustLinkMan="+reescape(CustLinkMan)+
                "&DestClerk="+reescape(DestClerk)+
                "&State="+reescape(State)+
                "&ComplainTypeW="+reescape(ComplainTypeW) +
                "&ComplainPerson="+reescape(ComplainPerson) +
                "&CustNameW="+reescape(CustNameW) +
                "&TimeIndex="+reescape(TimeIndex) +
                "&GroupBy="+reescape(GroupBy) , //数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    var Critical,State;
   //*************************************修改2  王乾睿***************************************************                 
                    if(  (ComplainTypeW !="" || ComplainPerson!="" || GroupBy !="" || TimeIndex !=  "")   ){
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")   
                      {                   
                          switch(item.Critical)
                          {
                                case "1":
                                Critical = "宽松";
                                break;
                                
                                case "2":
                                Critical = "一般";
                                break;
                                
                                case "3":
                                Critical = "较急";
                                break;
                                
                                case "4":
                                Critical = "紧急";
                                break;
                                
                                case "5":
                                Critical = "特急";
                                break;
                                
                                default:
                                Critical = "";
                          }
                          
                          switch(item.state)
                          {
                                case "1":
                                State = "处理中";
                                break;
                                
                                case "2":
                                State = "未处理";
                                break;
                                
                                case "3":
                                State = "已处理";
                                break;
                                                            
                                default:
                                State = "";
                                break;
                          }
                          
                            $("<tr class='newrow'></tr>").append(
                             "<td height='22' align='center'>"+ item.ComplainNo + "</td>"+
                             "<td height='22' align='center'>" + item.title + "</td>"+ 
                             "<td height='22' align='center'>"+ item.custNam + "</td>"+
                             "<td height='22' align='center'>" + item.ComplainDate + "</td>"+                        
                            
                            "<td height='22' align='center'>" + item.typename + "</td>"+
                             "<td height='22' align='center'>"+ Critical +"</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                           
                            "<td height='22' align='center'>"+ State +"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
                    }else{
       //*************************************end修改2  王乾睿***************************************************              
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")   
                      {                   
                          switch(item.Critical)
                          {
                                case "1":
                                Critical = "宽松";
                                break;
                                
                                case "2":
                                Critical = "一般";
                                break;
                                
                                case "3":
                                Critical = "较急";
                                break;
                                
                                case "4":
                                Critical = "紧急";
                                break;
                                
                                case "5":
                                Critical = "特急";
                                break;
                                
                                default:
                                Critical = "";
                          }
                          
                          switch(item.state)
                          {
                                case "1":
                                State = "处理中";
                                break;
                                
                                case "2":
                                State = "未处理";
                                break;
                                
                                case "3":
                                State = "已处理";
                                break;
                                                            
                                default:
                                State = "";
                                break;
                          }
                          
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')   value="+item.id+"  type='checkbox'/>"+"</td>"+
                             "<td height='22' align='center'><a href='#' onclick=SelectComplain('"+item.id+"')>" + item.ComplainNo + "</a></td>"+
                             "<td height='22' align='center'>" + item.title + "</td>"+ 
                             "<td height='22' align='center'><a href='#' onclick=SelectCust('"+item.custid+"','"+item.CustNo+"','"+item.CustBig+"','"+item.CanViewUser+"','"+item.Manager+"','"+item.Creator+"')>" + item.custNam + "</a></td>"+
                             "<td height='22' align='center'>" + item.ComplainDate + "</td>"+                        
                            
                            "<td height='22' align='center'>" + item.typename + "</td>"+
                             "<td height='22' align='center'>"+ Critical +"</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                           
                            "<td height='22' align='center'>"+ State +"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
 //**************** ************ ***********修改3  王乾睿***************************************************                   
                   }
                   
  //*************************************end修改3  王乾睿***************************************************                  
                   
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
//检索
function SearchComplainData(aa)
{
    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
    search="1";
    TurnToPage(aa);
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
     if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
 //排序
function OrderBy(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
     }
    ifdel = "0";
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
}

function CreateComplain()
{
    SelectComplain(-1);
}

//转到客户投诉信息查看
function SelectComplain(Complainid)
{
    var CustName = document.getElementById("txtUcCustName").value;
    var CustID = document.getElementById("hfCustID").value;
    var ComplainType = document.getElementById("ddlComplainType").value;
    var Critical = document.getElementById("seleCritical").value;
    var ComplainBegin = document.getElementById("txtComplainBegin").value;
    var ComplainEnd = document.getElementById("txtComplainEnd").value;
    var Title = document.getElementById("txtTitle").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var EmplNameL = document.getElementById("txtEmplNameL").value;
    var State = document.getElementById("seleState").value;
    
    window.location.href='CustComplain_Add.aspx?Complainid='+Complainid+'&Pages=CustComplain_Info.aspx&CustName='+CustName+'&ComplainType='+ComplainType+
        '&Critical='+Critical+'&ComplainBegin='+ComplainBegin+'&ComplainEnd='+ComplainEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan+'&CustID='+CustID+
        '&EmplNameL='+EmplNameL+'&State='+State+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021701';
}
//根据客户ID转到客户信息查看
function SelectCust(custid,custno,custbig,canuser,manager,creator)
{
    var j = 0;
    var UserId = document.getElementById("hiddUserId").value;
    if(UserId != manager && UserId != creator && canuser != ",,")
    {
        var str= new Array();
        str = canuser.split(",");
        for(i=0;i<str.length;i++)
        {
            if(str[i] == UserId)
            {
                j++;
            }        
        }
    }
    else
    {
        j++;
    }
    
    if(j == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","对不起，您没有浏览此客户信息的权限！");
        return;
    }
    
    var CustName  = document.getElementById("txtUcCustName").value;
    var CustID = document.getElementById("hfCustID").value;
    var ComplainType = document.getElementById("ddlComplainType").value;
    var Critical = document.getElementById("seleCritical").value;
    var ComplainBegin = document.getElementById("txtComplainBegin").value;
    var ComplainEnd = document.getElementById("txtComplainEnd").value;
    var Title = document.getElementById("txtTitle").value;
    var CustLinkMan = document.getElementById("txtCustLinkMan").value;
    var EmplNameL = document.getElementById("txtEmplNameL").value;
    var State = document.getElementById("seleState").value;
    
    window.location.href='Cust_Add.aspx?custid='+custid+'&custno='+custno+'&custbig='+custbig+'&Pages=CustComplain_Info.aspx&CustName='+CustName+'&ComplainType='+ComplainType+'&CustID='+CustID+
        '&Critical='+Critical+'&ComplainBegin='+ComplainBegin+'&ComplainEnd='+ComplainEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan+
        '&EmplNameL='+EmplNameL+'&State='+State+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021103';
}
//删除客户投诉
function DelComplainInfo()
{
    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        
        var custids = ck2.substring(0,ck2.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条投诉信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/ComplainDel.ashx",
                  data:"allcustid="+reescape(custids),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==1) 
                    { 
                        //成功
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                    }
                    else
                    {
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    } 
                } 
               });
        }
    }
}
//是否可以导出
function IfExp() {
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先检索出数据，才可以导出！");
        return false;
    }
    
    document.getElementById("hiddCustID").value = $("#hfCustID").val(); //客户ID
    return true;
}

//主表单验证
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
        
    var ComplainBegin = document.getElementById("txtComplainBegin").value;//投诉开始时间
    var ComplainEnd = document.getElementById("txtComplainEnd").value;//结束时间 
    var RetVal=CheckSpecialWords();
    
    var ddlComplainType = document.getElementById('ddlComplainType').value;//投诉类型
    if(ddlComplainType == "")
    {
        isFlag = false;
        fieldText = fieldText + "投诉类型|";
	    msgText = msgText + "请首先配置投诉类型|";
    }
    
    if(ComplainBegin.length>0 && isDate(ComplainBegin) == false)
    {
        isFlag = false;
        fieldText = fieldText + "开始时间|";
	    msgText = msgText + "开始时间格式不正确|";
    }
    if(ComplainEnd.length>0 && isDate(ComplainEnd) == false)
    {
        isFlag = false;
        fieldText = fieldText + "结束时间|";
	    msgText = msgText + "结束时间格式不正确|";
    }
    if(!compareDate(ComplainBegin,ComplainEnd))
    {
        isFlag = false;
        fieldText = fieldText + "开始日期|";
	    msgText = msgText + "开始日期不能大于结束日期|";
    }
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}