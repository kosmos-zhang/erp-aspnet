<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefTableOPertion.aspx.cs" Inherits="Pages_DefManager_DefTableOPertion" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/office/DefManager/DefTableOPertion.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/PrintArea.js" type="text/javascript"></script>
    <script type="text/javascript">
    function RelationInLoad()
    {
        var relation = $("#HidRelation").val();
        relation = $.trim(relation);
        if(relation !="")
        {
            var arr = new Array();
            arr = relation.split(",");
            for(var i=0;i<arr.length;i++)
            {
                var subarr = new Array();
                subarr = arr[i].split("#");
                document.getElementById("db_"+subarr[subarr.length-2]).value="0";
                for(var ii=0;ii<subarr.length;ii++)
                {
                    if(ii%2!=0)
                    {
                        if(subarr[ii] !=subarr[subarr.length-1])
                        {
                             if(subarr[ii-1]=="加")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)+Number(subarr[ii]);  
                                }
                                else
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)+Number(document.getElementById("db_"+subarr[ii]).value);  
                                }
                             }
                             
                             if(subarr[ii-1] == "减")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)-Number(subarr[ii]);  
                                }
                                else
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)-Number(document.getElementById("db_"+subarr[ii]).value);  
                                }
                             }

                             if(subarr[ii-1] =="乘")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)*Number(subarr[ii]);
                                }
                                else
                                {
                                    document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)*Number(document.getElementById("db_"+subarr[ii]).value);
                                }
                             }
                             
                             if(subarr[ii-1] =="除")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    try
                                    {
                                        document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)/Number(subarr[ii]); 
                                        var num=parseFloat(document.getElementById("db_"+subarr[subarr.length-2]).value);
                                        document.getElementById("db_"+subarr[subarr.length-2]).value = num.toFixed(2);
                                    }catch(ex){document.getElementById("db_"+subarr[subarr.length-2]).value="0"}
                                }
                                else
                                {
                                    try
                                    {
                                        document.getElementById("db_"+subarr[subarr.length-2]).value=Number(document.getElementById("db_"+subarr[subarr.length-2]).value)/Number(document.getElementById("db_"+subarr[ii]).value); 
                                        var num=parseFloat(document.getElementById("db_"+subarr[subarr.length-2]).value);
                                        document.getElementById("db_"+subarr[subarr.length-2]).value = num.toFixed(2);
                                    }catch(ex){document.getElementById("db_"+subarr[subarr.length-2]).value="0"}
                                }
                             }
                        }
                    }
                } 
            }
        }
    }    
    </script>
    
    <script type="text/javascript">
    function RelationInLoadDetail()
    {
        var relation = $("#HidSubRelation").val();
        if(relation !="")
        {
            var arr = new Array();
            arr = relation.split(",");
            for(var m=0;m<arr.length;m++)
            {
                var subarr = new Array();
                subarr = arr[m].split("#");
                //subarr[subarr.length-1]取表名
                try
                {
                    var rowID = document.getElementById(subarr[subarr.length-1]).rows.length; //取表行数
                    for(var j=1;j<=rowID;j++)
                    {
                        document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value="0";
                        
                        
                        //--------****计算开始****------------
                for(var ii=0;ii<subarr.length;ii++)
                {
                    if(ii%2!=0)
                    {
                        if(subarr[ii] !=subarr[subarr.length-1])
                        {
                             if(subarr[ii-1]=="加")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    //命名规则：detail_db_明细表名_字段名_行号
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)+Number(subarr[ii]);  
                                }
                                else
                                {
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)+Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[ii]+"_"+j).value);  
                                }
                             }
                             
                             if(subarr[ii-1] == "减")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)-Number(subarr[ii]);  
                                }
                                else
                                {
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)-Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[ii]+"_"+j).value);  
                                }
                             }
                             
                             if(subarr[ii-1] =="乘")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)*Number(subarr[ii]);
                                }
                                else
                                {
                                    document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)*Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[ii]+"_"+j).value);
                                }
                             }
                             if(subarr[ii-1] =="除")
                             {
                                if(!isNaN(subarr[ii]))
                                {
                                    try
                                    {
                                        document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)/Number(subarr[ii]); 
                                        var num=parseFloat(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value);
                                        document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value = num.toFixed(2);
                                    }catch(ex){document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value="0"}
                                }
                                else
                                {
                                    try
                                    {
                                        document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value=Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value)/Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[ii]+"_"+j).value); 
                                        var num=parseFloat(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value);
                                        document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value = num.toFixed(2);
                                    }catch(ex){document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+j).value="0"}
                                }
                             }
                        }
                    }
                } 
                //-----------*****运算结束******--------------   
                
                        
                        
                        
                        
                        
                    }
                }catch(ex){}
            }
        }
    }    
    </script>
    
    <script type="text/javascript">
    function RelationInLoadDown()
    {
        var relation = $("#HidDownRelation").val();
        relation = $.trim(relation);
        if(relation !="")
        {
            var arr = new Array();
            arr = relation.split(",");
            for(var i=0;i<arr.length;i++)
            {
                try
                {
                    var subarr = new Array();
                    subarr = arr[i].split("#");
                    var aa= subarr[subarr.length-1];
                    var aaa = subarr[subarr.length-2];
                    document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value="0";
                    var rowID = document.getElementById(subarr[subarr.length-1]).rows.length; //取表行数
                    if(subarr[0] == "avg") //取平均数
                    {
                        //detail_db_" +subcode[0]+"_"+subcode[1]+"_"+ rowID
                        for(var row=1;row<rowID;row++)
                        {
                            try
                            {
                                document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value=Number(document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value)+Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+row).value);
                            }catch(ex){}
                        }
                        try
                        {
                            document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value = Number(document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value)/Number(rowID-2);
                            var num=parseFloat(document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value);
                            document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value = num.toFixed(2);
                        }catch(ex){}
                    }
                    
                    if(subarr[0] == "sum") //取汇总值
                    {
                        //detail_db_" +subcode[0]+"_"+subcode[1]+"_"+ rowID
                        for(var row=1;row<rowID;row++)
                        {
                            try
                            {
                                document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value=Number(document.getElementById("db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_total").value)+Number(document.getElementById("detail_db_"+subarr[subarr.length-1]+"_"+subarr[subarr.length-2]+"_"+row).value);
                            }catch(ex){}
                        }
                    }
                    
                }catch(ex){}
            }
        }
    }    
    </script>
    
    <script type="text/javascript">
    function AddDetailRow(obj) 
    {
        obj = $.trim(obj);
        var columstr = document.getElementById("HidTablRelation").value;
        var arrnum =0;
        //一级拆分
        var mainstr =columstr.split('@');
        for(var i=0;i<mainstr.length;i++)
        {
            if(obj == mainstr[i].substring(0,mainstr[i].indexOf("#")))
            {
                arrnum = i;
                break;
            }
        }
        var totalcolum = new Array();
        totalcolum = mainstr[arrnum].split(",");
        //新增明细行
        var signFrame = findObj(obj, document);
        var rowID = signFrame.rows.length;
        
        var detailcolum = totalcolum[0].split("#");

        if(detailcolum[8]=="1")
        {
            if(rowID>1)
            {
                rowID = rowID-1;
            }
        }
        var newTR = signFrame.insertRow(rowID); //添加行
        newTR.id = "Detail_Row_" + rowID;
        var newNameXH = newTR.insertCell(0); //添加列:选择
        newNameXH.className = "tdColInputCenter";
        newNameXH.id = 'Detail_TD_Check_' + rowID;
        newNameXH.innerHTML = "<input name='cusomchk_" + obj + "' id='chk_Option_" + rowID + "' value=\"\" type='checkbox' onclick=\"IfSelectAll('cusomchk_"+obj+"','checkall_"+obj+"');\"/>";
       
       for(var i=1;i<=totalcolum.length;i++)
       {
            var subcode = totalcolum[i-1].split("#");
            var addColum = newTR.insertCell(i); //添加列:字段代码
            addColum.className = "cell";
            
            if(subcode[3] == "0") //文本框
            {
                addColum.innerHTML = "<input id='detail_db_" +subcode[0]+"_"+subcode[1]+"_"+ rowID + "' onblur=\"RelationInLoadDetail();RelationInLoadDown()\" value='"+subcode[7]+"' type='text' class=\"tdinput\"  style='width:90%;' />";
            }
            
            if(subcode[3] == "1") //下拉框
            {
                addColum.innerHTML = "<select id='detail_db_" + subcode[0]+"_"+subcode[1]+"_"+ rowID + "' style='width:90%'>" + subcode[7] + "</select>";
            }
            
            if(subcode[3] == "2") // 时间控件
            {
                addColum.innerHTML = "<input id='detail_db_" + subcode[0]+"_"+subcode[1]+"_"+ rowID + "' type='text' style='width:90%' readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('detail_db_" + subcode[0]+"_"+ rowID + "')})\" /> ";
            }
       }
    }
    
    //删除行
    function DeleteDetailRow(obj) 
    {
        var signFrame = findObj(obj, document);
        var ck = document.getElementsByName("cusomchk_"+obj);
        for (var i = 0; i < ck.length; i++) {
            var rowID = i + 1;
            if (ck[i].checked) {
                signFrame.rows[rowID].style.display = "none";
//                alert("Detail_Row_"+i);
//                signFrame.deleteRow("Detail_Row_"+rowID);
            }
        }
    }
    
    //table.deleteRow(row);

    //全选
    function fnSelectAll(objtable) {
        $.each($("input[name='cusomchk_"+objtable+"']"), function(i, obj) {
            obj.checked = !obj.checked;
        });
    }
    </script>
    
    <script type="text/javascript">
    function InitDetailTableRows()
    {
        var state = document.getElementById("HidControlID").value;
        if(state == "-1")
        {
            //如果为-1表示是新建
            var rowstr = document.getElementById("HidTableRowNum").value;
            var tablerow = rowstr.split(",");
            for(var i=0;i<tablerow.length;i++)
            {
                var subtablerow = tablerow[i].split("#");
                for(var ii=0;ii<subtablerow[1];ii++)
                {
                    try
                    {
                        AddDetailRow(subtablerow[0]);
                    }catch(ex){continue;}
                }
            }
        }
    }
    </script>
    
    <script type="text/javascript">
     function PagePrint() {
         $("#myPrintArea").html($("#printstr").html());
         $("#myPrintArea img").hide();
         $('#myPrintArea').printArea();
     }
    </script>
