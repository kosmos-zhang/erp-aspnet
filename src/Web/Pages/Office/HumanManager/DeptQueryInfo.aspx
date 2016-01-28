<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptQueryInfo.aspx.cs" Inherits="Pages_Office_HumanManager_DeptQueryInfo" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>岗位说明书</title>
    <base target="_self" ></base>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <style type="text/css">
body {
	color: #000000;
	font-family: 宋体;
	font-size: 12px;
	line-height: 170%;
	margin-bottom: 0px;
	margin-left: 0px;
	margin-right: 0px;
	margin-top: 0px;
	scrollbar-3dlight-color: #999999;
	scrollbar-arrow-color: #888888;
	scrollbar-darkshadow-color: #FFFFFF;
	scrollbar-face-color: #EBEBEB;
	scrollbar-highlight-color: #FFFFFF;
	scrollbar-shadow-color: #7A7A7A;
	scrollbar-track-color: #FFFFFF;} /*滚动条样式*/

.hidden{display:none}

table {  font-family: "宋体"; font-size: 12px;}

a:link {
	color: #135294;
	text-decoration: none;
}
a:visited {
	color: #135294;
	text-decoration: none;
}

a:hover {
	color: #FF0000;
	text-decoration: underline;
}

a:active {
	background-color: transparent;
	color: #000000;
	text-decoration: none;} /*加了链接的字符样式*/
</style>

    <script language="javascript" type="text/javascript">
    function getUserOrdept()
    {
        var div_id=document.getElementById("dv_tree");
        var objs=div_id.getElementsByTagName('input');
        var select="";
                //遍历所有控件
                for(var i = 0; i < objs.length; i++)
                {
                    //判断是否是选中的checkbox
                    if(objs[i].getAttribute("type") == "checkbox" && objs[i].checked)
                    {
                        //获取列的值
                        var values = objs[i].value;
                        
                        select += values.toString() + ",";
                    }
                     //判断是否是选中的radiobutton
                    if(objs[i].getAttribute("type") == "radio" && objs[i].checked)
                    {
                        //获取列的值
                       var values = objs[i].value;
                        select += values.toString() + ",";
                    }
                }
                if(select=="")
                {
                    alert("请选择部门或人员！");
                    return;
                }
                select=select.substring(0,select.length - 1);
                window.returnValue=select;
                window.close();
    }
    	
    function ClearInfo() 	
    {
                window.returnValue="ClearInfo";
                window.close();

            }


            function readAfter2() {
                fnchanage("1");
            
            }

            function readAfter() {
                fnchanage('2');
                var attachment = document.getElementById("hidaddd").value;
                //附件存在时
                if (attachment == null || attachment == "") {

                    //下载删除不显示
                   // document.getElementById("divDealAttachment").style.display = "none";
                    //上传显示 
                  //  document.getElementById("divUploadAttachment").style.display = "block";
                }
                else {
                    //下载删除显示
                 //   document.getElementById("divDealAttachment").style.display = "block";
                    //上传不显示 
                  //  document.getElementById("divUploadAttachment").style.display = "none";
                }
               
              
                
             


            }
    </script>
    
    
    <script type="text/javascript">

        function FCKeditor_OnComplete(editorInstance) {
            editorInstance.Events.AttachEvent('OnBlur', FCKeditor_OnBlur);
            editorInstance.Events.AttachEvent('OnFocus', FCKeditor_OnFocus);

//            editorInstance.ToolbarSet.Name = "Basic";
//            var oCombo = document.getElementById('cmbToolbars');
//            oCombo.value = editorInstance.ToolbarSet.Name;
//            oCombo.style.visibility = '';

           // editorInstance.ToolbarSet.Name = "Basic";
           // editorInstance.ToolbarSet.RefreshModeState();
          //  var sToolbar;
           // if (document.location.search.length > 1)
             //   sToolbar = document.location.search.substr(1);

 

         //   if (sToolbar != null)
            //    editorInstance.ToolbarSet = sToolbar;

            //oFCKeditor.Value = '<p>This is some <strong>sample text<\/strong>. You are using <a href="http://www.fckeditor.net/">FCKeditor<\/a>.<\/p>';
           // editorInstance.Create();
            editorInstance.EditorDocument.body.contentEditable = false;
//            editorInstance.EditMode = FCK_EDITMODE_SOURCE;
//            editorInstance.ToolbarSet.RefreshModeState();
//            editorInstance.EditMode = FCK_EDITMODE_WYSIWYG;
//            editorInstance.ToolbarSet.RefreshModeState(); 
        }

        function FCKeditor_OnBlur(editorInstance) {
            editorInstance.ToolbarSet.Collapse();
        }

        function FCKeditor_OnFocus(editorInstance) {
            editorInstance.ToolbarSet.Collapse();
        }



        function InsertHTML() {
            // Get the editor instance that we want to interact with.
            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

            // Check the active editing mode.
            if (oEditor.EditMode == FCK_EDITMODE_WYSIWYG) {
                // Insert the desired HTML.
                oEditor.InsertHtml('- This is my world -');
            }
            else
                alert('You must be on WYSIWYG mode!');
        }

        function GetInnerHTML() {
            // Get the editor instance that we want to interact with.
            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

            alert(oEditor.EditorDocument.body.innerHTML);
        }

        function GetContents() {
            // Get the editor instance that we want to interact with.
            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

            // Get the editor contents in XHTML.
            alert(oEditor.GetXHTML(true)); 	// "true" means you want it formatted.
        }
    


         function fnchanage(sign)
         {
             if (sign == "1") {

                 document.getElementById("dvTemplate").style.display = "block";
                 document.getElementById("dvUserDept").style.display = "none";
                 document.getElementById("tr1").style.display = "none";
                        document.getElementById("tr2").style.display = "none";
                        document.getElementById("tr3").style.display = "none";
                        document.getElementById("tr4").style.display = "none";
                        document.getElementById("tr5").style.display = "none";
                        document.getElementById("tr6").style.display = "block";

                
             }
             else if (sign == "2") {
             document.getElementById("dvTemplate").style.display = "none";
             document.getElementById("dvUserDept").style.display = "block";
             document.getElementById("tr1").style.display = "block";
             document.getElementById("tr2").style.display = "block";
             document.getElementById("tr3").style.display = "block";
             document.getElementById("tr4").style.display = "block";
             document.getElementById("tr5").style.display = "block";
             document.getElementById("tr6").style.display = "none";
             }
         
         }
	</script>
</head>

<body>
    <form id="form1" runat="server">
    

    <input id ="hidaddd" type="hidden" runat="server"/>
   

    <br />
  <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#F0F0F0"  id="tblDetailList" >
        <tr>
        <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /> </td>
      </tr>
      <tr>
        <td height="30" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />职位说明书</span></td>
      </tr>
      <tr>
        <td  ><table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
          <tr>
            <td bgcolor="#FFFFFF"><table width="100%"border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC"   id="mainindex">
            <tr>
            <td> <span onmousemove="fnchanage('2')" class="Blue"> 职位说明书 </span>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;<span    onmousemove="fnchanage('1')"  class="Blue"  style="width:50%" >  职位说明书模板库  </span> </td>
            <td></td>
            </tr>
              <tr>
                <td align="left"  valign="top" bgcolor="#FFFFFF" class="Blue"  style=" width:50%">
                <div  id="dvUserDept" style=" display:block" ><br />
                        <input type="hidden" id="hidSelectValue" />
                        <asp:TreeView ID="UserDeptTree" runat="server" ShowLines="True"     > </asp:TreeView>
                      
                        <br />
                        <br />
                </div>
                
                   <div  id="dvTemplate" style=" display:none"><br />
                   
                        <asp:TreeView ID="trTemplate" runat="server" ShowLines="True"     > </asp:TreeView>
                      
                        <br />
                        <br />
                </div>
                
                </td>
                <td  bgcolor="#FFFFFF"  valign="top" > 
                
                 <div id="divEditDeptQuarter" runat="server" style="background: #fff; padding: 10px; width:50%;     ">    
        <table width="850px" border="0" cellpadding="0" cellspacing="0"  id="tblDeptInfo"  >
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <%--img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSaveInfo();" />--%>&nbsp;
                           
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td >
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfAttachment" runat="server" />
                                 <input  type="hidden" id="hfdNo" runat="server"/>
                                            <asp:HiddenField ID="hfPageAttachment" runat="server" />
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    <tr id="tr6" style=" display:none">
                                        <td height="20" align="right" class="tdColTitle" width="5%">岗位说明书名称 </td>
                                        <td height="30" class="tdColInput" width="15%"  >
                                           <asp:TextBox ID="txtTmpleateName" runat="server"  CssClass="tdinput"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="5%">  </td>
                                        <td height="20" class="tdColInput" width="10%">
                                          
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="6%"> </td>
                                        <td height="20" class="tdColInput" width="10%">
                                           
                                        </td>
                                    </tr>
                                    <tr id="tr5" style=" display:block ">
                                        <td height="20" align="right" class="tdColTitle" width="5%">岗位编号<span class="redbold">*</span></td>
                                        <td height="30" class="tdColInput" width="15%"  >
                                            <div id="divCodeRule" runat="server" style="float:left;display:none  ">
                                                <uc1:CodingRule ID="codeRule" runat="server" />
                                            </div>
                                            <div id="divCodeNo" runat="server" class="tdinput" style="float:left;  " >
                                                <input type="text" id="txtDisplayCode" class="tdinput"  runat="server" disabled="disabled"/>
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="5%">所属机构<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="10%">
                                            <asp:TextBox ID="txtDeptName" runat="server" Enabled="false" CssClass="tdinput"  Width="96%"  ></asp:TextBox>
                                            <input type="hidden" id="txtDeptID" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="6%">上级岗位</td>
                                        <td height="20" class="tdColInput" width="10%">
                                            <asp:TextBox ID="txtSuperQuarterName" runat="server" Enabled="false" CssClass="tdinput" Width="95%" ></asp:TextBox>
                                            <input type="hidden" id="hidSuperQuarter" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="tr4" style=" display:block ">
                                        <td height="20" align="right" class="tdColTitle">岗位名称<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtQuarterName" runat="server" Width="96%" MaxLength="25" CssClass="tdinput"   Enabled="false" ></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">拼音代码</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtPYShort" runat="server" CssClass="tdinput" Width="96%" Enabled="false"></asp:TextBox> 
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">是否关键岗位</td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlKeyFlag" runat="server" Enabled="false">
                                                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="tr3" style=" display:block ">
                                        <td height="20" align="right" class="tdColTitle">岗位分类</td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlQuarterType" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">描述信息</td>
                                        <td height="20" class="tdColInput"  style="width:10%">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Width="96%" CssClass="tdinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" >岗位级别</td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlQuarterLevel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="tr2" style=" display:block ">
                                        <td height="20" align="right" class="tdColTitle">启用状态<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlUsedStatus" runat="server" Enabled="false">
                                                <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">附件</td>
                                        <td height="20" class="tdColInput" style="width:10%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div id="divUploadAttachment" runat="server" style=" display:none ">
                                                            <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                                        </div>
                                                        <div id="divDealAttachment" runat="server" style="display:none;">
                                                            <a href="#" onclick="DealAttachment('download');">  <span id='spanAttachmentName' runat="server"></span></a>
                                                            <a href="#" onclick="DealAttachment('clear');" style=" display:none ">删除附件</a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                      
                                        </td>     
                                        <td height="20" align="right" class="tdColTitle"></td>
                                        <td height="20" class="tdColInput">
                                        
                                        
                                            <asp:TextBox ID="txtNothing" Enabled="false" runat="server" MaxLength="100" Width="95%" CssClass="tdinput" Visible="false" ></asp:TextBox></td>
                                    </tr>
                                 
                                            <tr id="tr1" style=" display:block ">
                                        <td height="20" align="right" class="tdColTitle" width="8%">开启关联模块  </td>
                                        
                                        <td height="20"   class="tdColTitle"   colspan="5"  style =" text-align:center">是否开启所属类型</td>
                                       
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chMMubiao" runat="server" Text="目标管理模块"   Enabled="false"    />   </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">
                                            <asp:CheckBox  ID="chMRi" runat="server" Text="日目标" Enabled="false"         />    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMZhou" runat="server" Text="周目标"   Enabled="false"   />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMYue" runat="server" Text="月目标"  Enabled="false"  />   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMJi" runat="server" Text="季目标"   Enabled="false"   />   </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMNian" runat="server" Text="年目标" Enabled="false"  />   </td>
                                    </tr>
                                   <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chRRenWu" runat="server" Text="任务管理模块"   Enabled="false"   />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center"><asp:CheckBox  ID="chRGEren" runat="server" Text="个人任务"  Enabled="false"    />    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> 
                                            <asp:CheckBox  ID="chRZhipai" runat="server" Text="指派任务" Enabled="false"  />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%"><asp:CheckBox  ID="chGgongzuo" runat="server" Text="工作日志模块" Enabled="false" />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">&nbsp;   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> &nbsp;    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                       <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%"><asp:CheckBox  ID="chCricheng" runat="server" Text="日程管理模块" Enabled="false" />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">&nbsp;   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> &nbsp;    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                    <tr>
                                    <td colspan="6">
                                       <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server"  Height="700px">

        </FCKeditorV2:FCKeditor></td>
                                    </tr>
                                 
                                       <tr>
                                        <td height="20" align="right" class="tdColTitle">岗位职责</td>
                                        <td height="20" colspan="5" class="tdColInput">
                                            <asp:TextBox ID="txtDuty" TextMode="MultiLine" MaxLength="1024" Width="95%" CssClass="tdinput" runat="server"    Height="54px"  SpecialWorkCheck="岗位职责" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">任职资格</td>
                                        <td height="20" colspan="5" class="tdColInput">
                                            <asp:TextBox ID="txtDutyRequire" TextMode="MultiLine" MaxLength="1024" Width="95%" CssClass="tdinput" runat="server"   Height="54px"   SpecialWorkCheck="任职资格" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2" height="10"></td></tr>
                    </table>
                <!-- </div> -->
                </td>
            </tr>
        </table>    
    </div>
                </td>
              </tr>
            </table></td>
          </tr>
        </table>
        <br /></td>
    </tr>
    </table>
    </form>
</body>
</html>