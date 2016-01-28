<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptQuarter_Info.aspx.cs" Inherits="Pages_Office_HumanManager_DeptQuarter_Info" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>岗位设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/DeptQuarter_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
        
    <script type="text/javascript">

        function FCKeditor_OnComplete(editorInstance) {
            editorInstance.Events.AttachEvent('OnBlur', FCKeditor_OnBlur);
            editorInstance.Events.AttachEvent('OnFocus', FCKeditor_OnFocus);
        }

        function FCKeditor_OnBlur(editorInstance) {
            editorInstance.ToolbarSet.Collapse();
        }

        function FCKeditor_OnFocus(editorInstance) {
            editorInstance.ToolbarSet.Expand();
        }


        function readsssss() {

            document.getElementById("divEditDeptQuarter").style.display = "block";
        
        
         }
        
        
         

//        function InsertHTML() {
//            // Get the editor instance that we want to interact with.
//            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');
//            
//            // Check the active editing mode.
//            if (oEditor.EditMode == FCK_EDITMODE_WYSIWYG) {
//                // Insert the desired HTML.
//                oEditor.InsertHtml('- This is my world -');
//            }
//            else
//                alert('You must be on WYSIWYG mode!');
//        }

//        function GetInnerHTML() {
//            // Get the editor instance that we want to interact with.
//            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

//            alert(oEditor.EditorDocument.body.innerHTML);
//        }

//        function GetContents() {
//            // Get the editor instance that we want to interact with.
//            var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

//            // Get the editor contents in XHTML.
//            alert(oEditor.GetXHTML(true)); 	// "true" means you want it formatted.
//        }

	</script>
