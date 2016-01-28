//************************全局变量begin*****************************//

var CustID = "";//客户ID
var CustNo = "";
var Tel = "";//联系电话
var CreatedBegin = "";//开始日期
var CreatedEnd = "";//结束日期
//档案
//var pageIndex;
var pageCount_da = 10;//每页计数
var totalRecord_da = 0;
var pagerStyle = "flickr";//jPagerBar样式
//var currentPageIndex = 1;
var orderby_da = "";//排序字段

//客户联系人
var pageCount_lxr = 10;//每页计数
var totalRecord_lxr = 0;
var orderby_lxr = "";//排序字段

//客户联络
var pageCount_ll = 10;//每页计数
var totalRecord_ll = 0;
var orderby_ll = "";//排序字段

//客户洽谈
var pageCount_qt = 10;//每页计数
var totalRecord_qt = 0;
var orderby_qt = "";//排序字段

//客户关怀
var pageCount_gh = 10;//每页计数
var totalRecord_gh = 0;
var orderby_gh = "";//排序字段

//客户服务
var pageCount_fw = 10;//每页计数
var totalRecord_fw = 0;
var orderby_fw = "";//排序字段

//客户投诉
var pageCount_ts = 10;//每页计数
var totalRecord_ts = 0;
var orderby_ts = "";//排序字段

//客户建议
var pageCount_jy = 10;//每页计数
var totalRecord_jy = 0;
var orderby_jy = "";//排序字段

//来电记录
var pageCount_ld = 10;//每页计数
var totalRecord_ld = 0;
var orderby_ld = "";//排序字段

//购买记录
var pageCount_gm = 10;//每页计数
var totalRecord_gm = 0;
var orderby_gm = "";//排序字段

//发货记录
var pageCount_fh = 10;//每页计数
var totalRecord_fh = 0;
var orderby_fh = "";//排序字段

//回款计划
var pageCount_hk = 10;//每页计数
var totalRecord_hk = 0;
var orderby_hk = "";//排序字段

//回款记录(收款单)
var pageCount_jl = 10;//每页计数
var totalRecord_jl = 0;
var orderby_jl = "";//排序字段

//销售机会
var pageCount_jh = 10;//每页计数
var totalRecord_jh = 0;
var orderby_jh = "";//排序字段


var PageType = "1";
//************************全局变量end*****************************//

function SwitchList(ide)
{
     switch(ide)
    {
        case "1"://客户档案显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        break;
        
        case "2"://客户联系人显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";      
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        break;
        
         case "3"://客户联络显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";     
        break;
        
         case "4"://客户洽谈显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
        break;
        
         case "5"://客户关怀显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
        break;
        
         case "6"://客户服务显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
        break;
        
        case "7"://客户投诉显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";     
        break;
        
        case "8"://客户建议显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        break;
        
        case "9"://客户来电显示        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";    
        break;
        
        case "10"://购买记录        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";   
        break;
        
        case "11"://发货明细        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";     
        break;
        
         case "12"://回款计划        
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";  
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";    
        break;
        
        case "13"://回款记录
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")"; 
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";     
        break;
        
        case "14"://回款记录
        document.getElementById("btn_da").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_lxr").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ll").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_qt").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_gh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fw").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ts").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jy").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_ld").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")"; 
         document.getElementById("btn_gm").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_fh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_hk").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";
        document.getElementById("btn_jl").style.backgroundImage =  "url(" + "../../../Images/default/btnbg2.jpg" + ")";      
        document.getElementById("btn_jh").style.backgroundImage =  "url(" + "../../../Images/default/btnbg.gif" + ")";      
        break;
    }
}

