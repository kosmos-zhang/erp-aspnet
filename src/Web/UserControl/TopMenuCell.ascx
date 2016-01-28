<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopMenuCell.ascx.cs" Inherits="UserControl_TopMenuCell" %>


<div id="<%=UniqueID %>">
</div>    
<script type="text/javascript">
   swfobject.embedSWF("<%=FlashPath %>", "<%=UniqueID %>", "70", "85", "8.0.0", "expressInstall.swf");
</script>

       