</head>
<body onload="InitDetailTableRows();RelationInLoadDetail();RelationInLoadDown()">
    <form id="frmMain" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <asp:HiddenField ID="HidControlID" runat="server" Value ="-1" /><!--储存当前表单的ID，为“-1”时新建否则跟新ID中对应的表单 -->
    <asp:HiddenField ID="HidControlTableID" runat="server"  Value ="-1" /><!--储存当前表单应该存储到的表的ID-->
    <asp:HiddenField ID="HidControlName" runat="server" /><!--存储控件ID，传递各个控件的值-->
    <asp:HiddenField ID="HidControlList" runat="server" /><!--存储控件ID+控件获取数据类型+容纳长度+是否允许为空，各值之间用#割开-->
    <asp:HiddenField ID="HidRelation" runat="server" /><!--存储表字段关系-->
    <asp:HiddenField ID="HidSubRelation" runat="server" /><!--存储子表字段关系-->
    <asp:HiddenField ID="HidDownRelation" runat="server" /><!--存储子表纵向统计关系-->
    <asp:HiddenField ID="HidTablRelation" runat="server" /><!--存储表之间关系-->
    <asp:HiddenField ID="HidTableRowNum" runat="server" /><!--存储明细表默认生成的行数-->
    <asp:HiddenField ID="HidSubTableCode" runat="server" /><!--存储明细表字段-->
    
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <span lang="zh-cn"><asp:Label ID="lbl_title" runat="server" Text=""></asp:Label></span>
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            <asp:HiddenField ID="txtUserName" runat="server" />
                            <input id="hfuserid" type="hidden" />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand;
                                float: left" border="0" onclick="GetDetailsTableStruct()" runat="server"/>
                             <img alt="" src="../../../Images/Button/btn_print.jpg" onclick="PagePrint()" style="cursor:pointer;" />
                             <img alt="" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="history.back()" style="cursor:pointer;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
            <table id="printstr">
            <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <div style="display:none">
                     <table width="99%" border="0" align="center" id="myPrintArea" cellpadding="0" cellspacing="0" bgcolor="#999999"> 
                     </table>
               </div>
                
                <table width="99%" border="0" align="center" id="Tb_01" cellpadding="0" cellspacing="0" bgcolor="#999999">
                     <tr id="printhead" style="display:none;">
                        <td></td>
                     </tr>
                    <tr>
                        <asp:DataList ID="StructDL" RepeatColumns="3" RepeatDirection="Horizontal" 
                            runat="server" onitemdatabound="StructDL_ItemDataBound">
                        <ItemTemplate>
                            <td align="right" bgcolor="#E6E6E6" style="border-top:solid 1pt Black;border-left:solid 1pt Black;border-bottom:solid 1pt Black;border-right:solid 1pt Black;">
                                <%#Eval("cname")%><asp:Literal ID="ltl_tag" runat="server"></asp:Literal>
                            </td>
                            <td bgcolor="#FFFFFF" class="style1" style="border-top:solid 1pt Black;border-left:asolid 1pt Black;border-bottom:solid 1pt Black;border-right:solid 1pt Black;">
                                <asp:Literal ID="ltl_input" runat="server"></asp:Literal>
                            </td>
                        </ItemTemplate>
                        </asp:DataList>
                    </tr>
                    <tr>
                        <td valign="top" style="background-color:#FFFFFF">
                            <asp:Literal ID="ltl_input_custom" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