function ShowList(idex)
{
    SwitchList(idex);//对应按钮改变样式
    
    CustID = document.getElementById("hidCustID_Tree").value;//客户ID
    CustNo = document.getElementById("hidCustNo_Tree").value;//客户No
    //alert(CustID+"***"+CustNo);

    switch(idex)
    {
        case "1"://客户档案显示
        var custID = document.getElementById("hidCustID_Tree").value;
        var custNo = document.getElementById("hidCustNo_Tree").value; 
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
        LoadBomInfo(custID,custNo);
        break;
        
        case "2"://客户联系人显示
        Cust_lxr(1);//检索客户联系人
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "block";
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
        break;
        
         case "3"://客户联络显示
         Cust_ll(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "block";
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
        break;
        
        case "4"://客户洽谈显示
        Cust_qt(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "block";
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
        break;
        
        case "5"://客户关怀显示
        Cust_gh(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "block";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "6"://客户服务显示
        Cust_fw(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "block";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "7"://客户投诉显示
        Cust_ts(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "block";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
         case "8"://客户建议显示
         Cust_jy(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "block";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "9"://客户来电记录显示
         Cust_ld(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "block";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "10"://购买记录显示
        Cust_gm(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "block";
        document.getElementById("div_fh").style.display = "none";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
         case "11"://发货记录显示
         Cust_fh(1);
        document.getElementById("div_da").style.display = "none";
        document.getElementById("div_lxr").style.display = "none";
        document.getElementById("div_ll").style.display = "none";
        document.getElementById("div_qt").style.display = "none";
        document.getElementById("div_gh").style.display = "none";
        document.getElementById("div_fw").style.display = "none";
        document.getElementById("div_ts").style.display = "none";
        document.getElementById("div_jy").style.display = "none";
        document.getElementById("div_ld").style.display = "none";
         document.getElementById("div_gm").style.display = "none";
        document.getElementById("div_fh").style.display = "block";
        document.getElementById("div_hk").style.display = "none";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "12"://回款计划列表显示
         Cust_hk(1);
        document.getElementById("div_da").style.display = "none";
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
        document.getElementById("div_hk").style.display = "block";
        document.getElementById("div_jl").style.display = "none";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "13"://收款单列表显示
         Cust_jl(1);
        document.getElementById("div_da").style.display = "none";
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
        document.getElementById("div_jl").style.display = "block";
        document.getElementById("div_jh").style.display = "none";
        break;
        
        case "14"://销售机会列表显示
         Cust_jh(1);
        document.getElementById("div_da").style.display = "none";
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
        document.getElementById("div_jh").style.display = "block";
        break;
        
    }
}

//返回时，判断显示对应列表
function SearchData(ListType,custID,custNo)
{
    document.getElementById("hidCustID_Tree").value = custID;//客户ID
    document.getElementById("hidCustNo_Tree").value = custNo;//客户No
   
    ShowList(ListType); 
}

//***********************客户联系人js Begin****************************************

function Cust_lxr(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_lxr="+pageIndex+"&pageCount_lxr="+pageCount_lxr+"&action=Cust_lxr&orderby_lxr="+orderby_lxr+
                    "&CustID="+escape(CustID)+"&CustNo="+escape(CustNo)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_lxr").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_lxr tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {                      
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelLinkMan('"+item.ID+"')>" + item.LinkManName + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>" + SubValue(12,item.CustName) + "</td>"+
                            "<td height='22' align='center'>"+ item.LinkTypeName +"</td>"+
                            "<td height='22' align='center'>" + item.Important + "</td>"+
                            "<td height='22' align='center'>" + item.WorkTel + "</td>"+
                            "<td height='22' align='center'>" + item.Handset + "</td>"+
                            "<td height='22' align='center'>" + item.Fax + "</td>"+
                            "<td height='22' align='center'>" + item.HomeTel + "</td>"+
                            "<td height='22' align='center'>" + item.QQ + "</td>"+
                            "<td height='22' align='center'>" + item.Birthday + "</td>").appendTo($("#pageDataList_lxr tbody"));
                        }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_lxr",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_lxr",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_lxr,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_lxr({pageindex});return false;"}//[attr]
                    );
                  totalRecord_lxr = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text1").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_lxr").val(pageCount_lxr);
                  ShowTotalPage(msg.totalCount,pageCount_lxr,pageIndex,$("#pagecount_lxr"));
                 
                  $("#ToPage_lxr").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_lxr").show();Ifshow_lxr(document.getElementById("Text1").value);pageDataList1_lxr("pageDataList_lxr","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_lxr(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_lxr(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_lxr").style.display = "none";
        document.getElementById("pagecount_lxr").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_lxr").style.display = "block";
        document.getElementById("pagecount_lxr").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_lxr(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_lxr-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_lxr=parseInt(newPageCount);
        Cust_lxr(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_lxr(orderColum,orderTip)
{
    if (totalRecord_lxr == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_lxr = orderColum+"_"+ordering;    
    Cust_lxr(1);
}
function SelLinkMan(linkid)
{  
    window.location.href='LinkMan_Edit.aspx?linkid='+linkid+'&Pages=CustColligate.aspx&ListType=2&custID='+CustID
    +'&CustNo='+CustNo+'&ModuleID=2021201';
}
//***********************客户联系人js End****************************************

//***********************客户联络js Begin****************************************
function Cust_ll(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_ll="+pageIndex+"&pageCount_ll="+pageCount_ll+"&action=Cust_ll&orderby_ll="+orderby_ll+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_ll").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_ll tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelContact('"+item.ID+"')>" + item.ContactNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                            "<td height='22' align='center'>" + item.LinkDate + "</td>"+                           
                            "<td height='22' align='center'>" + item.LinkManName + "</td>").appendTo($("#pageDataList_ll tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_ll",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_ll",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_ll,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_ll({pageindex});return false;"}//[attr]
                    );
                  totalRecord_ll = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text3").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_ll").val(pageCount_ll);
                  ShowTotalPage(msg.totalCount,pageCount_ll,pageIndex,$("#pagecount_ll"));
                 
                  $("#ToPage_ll").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_ll").show();Ifshow_ll(document.getElementById("Text3").value);pageDataList1_ll("pageDataList_ll","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_ll(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_ll(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_ll").style.display = "none";
        document.getElementById("pagecount_ll").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_ll").style.display = "block";
        document.getElementById("pagecount_ll").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_ll(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_ll-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_ll=parseInt(newPageCount);
        Cust_ll(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_ll(orderColum,orderTip)
{
    if (totalRecord_ll == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_ll = orderColum+"_"+ordering;    
    Cust_ll(1);
}
function SelContact(Id)
{  
    window.location.href='CustContactHistory_Add.aspx?contactid='+Id+'&Pages=CustColligate.aspx&ListType=3&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021301';
}

//***********************客户联络js End****************************************

//***********************客户洽谈js Begin****************************************
function Cust_qt(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_qt="+pageIndex+"&pageCount_qt="+pageCount_qt+"&action=Cust_qt&orderby_qt="+orderby_qt+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_qt").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_qt tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelTalk('"+item.ID+"')>" + item.TalkNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                            "<td height='22' align='center'>" + item.CompleteDate + "</td>"+ 
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                            "<td height='22' align='center'>" + item.Status + "</td>").appendTo($("#pageDataList_qt tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_qt",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_qt",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_qt,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_qt({pageindex});return false;"}//[attr]
                    );
                  totalRecord_qt = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text4").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_qt").val(pageCount_qt);
                  ShowTotalPage(msg.totalCount,pageCount_qt,pageIndex,$("#pagecount_qt"));
                 
                  $("#ToPage_qt").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_qt").show();Ifshow_qt(document.getElementById("Text4").value);pageDataList1_qt("pageDataList_qt","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_qt(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_qt(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_qt").style.display = "none";
        document.getElementById("pagecount_qt").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_qt").style.display = "block";
        document.getElementById("pagecount_qt").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_qt(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_qt-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_qt=parseInt(newPageCount);
        Cust_qt(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_qt(orderColum,orderTip)
{
    if (totalRecord_qt == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_qt = orderColum+"_"+ordering;    
    Cust_qt(1);
}
function SelTalk(Id)
{  
    window.location.href='CustTalk_Add.aspx?Talkid='+Id+'&Pages=CustColligate.aspx&ListType=4&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021401';
}

//***********************客户洽谈js End****************************************

//***********************客户关怀js Begin****************************************
function Cust_gh(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_gh="+pageIndex+"&pageCount_gh="+pageCount_gh+"&action=Cust_gh&orderby_gh="+orderby_gh+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_gh").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_gh tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelLove('"+item.ID+"')>" + item.LoveNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                            "<td height='22' align='center'>" + item.LoveDate + "</td>"+                             
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>").appendTo($("#pageDataList_gh tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_gh",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_gh",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_gh,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_gh({pageindex});return false;"}//[attr]
                    );
                  totalRecord_gh = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text5").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_gh").val(pageCount_qt);
                  ShowTotalPage(msg.totalCount,pageCount_gh,pageIndex,$("#pagecount_gh"));
                 
                  $("#ToPage_gh").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_gh").show();Ifshow_gh(document.getElementById("Text5").value);pageDataList1_gh("pageDataList_gh","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_gh(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_gh(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_gh").style.display = "none";
        document.getElementById("pagecount_gh").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_gh").style.display = "block";
        document.getElementById("pagecount_gh").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_gh(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_gh-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_gh=parseInt(newPageCount);
        Cust_gh(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_gh(orderColum,orderTip)
{
    if (totalRecord_gh == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_gh = orderColum+"_"+ordering;    
    Cust_gh(1);
}
function SelLove(Id)
{  
    window.location.href='CustLove_Add.aspx?Loveid='+Id+'&Pages=CustColligate.aspx&ListType=5&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021501';
}

//***********************客户关怀js End****************************************

//***********************客户服务js Begin****************************************
function Cust_fw(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_fw="+pageIndex+"&pageCount_fw="+pageCount_fw+"&action=Cust_fw&orderby_fw="+orderby_fw+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_fw").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_fw tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelServe('"+item.ID+"')>" + item.ServeNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.BeginDate + "</td>"+
                            "<td height='22' align='center'>" + item.LinkManName + "</td>"+                             
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>").appendTo($("#pageDataList_fw tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_fw",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_fw",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_fw,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_fw({pageindex});return false;"}//[attr]
                    );
                  totalRecord_fw = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text6").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_fw").val(pageCount_fw);
                  ShowTotalPage(msg.totalCount,pageCount_fw,pageIndex,$("#pagecount_fw"));
                 
                  $("#ToPage_fw").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_fw").show();Ifshow_fw(document.getElementById("Text6").value);pageDataList1_fw("pageDataList_fw","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_fw(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_fw(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_fw").style.display = "none";
        document.getElementById("pagecount_fw").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_fw").style.display = "block";
        document.getElementById("pagecount_fw").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_fw(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_fw-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_fw=parseInt(newPageCount);
        Cust_fw(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_fw(orderColum,orderTip)
{
    if (totalRecord_fw == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_fw = orderColum+"_"+ordering;    
    Cust_fw(1);
}

function SelServe(Id)
{  
    window.location.href='CustService_Add.aspx?serviceid='+Id+'&Pages=CustColligate.aspx&ListType=6&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021601';
}

//***********************客户服务js End****************************************

//***********************客户投诉js Begin****************************************
function Cust_ts(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_ts="+pageIndex+"&pageCount_ts="+pageCount_ts+"&action=Cust_ts&orderby_ts="+orderby_ts+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_ts").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_ts tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelComplain('"+item.ID+"')>" + item.ComplainNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.ComplainDate + "</td>"+
                            "<td height='22' align='center'>" + item.Critical + "</td>"+ 
                             "<td height='22' align='center'>" + item.EmployeeName + "</td>"+                             
                            "<td height='22' align='center'>" + item.state + "</td>").appendTo($("#pageDataList_ts tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_ts",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_ts",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_ts,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_ts({pageindex});return false;"}//[attr]
                    );
                  totalRecord_ts = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text7").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_ts").val(pageCount_ts);
                  ShowTotalPage(msg.totalCount,pageCount_ts,pageIndex,$("#pagecount_ts"));
                 
                  $("#ToPage_ts").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_ts").show();Ifshow_ts(document.getElementById("Text7").value);pageDataList1_ts("pageDataList_ts","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_ts(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_ts(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_ts").style.display = "none";
        document.getElementById("pagecount_ts").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_ts").style.display = "block";
        document.getElementById("pagecount_ts").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_ts(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_ts-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_ts=parseInt(newPageCount);
        Cust_ts(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_ts(orderColum,orderTip)
{
    if (totalRecord_ts == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_ts = orderColum+"_"+ordering;    
    Cust_ts(1);
}
function SelComplain(Id)
{
    window.location.href='CustComplain_Add.aspx?Complainid='+Id+'&Pages=CustColligate.aspx&ListType=7&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021701';
}

//***********************客户投诉js End****************************************

//***********************客户建议js Begin****************************************
function Cust_jy(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_jy="+pageIndex+"&pageCount_jy="+pageCount_jy+"&action=Cust_jy&orderby_jy="+orderby_jy+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_jy").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_jy tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=SelAdvice('"+item.ID+"')>" + item.AdviceNo + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>" + SubValue(12,item.Title) + "</td>"+
                            "<td height='22' align='center' title='"+ item.CustName +"'>"+ SubValue(12,item.CustName) +"</td>"+
                            "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>"+ 
                             "<td height='22' align='center'>" + item.Accept + "</td>"+                             
                            "<td height='22' align='center'>" + item.AdviceDate + "</td>").appendTo($("#pageDataList_jy tbody"));
                      }
                   });                   
                    //页码                    
                   ShowPageBar("pageDataList1_Pager_jy",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_jy",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_jy,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_jy({pageindex});return false;"}//[attr]
                    );
                  totalRecord_jy = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_jy").val(pageCount_jy);
                  ShowTotalPage(msg.totalCount,pageCount_jy,pageIndex,$("#pagecount_jy"));
                 
                  $("#ToPage_jy").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_jy").show();Ifshow_jy(document.getElementById("Text8").value);pageDataList1_jy("pageDataList_jy","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_jy(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_jy(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_jy").style.display = "none";
        document.getElementById("pagecount_jy").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_jy").style.display = "block";
        document.getElementById("pagecount_jy").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_jy(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_jy-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_jy=parseInt(newPageCount);
        Cust_jy(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_jy(orderColum,orderTip)
{
    if (totalRecord_jy == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_jy = orderColum+"_"+ordering;    
    Cust_jy(1);
}
function SelAdvice(Id)
{
    window.location.href='CustAdviceAdd.aspx?ID='+Id+'&myAction=edit&ListType=8&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021901';
}

//***********************客户建议js End****************************************


//***********************客户来电js Begin****************************************
function Cust_ld(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_ld="+pageIndex+"&pageCount_ld="+pageCount_ld+"&action=Cust_ld&orderby_ld="+orderby_ld+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_ld").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_ld tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' title='"+ item.CustName +"'>" + SubValue(12,item.CustName) + "</td>"+
                            "<td height='22' align='center'><a href='#' onclick=SelCall('"+item.ID+"')>" + item.Tel + "</a></td>"+
                            "<td height='22' align='center' title='"+ item.Title +"'>"+ SubValue(12,item.Title) +"</td>"+
                            "<td height='22' align='center'>" + item.CallTime + "</td>"+
                            "<td height='22' align='center'>" + item.Callor + "</td>"+
                            "<td height='22' align='center'>" + item.EmployeeName + "</td>").appendTo($("#pageDataList_ld tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_ld",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_ld",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_ld,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_ld({pageindex});return false;"}//[attr]
                    );
                  totalRecord_ld = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_ld").val(pageCount_ld);
                  ShowTotalPage(msg.totalCount,pageCount_ld,pageIndex,$("#pagecount_ld"));
                 
                  $("#ToPage_ld").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_ld").show();Ifshow_ld(document.getElementById("Text8").value);pageDataList1_ld("pageDataList_ld","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_ld(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_ld(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_ld").style.display = "none";
        document.getElementById("pagecount_ld").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_ld").style.display = "block";
        document.getElementById("pagecount_ld").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_ld(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_ld-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_ld=parseInt(newPageCount);
        Cust_ld(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_ld(orderColum,orderTip)
{
    if (totalRecord_ld == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_ld = orderColum+"_"+ordering;    
    Cust_ld(1);
}
function SelCall(Id)
{
    window.location.href='CustCall_Edit.aspx?ID='+Id+'&ListType=9&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2021201';
}

//***********************客户来电js End****************************************

//***********************购买记录js Begin**************************************
function Cust_gm(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_gm="+pageIndex+"&pageCount_gm="+pageCount_gm+"&action=Cust_gm&orderby_gm="+orderby_gm+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_gm").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_gm tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.OrderDate + "</td>"+
                            "<td height='22' align='center'>" + item.OrderNo + "</td>"+
                            "<td height='22' align='center'>"+ item.RealTotal +"</td>"+                           
                            "<td height='22' align='center'><a href='#' onclick=SelOrder('"+item.ID+"')>" + '详情' + "</a></td>").appendTo($("#pageDataList_gm tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_gm",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_gm",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_gm,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_gm({pageindex});return false;"}//[attr]
                    );
                  totalRecord_gm = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_gm").val(pageCount_gm);
                  ShowTotalPage(msg.totalCount,pageCount_gm,pageIndex,$("#pagecount_gm"));
                 
                  $("#ToPage_gm").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_gm").show();Ifshow_gm(document.getElementById("Text8").value);pageDataList1_gm("pageDataList_gm","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_gm(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_gm(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_gm").style.display = "none";
        document.getElementById("pagecount_gm").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_gm").style.display = "block";
        document.getElementById("pagecount_gm").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_gm(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_gm-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_gm=parseInt(newPageCount);
        Cust_gm(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_gm(orderColum,orderTip)
{
    if (totalRecord_gm == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_gm = orderColum+"_"+ordering;    
    Cust_gm(1);
}

function SelOrder(Id)
{
    window.location.href='../SellManager/SellOrderMod.aspx?id='+Id+'&intFromType=6&ListType=10&custID='+CustID
    +'&custNo='+CustNo+'&ModuleID=2031501';
}
//***********************购买记录js End****************************************

//***********************发货记录js begin****************************************
function Cust_fh(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_fh="+pageIndex+"&pageCount_fh="+pageCount_fh+"&action=Cust_fh&orderby_fh="+orderby_fh+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_fh").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_fh tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.SendDate + "</td>"+
                            "<td height='22' align='center'>" + item.SendNo + "</td>"+
                            "<td height='22' align='center'>" + item.ProductName + "</td>"+
                            "<td height='22' align='center'>"+ item.Specification +"</td>"+
                            "<td height='22' align='center'>" + item.TypeName + "</td>"+ 
                            "<td height='22' align='center'>" + item.ProductCount + "</td>").appendTo($("#pageDataList_fh tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_fh",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_fh",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_fh,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_fh({pageindex});return false;"}//[attr]
                    );
                  totalRecord_fh = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_fh").val(pageCount_fh);
                  ShowTotalPage(msg.totalCount,pageCount_fh,pageIndex,$("#pagecount_fh"));
                 
                  $("#ToPage_fh").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_fh").show();Ifshow_fh(document.getElementById("Text8").value);pageDataList1_fh("pageDataList_fh","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_fh(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_fh(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_fh").style.display = "none";
        document.getElementById("pagecount_fh").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_fh").style.display = "block";
        document.getElementById("pagecount_fh").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_fh(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_fh-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_fh=parseInt(newPageCount);
        Cust_fh(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_fh(orderColum,orderTip)
{
    if (totalRecord_fh == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_fh = orderColum+"_"+ordering;    
    Cust_fh(1);
}

//***********************发货记录js End****************************************

//***********************回款计划js begin****************************************
function Cust_hk(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_hk="+pageIndex+"&pageCount_hk="+pageCount_hk+"&action=Cust_hk&orderby_hk="+orderby_hk+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_hk").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_hk tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.PlanGatherDate + "</td>"+
                            "<td height='22' align='center'>" + item.GatheringNo + "</td>"+
                            "<td height='22' align='center'>" + item.PlanPrice + "</td>"+
                            "<td height='22' align='center' >"+ item.FactGatherDate +"</td>"+
                            "<td height='22' align='center'>" + item.FactPrice + "</td>"+
                            "<td height='22' align='center'>" + item.State + "</td>").appendTo($("#pageDataList_hk tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_hk",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_hk",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_hk,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_hk({pageindex});return false;"}//[attr]
                    );
                  totalRecord_hk = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_hk").val(pageCount_hk);
                  ShowTotalPage(msg.totalCount,pageCount_hk,pageIndex,$("#pagecount_hk"));
                 
                  $("#ToPage_hk").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_hk").show();Ifshow_hk(document.getElementById("Text8").value);pageDataList1_hk("pageDataList_hk","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_hk(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_hk(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_hk").style.display = "none";
        document.getElementById("pagecount_hk").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_hk").style.display = "block";
        document.getElementById("pagecount_hk").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_hk(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_hk-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_hk=parseInt(newPageCount);
        Cust_hk(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_hk(orderColum,orderTip)
{
    if (totalRecord_hk == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_hk = orderColum+"_"+ordering;    
    Cust_hk(1);
}
//***********************回款计划js End****************************************

//***********************收款单js begin****************************************
function Cust_jl(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_jl="+pageIndex+"&pageCount_jl="+pageCount_jl+"&action=Cust_jl&orderby_jl="+orderby_jl+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_jl").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_jl tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' >" + item.AcceDate + "</td>"+
                            "<td height='22' align='center'>" + item.TotalPrice + "</td>"+
                            "<td height='22' align='center' >"+ item.AcceWay +"</td>"+
                            "<td height='22' align='center'>" + item.InvoiceType + "</td>"+
                            "<td height='22' align='center'>" + item.BillingNum + "</td>"+
                            "<td height='22' align='center'>" + item.CreateDate + "</td>").appendTo($("#pageDataList_jl tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_jl",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_jl",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_jl,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_jl({pageindex});return false;"}//[attr]
                    );
                  totalRecord_jl = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_jl").val(pageCount_jl);
                  ShowTotalPage(msg.totalCount,pageCount_jl,pageIndex,$("#pagecount_jl"));
                 
                  $("#ToPage_jl").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_jl").show();Ifshow_jl(document.getElementById("Text8").value);pageDataList1_jl("pageDataList_jl","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_jl(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_jl(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_jl").style.display = "none";
        document.getElementById("pagecount_jl").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_jl").style.display = "block";
        document.getElementById("pagecount_jl").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_jl(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_jl-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_jl=parseInt(newPageCount);
        Cust_jl(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_jl(orderColum,orderTip)
{
    if (totalRecord_jl == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_jl = orderColum+"_"+ordering;    
    Cust_jl(1);
}
//***********************收款单js End****************************************

//***********************销售机会列表js begin****************************************
function Cust_jh(pageIndex)
{
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustColligate.ashx',//目标地址
           cache:false,
           data: "pageIndex_jh="+pageIndex+"&pageCount_jh="+pageCount_jh+"&action=Cust_jh&orderby_jh="+orderby_jh+
                    "&CustID="+escape(CustID)+"&Tel="+escape(Tel)+"&CreatedBegin="+escape(CreatedBegin)+
                    "&CreatedEnd="+escape(CreatedEnd),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager_jh").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList_jh tbody").find("tr.newrow").remove();                    
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                      {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' >" + item.ChanceNo + "</td>"+
                            "<td height='22' align='center'>" + item.Title + "</td>"+
                            "<td height='22' align='center' >"+ item.EmployeeName +"</td>"+
                            "<td height='22' align='center'>" + item.FindDate + "</td>"+
                            "<td height='22' align='center'>" + item.PhaseName + "</td>"+
                            "<td height='22' align='center'>" + item.StateName + "</td>").appendTo($("#pageDataList_jh tbody"));
                      }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager_jh",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark_jh",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount_jh,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"Cust_jh({pageindex});return false;"}//[attr]
                    );
                  totalRecord_jh = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text8").value=msg.totalCount;                 
                  
                  $("#ShowPageCount_jh").val(pageCount_jh);
                  ShowTotalPage(msg.totalCount,pageCount_jh,pageIndex,$("#pagecount_jh"));
                 
                  $("#ToPage_jh").val(pageIndex);
                  
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){hidePopup();$("#pageDataList1_Pager_jh").show();Ifshow_jh(document.getElementById("Text8").value);pageDataList1_jh("pageDataList_jh","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}

//table行颜色
function pageDataList1_jh(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow_jh(count)
{
    if(count=="0")
    {
        document.getElementById("divpage_jh").style.display = "none";
        document.getElementById("pagecount_jh").style.display = "none";
    }
    else
    {
        document.getElementById("divpage_jh").style.display = "block";
        document.getElementById("pagecount_jh").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndex_jh(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_jh-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {     
        this.pageCount_jh=parseInt(newPageCount);
        Cust_jh(parseInt(newPageIndex));
    }
}
//排序
function OrderBy_jh(orderColum,orderTip)
{
    if (totalRecord_jh == 0) 
     {
        return;
     }
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderby_jh = orderColum+"_"+ordering;    
    Cust_jh(1);
}
//***********************销售机会列表js End****************************************