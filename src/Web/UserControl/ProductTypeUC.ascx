<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductTypeUC.ascx.cs"
    Inherits="UserControl_ProductTypeUC" %>
    <div id="divProductType" style="display:none" >
        <%--<table width="100%">
            <tr>
                <td>
                    <button onclick="popProductTypeObj.Fill();">确定</button>
                    <button onclick="popProductTypeObj.CloseList();">取消</button>
                </td>
            </tr>
        </table>--%>
        <%--<table>
            <tr>
                <input name="txtID" id="txtID" maxlength="50"  runat="server"  type="text" class="tdinput" size = "13" />
                <input name="txtName" id="txtName" maxlength="50" runat="server"  type="text" class="tdinput" size = "13" /></td>
            </tr>
        </table>--%>
        <asp:TreeView ID="ProductTypeTree" runat="server" Width="132px" 
            ShowLines="True" 
            onselectednodechanged="ProductTypeTree_SelectedNodeChanged" >
        </asp:TreeView>
    </div>

<script type="text/javascript">
var popProductTypeObj = new Object();

popProductTypeObj.InputObjID = null;
popProductTypeObj.InputObjName = null;



popProductTypeObj.ShowList = function(objInputID,objInputName)
{
    popProductTypeObj.InputObjID = objInputID;
    popProductTypeObj.InputObjName = objInputName;
    document.getElementById('divProductType').style.display='block';
}

popProductTypeObj.CloseList = function()
{
    document.getElementById('divProductType').style.display='none';
}

popProductTypeObj.Fill = function(id,name)
{
    document.getElementById(popProductTypeObj.InputObjID).value = id;
    document.getElementById(popProductTypeObj.InputObjName).value = name;
document.getElementById('divProductType').style.display='none';
}


popProductTypeObj.SelectedNodeChanged = function(id,name)
{
    popProductTypeObj.Fill(id,name);
}



</script>

