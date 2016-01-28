<%@ WebHandler Language="C#" Class="PurchaseApply" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;

public class PurchaseApply : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
    //    //获得登录页面POST过来的参数 
    //    string action = context.Request.Params["action"].ToString();
    //    PurchaseApplyModel PurchaseApplyM = new PurchaseApplyModel();
    //    PurchaseApplyDetailSourceModel PurchaseApplyDetailSourceM = new PurchaseApplyDetailSourceModel();
    //    PurchaseApplyDetailModel PurchaseApplyDetailM = new PurchaseApplyDetailModel();

    //string userID = "fdsdf";// ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//用户ID
    //string CompanyCD = "feeef";// ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

    //string applyNo = context.Request.Params["applyNo"].ToString().Trim();
        
    //    string equipnum = context.Request.Params["strequipnum"].ToString().Trim();//设备编号
    //    string equipcode = context.Request.Params["strequipcode"].ToString().Trim();//设备序列号
    //    string equipname = context.Request.Params["strequipname"].ToString().Trim();//设备名称
    //    string equipbuydate = context.Request.Params["strequipbuydate"].ToString().Trim().Replace("-", "");//购入日期
    //    string equipcheckdate = context.Request.Params["strequipcheckdate"].ToString().Trim().Replace("-", "");//检定日期
    //    string equipnorm = context.Request.Params["strequipnorm"].ToString().Trim();//规格类型
    //    string equipprecision = context.Request.Params["strequipprecision"].ToString().Trim();//精度
    //    string equiptype = context.Request.Params["strequiptype"].ToString().Trim();//设备类型
    //    string equipprovider = context.Request.Params["strequipprovider"].ToString().Trim();//供应商
    //    string equipkeep = context.Request.Params["strequipkeep"].ToString().Trim();//保修期
    //    string equipequipcurruser = context.Request.Params["strequipequipcurruser"].ToString().Trim();//当前使用人
    //    string equipcurrdept = context.Request.Params["strequipcurrdept"].ToString().Trim();//当前部门
    //    string equipmix = context.Request.Params["strequipmix"].ToString().Trim();//有无配件
    //    string equipamount = context.Request.Params["strequipamount"].ToString().Trim();//总量
    //    string equipalarm = context.Request.Params["strequipalarm"].ToString().Trim();//报警下限
    //    string equipstatus = context.Request.Params["strequipstatus"].ToString().Trim();//设备状态
    //    string equipdetial = context.Request.Params["strequipdetial"].ToString().Trim();//设备详细
    //    string equipremark = context.Request.Params["strequipremark"].ToString().Trim();//备注
    //    //配件信息
    //    string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();//附件
    //    string[] strarray = null;
    //    string recorditems = "";
    //    string[] inseritems = null;
    //    EquipmentInfoM.EquipmentNo = equipnum;
    //    EquipmentInfoM.EquipmentIndex = equipcode;
    //    EquipmentInfoM.EquipmentName = equipname;
    //    EquipmentInfoM.CompanyCD = CompanyID;
    //    EquipmentInfoM.Norm = equipnorm;
    //    EquipmentInfoM.Precision = equipprecision;
    //    EquipmentInfoM.BuyDate = equipbuydate;
    //    EquipmentInfoM.ExaminePeriod = equipcheckdate;
    //    EquipmentInfoM.Provider = equipprovider;
    //    EquipmentInfoM.Type = equiptype;
    //    EquipmentInfoM.Warranty = equipkeep;
    //    EquipmentInfoM.CurrentUser = equipequipcurruser;
    //    EquipmentInfoM.CurrentDepartment = equipcurrdept;
    //    EquipmentInfoM.FittingFlag = equipmix == "" ? "0" : equipmix;
    //    EquipmentInfoM.TotalCount = equipamount == "" ? 0 : Convert.ToDecimal(equipamount);
    //    EquipmentInfoM.WarningLimit = equipalarm == "" ? 0 : Convert.ToInt32(equipalarm);
    //    EquipmentInfoM.Status = EnumType.Free;
    //    EquipmentInfoM.EquipmentDetail = equipdetial;
    //    EquipmentInfoM.EquipmentRemark = equipremark;

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}