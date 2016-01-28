<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuterTem.aspx.cs" Inherits="Pages_Office_HumanManager_QuterTem" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>岗位说明书模板设置页面</title>
        <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
  <%--  <script src="../../../js/office/HumanManager/DeptQuarter_Query.js" type="text/javascript"></script>--%>
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

	</script>
</head>
<body>
    <form id="form1" runat="server">
    
    
    
    
<div id="divEditDeptQuarter" runat="server" style="background: #fff; padding: 10px; width: 850px; z-index:1; position: absolute;top: 20%; left: 15%;       ">    
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
                            
                          <%--      <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" visible="true" id="btnBack" runat="server" style="cursor:hand"  onclick="DoBack();"/>--%>
                                
                                <asp:ImageButton ID="btnSave" runat="server" 
                                    ImageUrl="../../../Images/Button/Bottom_btn_save.jpg" 
                                    onclick="imbSave_Click" style="height: 24px"   />
                               
                               <asp:Label ID="lblErrorMes" runat="server"  ForeColor="Red" Width="400px"  Visible="true"></asp:Label>
                           
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
                                        <td height="20" align="right" class="tdColTitle" width="8%">   岗位说明书名称</td>
                                        
                                        <td height="20"   class="tdColTitle"   colspan="5"  style =" text-align:left; vertical-align:top">    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        
                                        </td>
                                       
                                    </tr>
                         <tr>
                                        <td height="20" align="right" class="tdColTitle" width="8%">   已保存的模板</td>
                                        
                                        <td height="20"   class="tdColTitle"   colspan="5"  style =" text-align:left; vertical-align:top"> (选择可用于修改，不选择模板，则用于新建)     <asp:DropDownList ID="QuterModelSelect"  runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="QuterModelSelect_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:Button ID="BtnRead" runat="server" Text="读取模块" onclick="BtnRead_Click" />                             
                                                           <asp:Button ID="BtnDelete"   runat="server" Text="删除模块" 
                                                onclick="BtnDelete_Click"  />
                                        
                                        </td>
                                       
                                    </tr>
                         <tr>
                                        <td height="20" align="right" class="tdColTitle" width="8%">开启关联模块  </td>
                                        
                                        <td height="20"   class="tdColTitle"   colspan="5"  style =" text-align:center">是否开启所属类型</td>
                                       
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chMMubiao" runat="server" Text="目标管理模块"    />   </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center">
                                            <asp:CheckBox  ID="chMRi" runat="server" Text="日目标"   />    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMZhou" runat="server" Text="周目标"    />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMYue" runat="server" Text="月目标"   />   </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> <asp:CheckBox  ID="chMJi" runat="server" Text="季目标"   />   </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">  <asp:CheckBox  ID="chMNian" runat="server" Text="年目标"   />   </td>
                                    </tr>
                                   <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%">
                                            <asp:CheckBox  ID="chRRenWu" runat="server" Text="任务管理模块"    />    </td>
                                        <td height="30" class="tdColInput" width="15%"   align="center"><asp:CheckBox  ID="chRGEren" runat="server" Text="个人任务"    />    </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center"> 
                                            <asp:CheckBox  ID="chRZhipai" runat="server" Text="指派任务"    />    </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">   &nbsp;  </td>
                                        <td height="20"  class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                        <td height="20" class="tdColInput" width="15%"  align="center">&nbsp;     </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="10%"><asp:CheckBox  ID="chGgongzuo" runat="server" Text="工作日志模块"/>    </td>
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
                            <td  colspan="6">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                              
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                                    
                             
                                    <tr>
                                    <td colspan="6">
                                       <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server"  Height="700px">

        </FCKeditorV2:FCKeditor></td>
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
    </form>
</body>
</html>