</head>
<body>
<form id="frmMain" runat="server">
<input id="hidDeptInfo" type="hidden" runat="server" />
<input id ="hidquarterID" type="hidden" runat="server"/>
<input id ="hidaddd" type="hidden" runat="server"/>
  <br />
  
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#F0F0F0" class="checktable" id="tblDetailList" >
      <tr>
        <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /> </td>
      </tr>
      <tr>
        <td height="30" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />岗位设置</span></td>
      </tr>
      <tr>
        <td height="2"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
          <tr>
            <td bgcolor="#FFFFFF"><table width="100%"border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC" id="mainindex">
              <tr>
                <td width="350px" height="100%" align="left"  valign="top" bgcolor="#FFFFFF" class="Blue"><input type="hidden" id="hidSelectValue" />
                    <input type="hidden" id="hidSelectControl" />
                    <div>
                      <table style="margin-left:8%;margin-top:5%; width:100%">
                        <tr>
                          <td><span  class="Blue" style="font-size:14px">
                            <div runat="server" id="divCompany"></div>
                          </span> </td>
                        </tr>
                        <tr>
                          <td><table  width="100%">
                              <tr>
                               
                                <td><div id="divDeptQuarterTree" runat="server" style="overflow-x:auto;overflow-y:auto;height:500px;width:100%;  "> </div></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table>
                    </div></td>
                <td align="left" valign="top" bgcolor="#FFFFFF"><table width="99%" border="0" style="margin-right:10%;margin-top:5%;" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td ><span  class="Blue" style="font-size:14px">组 &nbsp;&nbsp;织 ： </span> </td>
                      <td> <div id="selectedDeptName" style="text-align:left;"></div> </td>
                    </tr>
                    <tr>
                      <td colspan="2"><span  class="Blue" style="font-size:14px">岗 &nbsp;&nbsp;位 </span> </td>
                    </tr>
                    <tr>
                      <td  style="width:220px"><div id="divDeptTree" runat="server" style="overflow-x:auto;overflow-y:auto;height:500px;width:100%;"></div></td>
                      <td style="vertical-align:top"><img id="btnNew" runat="server" alt="新建"   onclick="DoNew();"   src="../../../Images/Button/Bottom_btn_new.jpg" style="cursor:hand"   visible="false"  /> <br />
                          <img src="../../../Images/Button/cw_tjtj.jpg" alt="添加同级" visible="false" id="btnAddSame" runat="server" style="cursor:hand"   onclick="DoEditDeptQuarter('0');"/> <br />
                          <img src="../../../Images/Button/cw_xj.jpg" alt="添加下级" visible="false" id="btnAddSub" runat="server" style="cursor:hand"   onclick="DoEditDeptQuarter('1');"/> <br />
                        <%--  <img src="../../../Images/Button/Show_edit.jpg" alt="修改" visible="false" id="btnModify" runat="server" style="cursor:hand"   onclick="DoEditDeptQuarter('2');"/> <br />--%>
                          
                          
                              <asp:ImageButton ID="btnModify" runat="server"   
                              ImageUrl="../../../Images/Button/Show_edit.jpg" onclick="imbEdit_Click"   OnClientClick =" return  DoEditDeptQuarter('2');"   /> <br />
                          <img src="../../../images/Button/Show_del.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'  /></td>
                    </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <br /></td></tr>
    </table>
 
    <div id="divEditDeptQuarter" runat="server" style="background: #fff; padding: 10px; width: 850px; z-index:10; position: absolute;top: 20%; left: 15%;  display:none       ">    
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
                                <%--img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSaveInfo();" />--%>
                            
                               
                                
                                <asp:ImageButton ID="btnSave" runat="server" 
                                    ImageUrl="../../../Images/Button/Bottom_btn_save.jpg" onclick="imbSave_Click"  OnClientClick=" return CheckClient()"/>
                                <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand"  onclick="DoBack();"/>
                               <asp:Label ID="lblErrorMes" runat="server"  ForeColor="Red" Width="400px"  Visible="false"></asp:Label>
                           
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
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
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="5%">岗位编号<span class="redbold">*</span></td>
                                        <td height="30" class="tdColInput" width="15%"  >
                                            <div id="divCodeRule" runat="server" style="float:left;  ">
                                                <uc1:CodingRule ID="codeRule" runat="server" />
                                            </div>
                                            <div id="divCodeNo" runat="server" class="tdinput" style="float:left; display:none " >
                                                <input type="text" id="txtDisplayCode" class="tdinput"  runat="server"/>
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="5%">所属机构<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="10%">
                                            <asp:TextBox ID="txtDeptName" runat="server"   CssClass="tdinput"  Width="96%"></asp:TextBox>
                                            <input type="hidden" id="txtDeptID" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="6%">上级岗位</td>
                                        <td height="20" class="tdColInput" width="10%">
                                            <asp:TextBox ID="txtSuperQuarterName" runat="server"  CssClass="tdinput" Width="95%"></asp:TextBox>
                                            <input type="hidden" id="hidSuperQuarter" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">岗位名称<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtQuarterName" runat="server" Width="96%" MaxLength="25" CssClass="tdinput" onblur="GetPYShort();"  SpecialWorkCheck="岗位名称"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">拼音代码<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtPYShort" runat="server" CssClass="tdinput" Width="96%"></asp:TextBox> 
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">是否关键岗位</td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlKeyFlag" runat="server">
                                                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">岗位分类</td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlQuarterType" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">描述信息</td>
                                        <td height="20" class="tdColInput"  style="width:10%">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Width="96%" CssClass="tdinput"  SpecialWorkCheck="描述信息"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" >岗位级别</td>
                                        <td height="20" class="tdColInput">
                                            <uc1:CodeType ID="ddlQuarterLevel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">启用状态<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlUsedStatus" runat="server">
                                                <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">附件</td>
                                        <td height="20" class="tdColInput" style="width:10%">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div id="divUploadAttachment" runat="server">
                                                            <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                                                        </div>
                                                        <div id="divDealAttachment" runat="server" style="display:none;">
                                                            <a href="#" onclick="DealAttachment('download');">  <span id='spanAttachmentName' runat="server"></span></a>
                                                            <a href="#" onclick="DealAttachment('clear');">删除附件</a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                      
                                        </td>     
                                        <td height="20" align="right" class="tdColTitle">岗位说明书模块</td>
                                        <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="QuterModelSelect"  runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="QuterModelSelect_SelectedIndexChanged" ></asp:DropDownList>
                                        
                                            <asp:TextBox ID="txtNothing" Enabled="false" runat="server" MaxLength="100" Width="95%" CssClass="tdinput" Visible="false" ></asp:TextBox></td>
                                    </tr>
                                 
                                            <tr>
                                        <td height="20" align="right" class="tdColTitle" width="8%">开启关联模块  </td>
                                        
                                        <td height="20"   class="tdColTitle"   colspan="5"  style =" text-align:center">是否开启所属类型</td>
                                       
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chMMubiao" runat="server" Text="目标管理模块"          
                                                oncheckedchanged="chMMubiao_CheckedChanged"     />   </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">
                                            <asp:CheckBox  ID="chMRi" runat="server" Text="日目标"   oncheckedchanged="chMRi_CheckedChanged"      />    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMZhou" runat="server" Text="周目标"    oncheckedchanged="chMRi_CheckedChanged" />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMYue" runat="server" Text="月目标"   oncheckedchanged="chMRi_CheckedChanged"/>   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMJi" runat="server" Text="季目标"     oncheckedchanged="chMRi_CheckedChanged"/>   </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMNian" runat="server" Text="年目标"   oncheckedchanged="chMRi_CheckedChanged"/>   </td>
                                    </tr>
                                   <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chRRenWu" runat="server" Text="任务管理模块"   
                                                oncheckedchanged="chRRenWu_CheckedChanged" />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center"><asp:CheckBox  ID="chRGEren" runat="server" Text="个人任务"     oncheckedchanged="chRZhipai_CheckedChanged"/>    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> 
                                            <asp:CheckBox  ID="chRZhipai" runat="server" Text="指派任务"   
                                                oncheckedchanged="chRZhipai_CheckedChanged" />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%"><asp:CheckBox  ID="chGgongzuo" runat="server" Text="工作日志模块" />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">&nbsp;   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> &nbsp;    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                       <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%"><asp:CheckBox  ID="chCricheng" runat="server" Text="日程管理模块"/>    </td>
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
                                            <asp:TextBox ID="txtDuty" TextMode="MultiLine" MaxLength="1024" Width="95%" CssClass="tdinput" runat="server"    Height="54px"  SpecialWorkCheck="岗位职责"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">任职资格</td>
                                        <td height="20" colspan="5" class="tdColInput">
                                            <asp:TextBox ID="txtDutyRequire" TextMode="MultiLine" MaxLength="1024" Width="95%" CssClass="tdinput" runat="server"   Height="54px"   SpecialWorkCheck="任职资格"></asp:TextBox>
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
    
 
    
      <input type="hidden" id="hidSuper" runat="server" />
        
    <uc1:Message ID="msgError" runat="server" />
    <a name="DetailListMark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>