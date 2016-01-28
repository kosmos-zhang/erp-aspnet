<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptInfo_Info.aspx.cs" Inherits="Pages_Office_HumanManager_DeptInfo_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/DeptInfo_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
 

    <br />
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#F0F0F0" class="checktable" id="tblDetailList" >
      <tr>
        <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /> </td>
      </tr>
      <tr>
        <td height="30" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />组织机构设置</span></td>
      </tr>
      <tr>
        <td height="2"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC" >
        <tr>
              <td bgcolor="#FFFFFF"><table width="100%"border="0" cellpadding="0" cellspacing="0"   id="mainindex">
                <tr>
                  <td width="350" align="left"  valign="top" bgcolor="#FFFFFF" class="Blue" ><input type="hidden" id="hidSelectValue" />
                      <div>
                        <table width="98%" >
                          <tr>
                            <td><%--    <a href="#" onclick="SetSelectValue('','','');">组织机构 </a>--%>
                                <div runat="server" id="divCompany"></div></td>
                          </tr>
                          <tr>
                            <td><table>
                                <tr>
                                  <td>&nbsp;</td>
                                  <td><div id="divDeptTree" style="overflow-x:auto;overflow-y:auto;height:500px;width:300px;">正在加载数据,请稍等......</div></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                      </div></td>
                  <td align="left" valign="top" bgcolor="#FFFFFF"><table width="99%" border="0" style="margin-right:10%;margin-top:5%;" align="center" cellpadding="0" cellspacing="0" >
                      <tr>
                        <td width="20%" class="Blue">选择的组织机构：</td>
                        <td align="left"><div id="divSelectName" style="text-align:left;"></div></td>
                      </tr>
                      <tr>
                        <td colspan="2" height="20"></td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand"  onclick="DoEditDept('3');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/cw_tjtj.jpg" alt="添加同级" visible="false" id="btnAddSame" runat="server" style="cursor:hand"  onclick="DoEditDept('0');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/cw_xj.jpg" alt="添加下级" visible="false" id="btnAddSub" runat="server" style="cursor:hand"  onclick="DoEditDept('1');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/Show_edit.jpg" alt="修改" visible="false" id="btnModify" runat="server" style="cursor:hand"  onclick="DoEditDept('2');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../images/Button/Show_del.jpg" alt="删除" visible="false" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'   /> </td>
                      </tr>
                  </table></td>
                </tr>
              </table></td>
          </tr>
        </table>
        <br /></td>
      </tr>
    </table>
    <br />    
    <br />
    <br />
      
  <div id="divEditDept" runat="server" style="background: #fff; padding: 10px; width: 850px; z-index:1; position: absolute;top: 20%; left: 15%; display:none    ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblDeptInfo">
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
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"  onclick="DoSaveInfo();"/>
                                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回"  visible="true" id="btnBack" runat="server" style="cursor:hand"   onclick="DoBack();"/>
                                         </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                            <input type="hidden" id="hidModuleID" runat="server" />
                                            <input type="hidden" id="hidSearchCondition" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblBaseInfo" style="display:block">
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="8%">机构编号<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="30%">
                                            <div id="divCodeRule" runat="server">
                                                <uc1:CodingRule ID="codeRule" runat="server" />
                                            </div>
                                            <div id="divCodeNo" runat="server" class="tdinput">
                                                <input type="text" id="txtDisplayCode" class="tdinput" maxlength="50" />
                                            </div>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="8%">上级机构</td>
                                        <td height="20" class="tdColInput" width="22%">
                                            <asp:TextBox ID="DeptTxtSuperName" runat="server"  ReadOnly ="true"  CssClass="tdinput" Width="100%"  onclick="alertdiv('DeptTxtSuperName,txtSuperDeptID');"></asp:TextBox>
                                            <input type="hidden" id="txtSuperDeptID" runat="server" />
                                             <input type="hidden" id="txtSuperHistroyID" runat="server" />
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="8%">独立核算</td>
                                        <td height="20" class="tdColInput" width="22%">
                                            <asp:DropDownList ID="ddlAccountFlag" runat="server">
                                                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" width="8%">机构名称<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <asp:TextBox ID="txtDeptName" runat="server" MaxLength="25" CssClass="tdinput" onblur="GetPYShort();" Width="99%"  SpecialWorkCheck="机构名称"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle" width="8%">拼音代码<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <asp:TextBox ID="txtPYShort" runat="server"  CssClass="tdinput"  MaxLength ="50"></asp:TextBox> 
                                        </td>
                                       <td height="20" align="right" class="tdColTitle">启用状态<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlUsedStatus" runat="server">
                                                  <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                                  <asp:ListItem Value="0" Text="停用"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle" >是否分公司</td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlSubFlag" runat="server" >
                                                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">是否分店</td>
                                        <td height="20" class="tdColInput">
                                            <asp:DropDownList ID="ddlSaleFlag" runat="server">
                                                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                             <td height="20" align="right" class="tdColTitle">Email</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="100%" CssClass="tdinput" SpecialWorkCheck="Email"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">地址</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtAddress" runat="server" Width="99%" MaxLength="50" CssClass="tdinput" SpecialWorkCheck="地址"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">邮编</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtPost" runat="server" Width="99%" MaxLength="6" CssClass="tdinput" SpecialWorkCheck="邮编"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">联系人</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtLinkMan" runat="server" Width="99%" MaxLength="10" CssClass="tdinput" SpecialWorkCheck="联系人"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="tdColTitle">电话</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtTel" runat="server" Width="99%" MaxLength="20" CssClass="tdinput" SpecialWorkCheck="电话"></asp:TextBox>
                                        </td>
                                        <td height="20" align="right" class="tdColTitle">传真</td>
                                        <td height="20" class="tdColInput">
                                            <asp:TextBox ID="txtFax" runat="server" Width="99%" MaxLength="20" CssClass="tdinput" SpecialWorkCheck="传真"></asp:TextBox>
                                        </td>
                                     <td height="20" align="right" class="tdColTitle"> </td>
                                        <td height="20" class="tdColInput">
                                      
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                     <td height="20" align="right" class="tdColTitle" >描述信息</td>
                                        <td height="20" class="tdColInput" colspan="5">
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="199" Width="100%" CssClass="tdinput"  TextMode="MultiLine"  Height="54px" SpecialWorkCheck="描述信息"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                         <td height="20" align="right" class="tdColTitle">机构职责</td>
                                        <td height="20" class="tdColInput" colspan="5">
                                            <asp:TextBox ID="txtDuty" runat="server" Width="100%" MaxLength="99" CssClass="tdinput"  TextMode="MultiLine"  Height="54px" SpecialWorkCheck="机构职责"></asp:TextBox>
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
    
    <uc1:Message ID="msgError" runat="server" />
    <a name="DetailListMark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>