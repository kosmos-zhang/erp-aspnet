<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseProductUC.ascx.cs" Inherits="UserControl_PurchaseProductUC" %>
  <style type="text/css">

.tdinput
{
	border-width:0pt;
	background-color:#ffffff; 
	height:21px;
	margin-left:2px;
}
  #Product_List 
        {
            border:solid 1px #111111;
            width:165px;
            z-index:11;
            display:none;
            position:absolute;
            background-color:White;
            
        }
    </style>
    <a name="pageprodDataListMark"></a>
    <span id="Forms" class="Spantype"></span>
    <!--提示信息弹出详情start-->
    <div id="divzhezhao1" style="filter: Alpha(opacity=0); width: 950px;padding: 10px; height: 500px;
        z-index: 199; position: absolute; display: none; top: 20%; left: 10%;">
        <iframe style="border: 0; width: 950px; height: 100%; position: absolute;"></iframe>
    </div>
    <div id="divStorageProduct" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 950px; z-index: 200; position: absolute; display:none;
        top: 20%; left:10%;>

        <div id="divquery" style="width: 100%; left: 0px;">
                   <%-- <a onclick="closeProductdiv('divStorageProduct')" style="text-align: right; cursor: pointer">关闭</a>--%>
                     <a onclick="closeProductdiv()" style="text-align: right; cursor: pointer"><img  src="../../../images/Button/Bottom_btn_close.jpg"/></a>
                   <input id="hf_typeid" type="hidden" />
                    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" border="0" align="left" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        物品编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                       <input type="text" id="txt_ProdNO"  specialworkcheck="物品编号" class="tdinput"/></td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       物品名称</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                            <input type="text" id="txt_ProdName" specialworkcheck="物品名称" class="tdinput"
                                  /></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        拼音代码
                                    </td>
                                    <td bgcolor="#FFFFFF" style="width: 0%">
                            <input type="text" id="txt_PYShort" specialworkcheck="拼音代码" name="txtConfirmorReal2" class="tdinput"
                                   /></td>
                                   
                                     <td width="10%" bgcolor="#E7E7E7" align="right">
                                        物品分类
                                    </td>
                                    <td bgcolor="#FFFFFF" style="width: 0%">
                                        <input type="text" id="txt_TypeID" readonly onclick="showUserList()" class="tdinput" /></td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                       <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_Product()' id="btn_search" />
        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="1" id="pageDataListProduct"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <%-- <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            查看<span id="oID" class="orderTip"></span></div>
                    </th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('ProdNo','ProdNo');return false;">
                            物品编号<span id="ProdNo" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('ProductName','ProductName');return false;">
                            物品名称<span id="ProductName" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('CodeTypeName','CodeTypeName');return false;">
                            物品分类<span id="CodeTypeName" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('Specification','Specification');return false;">
                            规格型号<span id="Specification" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('CodeName','CodeName');return false;">
                            基本单位<span id="CodeName" class="orderTip"></span></div>
                    </th>
                      <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('CurrentStore','CurrentStore');return false;">
                            可用存量<span id="CurrentStore" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('StandardBuy','StandardBuy');return false;">
                            含税进价<span id="StandardBuy" class="orderTip"></span></div>
                    </th>

                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('TaxBuy','TaxBuy');return false;">
                            去税进价<span id="TaxBuy" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('InTaxRate','InTaxRate');return false;">
                            进项税率(%)<span id="InTaxRate" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('StandardSell','StandardSell');return false;">
                            去税售价<span id="StandardSell" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('SellTax','SellTax');return false;">
                            含税售价<span id="SellTax" class="orderTip"></span></div>
                    </th>
                   
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('TaxRate','TaxRate');return false;">
                            销项税率(%)<span id="TaxRate" class="orderTip"></span></div>
                    </th>
                     <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderByProduct('Discount','Discount');return false;">
                           折扣(%)<span id="Discount" class="orderTip"></span></div>
                    </th>
                </tr>
            </tbody>
        </table>
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" style=" margin-left:10px; position:relative">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecountProduct">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pagerproduct" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpageProduct">
                                            <input name="text" type="text" id="TextProduct" style="display: none" />
                                            <span id="pageDataList1_TotalProduct"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCountProduct" size="6" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPageproduct"  size="6"/>
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProduct($('#ShowPageCountProduct').val(),$('#ToPageproduct').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="Product_List" style="display:none;">
  <iframe id="aaaa" style="position: absolute; z-index: -1; width:165px; height:100px;" frameborder="0">  </iframe>  
<div style="background-color:Silver;padding:3px; height:20px; padding-left:50px; padding-top:1px">
<a href="javascript:hidetxtUserList()" style="float:right;">关闭</a>
</div>
<div style=" padding-top:5px; height:300px; width:165px; overflow:auto; margin-top:1px">
    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
    </asp:TreeView></div>
</div>
        </div>
    </div>
    <!--提示信息弹出详情end-->
<script language="javascript">
var popTechObj={};
popTechObj.objInput = null;
popTechObj.ShowList=function(objInput)
{
    popTechObj.objInput=objInput;
    Hidebtn();
}

function Hidebtn()
{
 document.getElementById('divStorageProduct').style.display='inline';
 document.getElementById('divzhezhao1').style.display='inline';

  AlertProductMsg();
  Fun_Search_Product();
}

    var ProdNo="";
    var ProdName="";
    var PYShort="";
    var typeid="";
    var pageCountproduct = 10;//每页计数
    var totalRecord = 0;
    var pagerproductStyle = "flickr";//jPagerBar样式
    var flag="";
     var str="";
    var currentPageIndexproduct = 1;
    var action = "";//操作
    var orderByP = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageProduct(pageIndex)
    {
          currentPageIndexproduct = pageIndex;
           var ProductName="";
           var TypeID="";
           var UnitID = "";
           var UsedStatus = "";
           var Remark = "";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/PurchaseProduct.ashx',//目标地址
           cache:false,
                     data: "pageIndex="+pageIndex+"&pageCount="+pageCountproduct+"&action="+action+"&orderByP="+orderByP+"&ProductID="+escape(ProdNo)+"&ProdName="+escape(ProdName)+"&PYShort="+escape(PYShort)+"&typeid="+escape(typeid)+"&Condition="+escape(popTechObj.Condition)+"",//数据
           beforeSend:function(){$("#pageDataList_PagerProduct").hide();},//发送数据之前
           
         success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListProduct tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                        var tempOnclick="";
                         tempOnclick= ""+  item.ID+",'"+item.ProdNo+"','"+item.ProductName+"','"+item.StandardSell+"','"+item.UnitID+"','"+item.CodeName+"','"+item.TaxRate+"','"+item.SellTax+"','"+item.Discount+"','"+item.Specification+"','"+item.CodeTypeName+"','"+item.TypeID+"','"+item.StandardBuy+"','"+item.TaxBuy+"'";
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_Content("+tempOnclick+");closeProductdiv();\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProdNo + "</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.CodeTypeName+"</td>"+
                       "<td height='22' align='center'>"+item.Specification+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                        "<td height='22' align='center'>"+item.CurrentStore+"</td>"+
                         "<td height='22' align='center'>"+item.StandardBuy+"</td>"+
                         "<td height='22' align='center'>"+item.TaxBuy+"</td>"+
                        "<td height='22' align='center'>"+item.InTaxRate+"</td>"+
                        "<td height='22' align='center'>"+item.StandardSell+"</td>"+
                         "<td height='22' align='center'>"+item.SellTax+"</td>"+   
                        "<td height='22' align='center'>"+item.TaxRate+"</td>"+
                        "<td height='22' align='center'>"+item.Discount+"</td>").appendTo($("#pageDataListProduct tbody"));}
                   });
                     //页码
                      ShowPageBar("pageDataList1_Pagerproduct",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerproductStyle,mark:"pageprodDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountproduct,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageProduct({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     document.getElementById('TextProduct').value=msg.totalCount;
                      $("#ShowPageCountProduct").val(pageCountproduct);
                      ShowTotalPage(msg.totalCount,pageCountproduct,pageIndex);
                      $("#ToPageproduct").val(pageIndex);
                          ShowTotalPage(msg.totalCount,pageCountproduct,currentPageIndexproduct,$("#pagecountProduct"));
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pagerproduct").show();Ifshowproduct( document.getElementById('TextProduct').value);pageDataListProduct("pageDataListProduct","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
       //table行颜色
function pageDataListProduct(o,a,b,c,d){
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

function Fun_Search_Product(aa)
{
  var fieldText = "";
   var msgText = "";
   var isFlag = true;

     ProdNo=document.getElementById("txt_ProdNO").value;
     ProdName=document.getElementById("txt_ProdName").value;
     PYShort=document.getElementById("txt_PYShort").value;
      typeid=document.getElementById("hf_typeid").value;
      
          var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
      if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return ;
    }
      search="1";
     TurnToPageProduct(1)
}
    function Ifshowproduct(count)
    {
        if(count=="0")
        {
        document.getElementById('divpageProduct').style.display = "none";
        document.getElementById("pagecountProduct").style.display = "none";
        }
        else
        {
        document.getElementById("divpageProduct").style.display = "block";
        document.getElementById("pagecountProduct").style.display = "block";
        }
    }
    function SelectDept(retval)
    {
        alert(retval);
    }

 //改变每页记录数及跳至页数
    function ChangePageCountIndexProduct(newPageCount,newPageIndex)
    {
     if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCountproduct=parseInt(newPageCount);
            TurnToPageProduct(parseInt(newPageIndex));
        }
    }

    //排序
    function OrderByProduct(orderColum,orderTip)
    {
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
        orderByP = orderColum+"_"+ordering;
      TurnToPageProduct(1);
    }
 
