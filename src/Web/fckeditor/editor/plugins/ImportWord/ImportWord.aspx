<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ImportWord.aspx.cs" Inherits="Fck.CustomButton.ImportWord.ImportWord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
    <script src="../../dialog/common/fck_dialog_common.js" type="text/javascript"></script>

<script type="text/javascript">

var oEditor = window.parent.InnerDialogLoaded() ;

// Gets the document DOM
var oDOM = oEditor.FCK.EditorDocument ;

var ImportOk = false;

// Fired when the window loading process is finished. It sets the fields with the
// actual values if a table is selected in the editor.
window.onload = function()
{
	//oEditor.FCKLanguageManager.TranslatePage(document);

	window.parent.SetOkButton( true ) ;
	window.parent.SetAutoSize( true ) ; 
}

// Fired when the user press the OK button
function Ok()
{
    if(!ImportOk)
    {
        if(document.getElementById("fileWord").value == "")
        {
            alert("请先选择Word文件。");
            return false;
        }
        else
        {
            alert("请单击“开始导入”按钮。");
            return false;
        }
    }
    
    var oActiveEl = oEditor.FCK.EditorDocument.createElement( 'SPAN' ) ; 
    oActiveEl.innerHTML = document.getElementById('TextArea1').value ; 
    oEditor.FCKUndo.SaveUndoStep() ; 
    oActiveEl = oEditor.FCK.InsertElement( oActiveEl ) ;

    // Get the editor instance that we want to interact with.
//    var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');

//    // Check the active editing mode.
//    if (oEditor.EditMode == FCK_EDITMODE_WYSIWYG) {
//        // Insert the desired HTML.
//        oEditor.InsertHtml(document.getElementById('TextArea1').value);
//    }
//    else
//        alert('You must be on WYSIWYG mode!');


	return true;
}
</script>

</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:Panel ID="pnlUpload" runat="server">
        请选择您要导入的word文件：        
        <div style="margin:10px">
            <input id="fileWord" runat="server" type="file" style="width: 300px"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileWord"
                Display="Dynamic" ErrorMessage="您还没有选择要上传的Word文档。" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fileWord"
                Display="Dynamic" ErrorMessage="您选择的文件不是.doc(Word)文档。" SetFocusOnError="True"
                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.DOC)$"></asp:RegularExpressionValidator><br />
        </div>
    
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="开始导入" />    
    </asp:Panel>      
    <asp:Panel ID="pnlOk" runat="server" Visible="False">
        Word文件已经导入成功，单击“确定”导入到编辑器中，单击“取消”将放弃导入操作。
        <script type="text/javascript">ImportOk = true;</script>
    </asp:Panel>
    
    <div style="display:none">
   
        <textarea id="TextArea1" style="width: 648px; height: 220px" runat="server" cols="500"></textarea><br />
        <textarea id="Textarea2" style="width: 648px; height: 201px" runat="server"></textarea>
    </div>
        
    </form>
</body>
</html>
