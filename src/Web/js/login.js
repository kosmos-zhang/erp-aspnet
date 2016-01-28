
function ReadSNArray(reader,sp)
{   

    var s = "";
    
    try
    {
        s=reader.ReadUsbSn();
    }catch(e){
        return "";
    }
    
	if(s + "" == "")
	{		
		return "";
	}


	var usbList = s.split("\\\\?\\usb");
	var usbStringList = "";
	for(var i=1;i<usbList.length;i++)
	{		
		var s2 = usbList[i].substring(1,usbList[i].length-39);		
		var s1 = s2.replace("vid_","").replace("pid_","").replace(/\#/g,"").replace(/\&/g,"");
		if(usbStringList != "")
			usbStringList  += sp;
		usbStringList  += s1;
	}
	
	return usbStringList;
	
	
}

/*
* 提交验证
*/
function LoginSubmit() {

var USBSNReader  = document.getElementById("USBSNReaderOCX1");
var snlist = ReadSNArray(USBSNReader,",");
if(snlist == "")
{
     //alert("没有读取到USBKEY");
    // return;
}

//alert(snlist);

    var errorMsg = "";
    UserID = document.getElementById("txtUserID").value;
    //用户名必须输入
    if (UserID == "")
    {
        errorMsg += "请输入用户名\n";
    }
    
    if(!UserID.match(/^[\u4e00-\u9fa5A-Za-z0-9]+$/))
    {
        errorMsg += "不合法的账号名称，只能包含汉字，英文，数字";
    }
    
    
    Password = document.getElementById("txtPassword").value;
    //密码必须输入
    if (Password == "")
    {
        errorMsg += "请输入密码\n";
    }
    CheckCode = document.getElementById("txtCheckCode").value;
    //验证码必须输入
    if (CheckCode == "")
    {
        errorMsg += "请输入验证码";
    }
    else if (CheckCode.length != 4)
    {
        errorMsg += "请输入四位的验证码";
    }
    //如有项目未输入，输出错误信息
          
    if (errorMsg != "")
    {
        alert(errorMsg);
        return;
    }
    

         
    //拼写请求URL参数
    var postParams = "UserID=" + UserID + "&Password=" + Password + "&CheckCode=" + CheckCode+"&snlist="+snlist;
//var login_start_date = new Date();
    $.ajax({
        type: "POST",
        url: "Handler/Login.ashx?" + postParams,
        dataType: 'string', //返回json格式数据
        data: '',
        cache: false,
        success: function(data) {

            // var login_end_date = new Date();

            //  alert(login_end_date-login_start_date);//12 625

            // alert(data);       
            var checkError = data.split("|");
            if (checkError.length == 2) {
                document.getElementById(checkError[0]).focus();
                alert(checkError[1]);
            } else if (data.length > 0) {
                alert("登陆失败，请联系管理员");
            }
            else {
                //                window.open('Main.aspx','','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes');
                //                
                //                window.opener=null;
                //                window.open("","_self");
                //                window.close();


                //登陆成功,记住用户名和密码
                //记录用户名
                if (document.getElementById("chkUsername").checked) {
                    Cookie.set("chkERPUsername", UserID, 60 * 24 * 14);
                } else {
                    Cookie.del("chkERPUsername");
                }

                //记录用户密码
                if (document.getElementById("chkPassword").checked) {
                    Cookie.set("chkERPPassword", Password, 60 * 24 * 14);
                } else {
                    Cookie.del("chkERPPassword");
                }



                SetWindowOpen(1);




            }
        },
        error: function(r) {
            alert(r.responseText);
            alert("登陆失败，请联系管理员");
        }
    });    
}

function SetWindowOpen(type) 
{
    var objChild;                          
    var reWork = new RegExp('object','gi'); // Regular expression
    try 
    {
        objChild = window.open('Main.aspx','','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes'); 
        window.opener=null;
        window.open("","_self");                    
        window.close();        
    }
    catch(e){}

    if(!reWork.test(String(objChild)))
    {
        alert('您的IE设置了弹出窗口屏蔽功能，请通过IE设置允许本系统弹出窗口。\n\n设置步骤:IE菜单栏的“工具”---“弹出窗口阻止程序”---点击“关闭弹出窗口阻止程序”');
        window.location.href='login.aspx?flag=1';
    }      
}

/*
* 重新获取验证码
*/
function ReloadCheckCode() {
    var checkCode = document.getElementById("imgCheckCode");
    var rand = Math.random();
    checkCode.src = "CheckCode.aspx?randnum=" + rand;
}

document.onkeydown = KeyDown;

/*
*
*/
function KeyDown()
{
    var event = arguments[0]||window.event;  
    
    //获取按下键的值
    var keyCode = event.charCode||event.keyCode;
    
    //按回车时提交
    if (keyCode == 13)
    {
       // document.getElementById("btnLogin").click();
       LoginSubmit();
    }
}

$(document).ready(function() {

    if (Cookie.get("chkERPUsername") != null) {
        document.getElementById("txtUserID").value = Cookie.get("chkERPUsername");
        document.getElementById("chkUsername").checked = true;
    };

    if (Cookie.get("chkERPPassword") != null) {
        document.getElementById("txtPassword").value = Cookie.get("chkERPPassword");
        document.getElementById("chkPassword").checked = true;

    };
}); 


