<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UintInfoUC.ascx.cs" Inherits="UserControl_UintInfoUC" %>
<div id="layoutUnit">
    <!--提示信息弹出详情start-->
    <div id="divUnit" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; z-index: 1001; position: absolute; display: none;
        top: 50%; left: 70%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closedivUnit()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Unit" bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">单位<span id="oC2" class="orderTip"></span></div></th>
                   
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pageUnitcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1Unit_PagerUnit" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divUnitpage">
                                    <input name="text" type="text" id="Text2Unit" style="display: none" />
                                    <span id="pageDataList1Unit_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowUnitPageCount" />
                                    条 转到第
                                    <input name="text" type="text" id="ToUnitPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowUnitPageCount').val(),$('#ToProductPageUnit').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script language="javascript">
var popUnitObj=new Object();
popUnitObj.InputObj = null;


popUnitObj.ShowList=function(objInput)
{
    popUnitObj.InputObj = objInput;
    document.getElementById('divUnit').style.display='block';
    TurnToPageUnit(currentPageIndexUnit);
}
  
    var pageCountUnit = 10;//每页计数
    var totalRecordUnit = 0;
    var pagerStyleUnit = "flickr";//jPagerBar样式
    
    var currentPageIndexUnit = 1;
    var actionUnit = "";//操作
    var orderByUnit = "";//排序字段

function TurnToPageUnit(pageIndexUnit)
    {
           currentPageIndexUnit = pageIndexUnit;
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UnitInfo.ashx',//目标地址
           cache:false,
           data: "pageIndexUnit="+pageIndexUnit+"&pageCountUnit="+pageCountUnit+"&actionUnit="+actionUnit+"&orderbyUnit="+orderByUnit+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1Unit_PagerUnit").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Unit tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.UnitID != null && item.UnitID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillUnit('"+  item.UnitID+"','"+item.UnitName+"');\" />"+"</td>"+

                        "<td height='22' align='center'>"+item.UnitName+"</td>").appendTo($("#pageDataList1Unit tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1Unit_PagerUnit",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleUnit,mark:"pageDataList1UnitMark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUnit,currentPageIndex:pageIndexUnit,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPageUnit({pageindexUnit});return false;"}//[attr]
                    );
                  totalRecordUnit = msg.totalCount;
                 // $("#pageDataList1Unit_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2Unit").value=msg.totalCount;
                  $("#ShowUnitPageCount").val(pageCountUnit);
                  ShowTotalPage(msg.totalCount,pageCountUnit,pageIndexUnit,$("#pagecountUnit"));
                  $("#ToUnitPage").val(pageIndexUnit);
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1Unit_PagerUnit").show();IfShowUnit(document.getElementById("Text2Unit").value);pageDataList1Unit("pageDataList1Unit","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1Unit(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Fun_Search_Unit(aa)
{
      search="1";
      TurnToPageUnit(1);
}
function IfShowUnit(countUnit)
    {
        if(countUnit=="0")
        {
            document.getElementById("divUnit").style.display = "none";
            document.getElementById("pageUnitcount").style.display = "none";
        }
        else
        {
            document.getElementById("divUnit").style.display = "block";
            document.getElementById("pageUnitcount").style.display = "block";
        }
    }
    
    function SelectDept(retvalUnit)
    {
        alert(retvalUnit);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCountUnit,newPageIndexUnit)
    {
        if(newPageCountUnit <=0 || newPageIndexUnit <= 0 ||  newPageIndexUnit  > ((totalRecordUnit-1)/newPageCountUnit)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCountUnit);
            TurnToPageUnit(parseInt(newPageIndexUnit));
        }
    }
  

 
//function Create_Div(img,img1,bool)
//{
//	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
//	FormStr += "<tr>"
//	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
//	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
//	if(bool)
//	{
//		FormStr += "<img src='" + img +"' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
//	}
//	FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
//	FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
//	FormStr += "<tr>"
//	FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
//	FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
//	FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
//	FormStr += "</tr></table>"
//	FormStr += "</td></tr></table>"
//	return FormStr;
//} 
//function closedivUnit()
{
    document.getElementById("divUnit").style.display="none";
}

//function FillUnit(unitid,unitname)
//{
////    document.getElementById(objid).value = unitid;
//    document.getElementById(objname).value = unitname;
//}

</script>