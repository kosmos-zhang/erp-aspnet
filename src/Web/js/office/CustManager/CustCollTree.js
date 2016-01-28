
//获取树数据
var xmlHttp;
var ShowID;
var ParID;
var PrefixID;
var glbBomID;


/*初始化*/
$(document).ready(function() {

    /*加载Cust树*/
    Call(0, 0, 'TreeBomNo');
    
    //其他页面返回时，调用CustColligate.js中的方法
    requestobj = GetRequest();
    var Ltype = requestobj['ListType'];//列表种类
    var custID = requestobj['custID'];
    var custNo = requestobj['custNo'];
    if(typeof(Ltype) != "undefined")
    {
         SearchData(Ltype,custID,custNo);
    }
   

});


/*菜单切换*/
function MenuChange(objMenu)
{
     Call(0,0,'TreeBomNo');
}

function  Call(ID,Type,TreeType)
{
  GetData(ID,Type,TreeType);
  ShowID.style.display = "block";
}

/*获取数据*/
function GetData(ID,Type,TreeType)
{
    try
    {
        if(ID>0)
        {           
            LoadBomInfo(ID);//获取客户详细信息
        }
        glbBomID = ID;
        ParID  = document.getElementById("M1_" + ID);
        ShowID = document.getElementById("M2_" + ID);
        PrefixID = document.getElementById("P_" + ID);
        if(ShowID.innerHTML != "")
        {
            ShowID.innerHTML="";
//            if(ShowID.style.display == "none")
//            {
//                ShowID.style.display = "block";
//            }
//           else  if(ShowID.style.display == "block")
//            {
//                ShowID.style.display = "none";
//            } 
            ShowFolderICO();
            return;
        }

        ShowID.style.display = "block";
        ShowID.innerHTML = "<span class=\"load\">Loading…</span>";
         var Url = "../../../Handler/Office/CustManager/CustTree.ashx?ID=" + ID+"&Type="+Type+"&action=TreeShow&TreeType="+TreeType;
        createXMLHttpRequest();
        xmlHttp.onreadystatechange = ShowTree;
        xmlHttp.open("post", Url, true);
        xmlHttp.send(null);
    }
    catch(e){}
}
/*此方法直接从服务器端返回HTML*/
function ShowTree()
{
    if (xmlHttp.readyState == 4)
    {
        if (xmlHttp.status == 200 || xmlHttp.status == 500)
        {
            ShowID.innerHTML = xmlHttp.responseText;
            ShowFolderICO();
        }
        else
        {
            ShowID.innerHTML = "数据获取错误！"+xmlHttp.status;
        }
    }
}

/*创建对象*/
function createXMLHttpRequest()
{
    xmlHttp = false;
    xmlhttpObj = ["MSXML2.XmlHttp.5.0","MSXML2.XmlHttp.4.0","MSXML2.XmlHttp.3.0","MSXML2.XmlHttp","Microsoft.XmlHttp"];
    if(window.XMLHttpRequest)
    {
        xmlHttp = new XMLHttpRequest();
    }
    else if(window.ActiveXObject)
    {
        for(i=0;i<xmlhttpObj.length;i++)    
        {
            xmlHttp = new ActiveXObject(xmlhttpObj[i]);
            if(xmlHttp){break;}
        }
    }
    else
    {
        alert("暂时不能创建XMLHttpRequest对象");
    }
}

/*改变化文件夹图标打开/关闭状态*/
function ShowFolderICO()
{
    if (ParID != null)
    {    
        if (ParID.className == "folder_close")
        {
            ParID.className = "folder_open";
            PrefixID.innerHTML='<a href="#this" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/Tminus.gif" border="0" ></a>'+
                                            '<a href="javascript:void(0);" onclick="GetData('+glbBomID+',1)" ><img src="../../../Images/open.gif" border="0"></a>';
        }
        else if (ParID.className == "folder_open")
        {
            ParID.className = "folder_close";
            PrefixID.innerHTML='<a href="#this" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/Tplus.gif" border="0"></a>'+
                                            '<a href="javascript:void(0);" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/close.gif" border="0"></a>';
        }
        else if (ParID.className == "folder_close_end")
        {
            ParID.className = "folder_open_end";
            PrefixID.innerHTML='<a href="#this" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/Lminus.gif" border="0"></a>'+
                                            '<a href="javascript:void(0);" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/open.gif" border="0" ></a>';
        }
        else if (ParID.className == "folder_open_end")
        {
            ParID.className = "folder_close_end";
            PrefixID.innerHTML='<a href="#this" onclick="GetData('+glbBomID+',1)"><img src="../../../Images/Lplus.gif" border="0"></a>'+
                                            '<a href="javascript:void(0);"  onclick="GetData('+glbBomID+',1)"><img src="../../../Images/close.gif" border="0"></a>';
        }
    }
}

//加载客户相信信息
function LoadBomInfo(custID,custNo,custName)
{
     if(typeof(custID) == "undefined" || typeof(custNo) == "undefined")//typeof(requestparam1)!="undefined"
     {
        return;
     }
     else
     {     
        //显示客户div,及按钮样式
        document.getElementById("div_da").style.display = "block";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
        document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        
        SwitchList("1");
        
         document.getElementById("hidCustID_Tree").value = custID;
         document.getElementById("hidCustNo_Tree").value = custNo;
         
         if(typeof(custName) != "undefined")
         {
            document.getElementById("txtUcCustName").value = custName;
         }         
         document.getElementById("hfCustID").value = custID;
         //alert(custName);
         
      
        $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/Office/CustManager/CustTelShow.ashx',//目标地址
       data:'action=LoadCustInfo&ID='+custID+'&CustNo='+custNo,
       cache:false,
       beforeSend:function(){},//发送数据之前
           success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#txtCustNo").val(item.CustNo);                        
                        $("#txtCustName").val(item.CustName);
                        $("#txtCustBig").val(item.CustBig);                       
                        $("#txtCustTypeManage").val(item.CustTypeManage);                        
                        $("#txtCustTypeManage").val(item.CustTypeManage);
                        $("#txtCustTypeSell").val(item.CustTypeSell);
                        $("#txtCreditGrade").val(item.CreditGrade);
                        $("#txtCustTypeTime").val(item.CustTypeTime);
                        $("#txtCustClass").val(item.CustClass);
                        
                        $("#txtCustType").val(item.CustType);
                        $("#txtCustNote").val(item.CustNote);
                        $("#txtCreator").val(item.Creator);
                        $("#txtCreatDate").val(item.CreatDate);
                        $("#txtCountryID").val(item.CountryID);
                        $("#txtAreaID").val(item.AreaID);
                        $("#txtBusiType").val(item.BusiType);
                        $("#txtManager").val(item.Manager);
                        $("#txtMobile").val(item.Mobile);
                        $("#txtFax").val(item.Fax);
                        $("#txtTel").val(item.Tel);
                        $("#txtLinkCycle").val(item.LinkCycle);
                        $("#txtReceiveAddress").val(item.ReceiveAddress);
                        $("#txtCreditManage").val(item.CreditManage);
                        $("#txtMaxCredit").val(item.MaxCredit);
                        $("#txtMaxCreditDate").val(item.MaxCreditDate);
                        $("#txtPayType").val(item.PayType);
                        $("#txtCanUserName").val(item.CanUserName);
                        $("#txtTotalPrice").val(item.TotalPrice);
                        
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
       
    }
       
       
      

}