function ClearProductInfo()
{

 if(typeof(popTechObj.CleraHidden) != "undefined")
 {
    document.getElementById(popTechObj.CleraHidden).value="";
 }
 if(typeof(popTechObj.CleraInput) != "undefined")
 {
  document.getElementById(popTechObj.CleraInput).value="";
  document.getElementById(popTechObj.CleraInput).title="";
 }
 closeProductdiv();
}
function closeProductdiv()
{
    document.getElementById("divStorageProduct").style.display="none";
    document.getElementById("txt_ProdNO").value="";
    document.getElementById("txt_ProdName").value="";
    document.getElementById("txt_PYShort").value="";
    document.getElementById("hf_typeid").value="";
    var ProductBigDiv = document.getElementById("ProductBigDiv");
		var divStorageProduct = document.getElementById("divStorageProduct");
		document.body.removeChild(ProductBigDiv); 
		divStorageProduct.style.display="none";
		document.getElementById('divzhezhao1').style.display='none';
}
	function AlertProductMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','ProductBigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=10);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "100";
		document.body.appendChild(maskObj);
		
	}
function SelectedNodeChanged(code_text,type_code)
{   

document.getElementById("hf_typeid").value=type_code;
   document.getElementById("txt_TypeID").value=code_text;
   hideUserList();
}
    function hidetxtUserList()
    {
        hideUserList();
        document.getElementById("txt_TypeID").value="";
        document.getElementById("hf_typeid").value='';
    }
    
       function getChildNodes(nodeTable)
      {
            if(nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;  
           
            if(nodes.tagName == "DIV")
            {
                return nodes.childNodes;//return childnodes's nodeTables;
            }
            return [];
      }
      
        function showUserList()
        {
            var list = document.getElementById("Product_List");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("txt_TypeID"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            document.getElementById("Product_List").style.display = "block";
        }
        function elePos(et) {
   
    var left=-140;
	var top=-145;
	while(et.offsetParent){
	left+=et.offsetLeft;
	top+=et.offsetTop;
	et=et.offsetParent;
	}
	left+=et.offsetLeft;
	top+=et.offsetTop;
	return {x:left,y:top}; 
}
        
        function hideUserList()
        {
            document.getElementById("Product_List").style.display = "none";
        }
</script>


