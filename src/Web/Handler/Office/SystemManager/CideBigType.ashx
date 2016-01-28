<%@ WebHandler Language="C#" Class="CideBigType" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Common;
public class CideBigType : BaseHandler, System.Web.SessionState.IRequiresSessionState
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
            case "change":
                Change();//修改记录
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    //LoadData();     
    private void LoadData()
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string CompanyCD = "AAAAAA";
        string TableName = GetParam("TableName");
       string typeFlag=GetParam("typeFlag");
        DataTable dt = CategorySetBus.GetCodeBigFlagType(CompanyCD, TableName);
        string Codename="";
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        if (TableName == "officedba.CodeProductType")
        {
            for (int i = 1; i < 8; i++)
            {
                DataView dataView = dt.DefaultView;
                dataView.RowFilter = "TypeFlag='" + i + "'";
                DataTable dtnew = new DataTable();
                dtnew = dataView.ToTable();

                if (sb[sb.Length - 1] == '}')
                    sb.Append(",");
                sb.Append("{");
                sb.Append("TypeFlag:" + i.ToString() + ",");
                switch (i.ToString())
                {
                    case "1":
                        Codename = "成品";
                        break;
                    case "2":
                        Codename = "原材料";
                        break;
                    case "3":
                        Codename = "固定资产";
                        break;
                    case "4":
                        Codename = "低值易耗";
                        break;
                    case "5":
                        Codename = "包装物";
                        break;
                    case "6":
                        Codename = "服务产品";
                        break;
                    case "7":
                        Codename = "半成品";
                        break;
                }


                sb.Append("CodeName:'" + Codename + "',");
                sb.Append("SubNodes:[");
                foreach (DataRow row in dtnew.Select("SupperID=0"))
                {
                    if (sb[sb.Length - 1] == '}')
                        sb.Append(",");

                    sb.Append("{");
                    sb.Append("ID:" + row["ID"].ToString() + ",");
                    sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
                    sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
                    sb.Append("TypeFlag:" + row["TypeFlag"].ToString() + ",");
                    sb.Append("Description:'" + row["Description"].ToString() + "',");
                    sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");
                    sb.Append("SubNodes:[");
                    LoadSubData(row["ID"].ToString(), sb, dtnew);
                    sb.Append("]");

                    sb.Append("}");
                }
                sb.Append("]");

                sb.Append("}");
            }
           
           
        }
        if (TableName == "officedba.CodeCompanyType")
        {
            if (typeFlag == "5")//供应链设置
            {
                for (int i = 2; i < 8; i++)
                {
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "TypeFlag='" + i + "'";
                    DataTable dtnew = new DataTable();
                    dtnew = dataView.ToTable();

                    if (sb[sb.Length - 1] == '}')
                        sb.Append(",");
                    sb.Append("{");
                    sb.Append("TypeFlag:" + i.ToString() + ",");
                    switch (i.ToString())
                    {
                        case "2":
                            Codename = "供应商";
                            break;
                        case "3":
                            Codename = "竞争对手";
                            break;
                        case "4":
                            Codename = "银行";
                            break;
                        case "5":
                            Codename = "外协加工厂";
                            break;
                        case "6":
                            Codename = "运输商";
                            break;
                        case "7":
                            Codename = "其他";
                            break;
                    }
                    sb.Append("CodeName:'" + Codename + "',");
                    sb.Append("SubNodes:[");
                    foreach (DataRow row in dtnew.Select("SupperID=0"))
                    {
                        if (sb[sb.Length - 1] == '}')
                            sb.Append(",");

                        sb.Append("{");
                        sb.Append("ID:" + row["ID"].ToString() + ",");
                        sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
                        sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
                        sb.Append("TypeFlag:" + row["TypeFlag"].ToString() + ",");
                        sb.Append("Description:'" + row["Description"].ToString() + "',");
                        sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");
                        sb.Append("SubNodes:[");
                        LoadSubData(row["ID"].ToString(), sb, dtnew);
                        sb.Append("]");

                        sb.Append("}");
                    }
                    sb.Append("]");

                    sb.Append("}");
                }
            }
            else if (typeFlag == "4")
            {
                int i=1;
                    DataView dataView = dt.DefaultView;
                    dataView.RowFilter = "TypeFlag='" + i + "'";
                    DataTable dtnew = new DataTable();
                    dtnew = dataView.ToTable();

                    if (sb[sb.Length - 1] == '}')
                        sb.Append(",");
                    sb.Append("{");
                    sb.Append("TypeFlag:" + i.ToString() + ",");
                    switch (i.ToString())
                    {
                        case "1":
                              Codename = "客户";
                            break;
                    }
                    sb.Append("CodeName:'" + Codename + "',");
                    sb.Append("SubNodes:[");
                    foreach (DataRow row in dtnew.Select("SupperID=0"))
                    {
                        if (sb[sb.Length - 1] == '}')
                            sb.Append(",");

                        sb.Append("{");
                        sb.Append("ID:" + row["ID"].ToString() + ",");
                        sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
                        sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
                        sb.Append("TypeFlag:" + row["TypeFlag"].ToString() + ",");
                        sb.Append("Description:'" + row["Description"].ToString() + "',");
                        sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");
                        sb.Append("SubNodes:[");
                        LoadSubData(row["ID"].ToString(), sb, dtnew);
                        sb.Append("]");

                        sb.Append("}");
                    }
                    sb.Append("]");

                    sb.Append("}");
                }
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
            sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
            sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
            sb.Append("TypeFlag:" + row["TypeFlag"].ToString() + ",");
            sb.Append("Description:'" + row["Description"].ToString() + "',");
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
        string UsedStatus = GetParam("UsedStatus");
        string TypeFlag = GetParam("TypeFlag");
        string TableName = GetParam("TableName");
        model.CodeName = CodeName;
        //model.CompanyCD = "AAAAAA";
        model. CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.Description = Description;
        model.ModifiedDate = DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.SupperID = int.Parse(SupperID);
        model.UsedStatus = UsedStatus;
        model.BigType = TypeFlag;
        int result = CategorySetBus.InsertCodeBigFlagInfo(model,TableName);
        if (result > 0)
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
        string TableName = GetParam("TableName");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }
        int result = CategorySetBus.DeleteCodeBigType(int.Parse(ID),TableName);
        if (result > 0)
            OutputResult(true, "删除成功！");
        else if(result==-1)
            OutputResult(false, "删除失败，请检查数据是否在使用中！");
        else
            OutputResult(false, "删除失败！");
    }
    /// <summary>
    /// 修改记录
    /// </summary>
    private void EditItem()
    {
        string ID = GetParam("ID");
        string TableName = GetParam("TableName");
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
            OutputResult(false, "未指定TypeName");
            return;
        }

        string SupperTypeID = GetParam("SupperTypeID");
        if (SupperTypeID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");
            return;
        }
        model = CategorySetBus.GetCodeBigTypebyId(int.Parse(ID),TableName);
        model.ModifiedDate = DateTime.Now;
        model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string SupperID = GetParam("SupperTypeID");
        if (SupperID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");
            return;
        }
        model.Description = GetParam("Description");
        model.UsedStatus = GetParam("UsedStatus");
        model.CodeName = TypeName;
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.SupperID = int.Parse(SupperID);
        model.ID = int.Parse(ID);
        model.BigType = GetParam("TypeFlag");
        int result = CategorySetBus.UpdateCodeBigTypeInfo(model,TableName);
        if (result > 0)
            OutputResult(true, "修改成功！");
        else
            OutputResult(false, "修改失败！");
    }
    private void  Change()
    {
        StringBuilder sb = new StringBuilder();
        string SupperTypeID = GetParam("flag");
        if (SupperTypeID == string.Empty)
        {
            OutputResult(false, "未指定SupperTypeID");

        }
        string TableName = GetParam("TableName");



        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //string CompanyCD = "AAAAAA";
        DataTable dt = CategorySetBus.GetCodeBigFlagType(CompanyCD, TableName);
        DataView dataView = dt.DefaultView;
        dataView.RowFilter = "TypeFlag='" + SupperTypeID + "'";
        DataTable dtnew = new DataTable();
        dtnew = dataView.ToTable();
        foreach (DataRow row in dtnew.Rows)
        {
            sb.Append("{");
            sb.Append("ID:" + row["ID"].ToString() + ",");
            sb.Append("CodeName:'" + row["CodeName"].ToString() + "',");
            sb.Append("SupperID:" + row["SupperID"].ToString() + ",");
            sb.Append("TypeFlag:" + row["TypeFlag"].ToString() + ",");
            sb.Append("Description:'" + row["Description"].ToString() + "',");
            sb.Append("UsedStatus:" + row["UsedStatus"].ToString() + ",");
            sb.Append("}");
        }
    
        Output("{result:true,data:" + sb.ToString() + "}");
        //model = CategorySetBus.GetCodeBigTypebyId(int.Parse(SupperTypeID), TableName);
        //if (model != null)
        //{
        //  typeflag = model.BigType;
        //}
        //Output("{result:true,data:" + typeflag + "}");
    }
    
}