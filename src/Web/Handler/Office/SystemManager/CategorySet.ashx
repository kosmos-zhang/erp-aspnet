<%@ WebHandler Language="C#" Class="CategorySet" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Common;
public class CategorySet : BaseHandler, System.Web.SessionState.IRequiresSessionState
{
    CodeEquipmentTypeModel model = new CodeEquipmentTypeModel();   
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "loaddata":
                LoadData();//加载数据
                break;
            case "delitem":
                DelItem();//删除记录
                break;
            case "additem":
                AddItem();//添加记录
                break;
            case "edititem":
                EditItem();//修改记录
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
        //LoadData();     
    private  void LoadData()
    {
        //nodes = {text:'',value:'',subNodes:[]}
        //string CompanyCD="AAAAAA";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string flag = GetParam("Flag");
        DataTable dt = CategorySetBus.GetCodeEquipmentType(CompanyCD, flag);
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow row in dt.Select("SupperID=0"))
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("ID:" + row["ID"].ToString() + ",");
            sb.Append("TypeFlag:'" + row["TypeFlag"].ToString() + "',");
            sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
            sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
            sb.Append("Description:'" + row["Description"].ToString() + "',");
            sb.Append("WarningLimit:" + row["WarningLimit"].ToString() + ",");
            sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");

            sb.Append("SubNodes:[");
            LoadSubData(row["ID"].ToString(), sb, dt);
            sb.Append("]");

            sb.Append("}");
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");

    }

    private void LoadSubData(string pid, StringBuilder sb, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("ID:" + row["ID"].ToString() + ",");
            sb.Append("TypeFlag:'" + row["TypeFlag"].ToString() + "',");
            sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
            sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
            sb.Append("Description:'" + row["Description"].ToString() + "',");
            sb.Append("WarningLimit:" + row["WarningLimit"].ToString() + ",");
            sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");

            sb.Append("SubNodes:[");
            LoadSubData(row["ID"].ToString(), sb, dt);
            sb.Append("]");

            sb.Append("}");
        }
    }

    /// <summary>
    /// 添加项目
    /// </summary>
    private void AddItem()
    {
        string Flag = GetParam("Flag");
        if (Flag == string.Empty)
        {
            OutputResult(false, "未指定Flag");
            return;
        }

        string CodeName = GetParam("TypeName");
        if (CodeName == string.Empty)
        {
            OutputResult(false, "请填写类别名称");
            return;
        }

        string SupperID = GetParam("SupperTypeID");
        if (SupperID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");
            return;
        }
        string Description = GetParam("Description");
        string WarningLimit = GetParam("WarningLimit");
        string UsedStatus = GetParam("UsedStatus");
        string flag = GetParam("flagtype");

        model.CodeName = CodeName;
        //model.CompanyCD = "AAAAAA";
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.Description = Description;
        model.ModifiedDate = DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        //model.ModifiedUserID = "admin";
        model.SupperID = int.Parse(SupperID);
        if (!string.IsNullOrEmpty(WarningLimit))
            model.WarningLimit = int.Parse(GetParam("WarningLimit"));
        model.UsedStatus = UsedStatus;
        model.TypeFlag = flag == "1" ? ConstUtil.CodeEquipmentType_Office_Flag : ConstUtil.CodeEquipmentType_Equipment_Flag;
     int result= CategorySetBus.InsertCodeEquipmentInfo(model);
     if (result>0)
         OutputResult(true, "添加成功！");
      else
         OutputResult(false, "添加失败！");
    }
    /// <summary>
    /// 删除设备种类
    /// </summary>
    private void DelItem()
    {
        string ID = GetParam("ID");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }
        int result = CategorySetBus.DeleteCodeEquipmentInfo(int.Parse(ID));
        if (result > 0)
            OutputResult(true, "删除成功！");
        else
            OutputResult(false, "修改失败！");
    }
    /// <summary>
    /// 修改记录
    /// </summary>
    private void EditItem()
    {
        string ID = GetParam("ID");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }

        string Flag = GetParam("Flag");
        if (Flag == string.Empty)
        {
            OutputResult(false, "未指定Flag");
            return;
        }

        string TypeName = GetParam("TypeName");
        if (TypeName == string.Empty)
        {
            OutputResult(false, "请填写类别名称");
            return;
        }

        string SupperTypeID = GetParam("SupperTypeID");
        if (SupperTypeID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");
            return;
        }
        model = CategorySetBus.GetCodeEuipment(int.Parse(ID));
        model.ModifiedDate = DateTime.Now;
        //model.ModifiedUserID = "admin";
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string SupperID = GetParam("SupperTypeID");
        if (SupperID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");
            return;
        }
        model. Description = GetParam("Description");
        string WarningLimit = GetParam("WarningLimit");
        if (!string.IsNullOrEmpty(WarningLimit))
        model. WarningLimit =int.Parse( GetParam("WarningLimit"));
        model. UsedStatus = GetParam("UsedStatus");
        model.CodeName = TypeName;
        //model.CompanyCD = "AAAAAA";
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.SupperID = int.Parse(SupperID);
        model.ID = int.Parse(ID);
        int result = CategorySetBus.UpdateCodeEquipmentInfo(model);
        if (result > 0)
            OutputResult(true, "修改成功！");
        else
            OutputResult(false, "修改失败！");
    }
    
    
}
