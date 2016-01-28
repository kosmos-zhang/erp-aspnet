using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Data.OleDb;

using XBase.Business.Office.CustManager;


public partial class Pages_Office_CustManager_CustomContactImport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        string ErrorMsg = string.Empty;

        #region 上传文件包含验证
        /*获取公司的上传路径*/
        string FileUrl = XBase.Business.Office.SupplyChain.ProductInfoBus.GetCompanyUpFilePath(UserInfo.CompanyCD);



        /*验证该公司路径是否存在 不存在则创建*/
        DirectoryInfo dir = new DirectoryInfo(FileUrl);
        if (!dir.Exists)
        {
            try
            {
                dir.Create();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                return;
            }
        }
        /*验证是否选择了文件*/
        if (string.IsNullOrEmpty(fileExportExcel.PostedFile.FileName))
        {
            lblMsg.Text = "请选择需要导入的Excel文件";
            return;
        }

        /*验证文件类型*/
        string FileExtension = fileExportExcel.PostedFile.FileName.Split('.')[1].ToUpper();
        if (FileExtension != "XLS" && FileExtension != "XLSX")
        {
            lblMsg.Text = "文件类型错误，请上传正确的Excel文件";
            return;
        }

        /*上传文件*/
        string FileName = Guid.NewGuid().ToString() + "." + FileExtension.ToLower();
        string FileNewUrl = FileUrl + "\\" + FileName;
        try
        {
            fileExportExcel.PostedFile.SaveAs(FileNewUrl);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.ToString();
            return;
        }

        #endregion

        #region 将Excel转换为DataTable
        /*将Excel转换成DataTable*/
        System.Data.DataTable dt = ExcelToDataTable(FileNewUrl);

        /*验证Excel是否为空*/
        if (dt == null || dt.Rows.Count <= 0)
        {
            lblMsg.Text = "Excel文件为空，请重新上传Excel文件";
            try
            {
                //删除上传的Excel文件
                DelExcel(FileNewUrl);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                return;
            }
            return;
        }

        //转换成功，删除上传的Excel文件、
        try
        {
            DelExcel(FileNewUrl);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.ToString();
        }


        #endregion

        #region 重新设置列名
        int m = 0;
        dt.Columns[m++].ColumnName = "CustName";
        dt.Columns[m++].ColumnName = "ContactName";
        dt.Columns[m++].ColumnName = "Sex";
        dt.Columns[m++].ColumnName = "Important";
        dt.Columns[m++].ColumnName = "TypeName";
        dt.Columns[m++].ColumnName = "Age";
        dt.Columns[m++].ColumnName = "Operation";
        dt.Columns[m++].ColumnName = "Company";
        dt.Columns[m++].ColumnName = "Position";
        dt.Columns[m++].ColumnName = "Department";
        dt.Columns[m++].ColumnName = "WorkTel";
        dt.Columns[m++].ColumnName = "Handset";
        dt.Columns[m++].ColumnName = "Fax";
        dt.Columns[m++].ColumnName = "HomeTel";
        dt.Columns[m++].ColumnName = "MailAddress";
        #endregion

        #region 重新构造数据源
        //DataTable dtNew = new DataTable();
        //dtNew.Columns.Add(new DataColumn("custNo", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("LinkType", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("LinkManName", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Sex", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Important", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Age", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Operation", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Company", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Position", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Department", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("WorkTel", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Fax", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("Handset", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("HomeTel", typeof(string)));
        //dtNew.Columns.Add(new DataColumn("MailAddress", typeof(string)));
        #endregion

        #region 数据验证,并重新构造实体列表
        List<XBase.Model.Office.CustManager.LinkManModel> modelList = new List<XBase.Model.Office.CustManager.LinkManModel>();
        int index = 2;
        foreach (DataRow row in dt.Rows)
        {
            #region 对应客户验证
            string custNo = string.Empty;
            if (string.IsNullOrEmpty(row["CustName"].ToString()))
                ErrorMsg += "对应客户不能为空。<br />";
            else
            {
                custNo = CustomContactImportBus.CustomExists(UserInfo.CompanyCD, row["CustName"].ToString());
                if (string.IsNullOrEmpty(custNo))
                    ErrorMsg += "所填写的客户在系统中不存在。<br />";
            }
            #endregion

            #region 验证联系人姓名
            if (string.IsNullOrEmpty(row["ContactName"].ToString()))
                ErrorMsg += "联系人姓名不能为空。<br />";
            #endregion

            #region 验证联系人类型
            string typeName = string.Empty;
            if (!string.IsNullOrEmpty(row["TypeName"].ToString()))
            {
                typeName = CustomContactImportBus.ContactExists(UserInfo.CompanyCD, row["TypeName"].ToString());
                if (string.IsNullOrEmpty(typeName))
                    ErrorMsg += "所填写的联系人类型在系统中不存在。<br />";
            }
            else
                typeName = "0";
            #endregion

            #region 客户重要程度
            string Important = "0";
            if (!string.IsNullOrEmpty(row["Important"].ToString()))
            {
                switch (row["Important"].ToString())
                {
                    case "": Important = "0"; break;
                    case "不重要": Important = "1"; break;
                    case "普通": Important = "2"; break;
                    case "重要": Important = "3"; break;
                    case "关键": Important = "4"; break;
                    default: ErrorMsg += "联系人重要程度不正确。<br />"; break;
                }
            }
            #endregion


            #region 性别
            string sex = string.Empty;
            if (string.IsNullOrEmpty(row["Sex"].ToString()))
            {
                ErrorMsg += "性别不能为空。<br />";
            }
            else
            {
                switch (row["Sex"].ToString())
                {
                    case "男": sex = "1"; break;
                    case "女": sex = "2"; break;
                    default: ErrorMsg += "性别只能为男或女。<br />"; break;
                }
            }
            #endregion

            #region 若无错误信息，则将该行数据添加至新的数据源,反之则保存错误信息
            if (string.IsNullOrEmpty(ErrorMsg))
            {
                XBase.Model.Office.CustManager.LinkManModel model = new XBase.Model.Office.CustManager.LinkManModel();
                model.CustNo = custNo;
                model.LinkType = Convert.ToInt32(typeName);
                model.LinkManName = row["ContactName"].ToString();
                model.Sex = sex;
                model.Important = Important;
                model.Age = row["Age"].ToString();
                model.Operation = row["Operation"].ToString();
                model.Company = row["Company"].ToString();
                model.Position = row["Position"].ToString();
                model.Department = row["Department"].ToString();
                model.WorkTel = row["WorkTel"].ToString();
                model.Fax = row["Fax"].ToString();
                model.Handset = row["Handset"].ToString();
                model.HomeTel = row["HomeTel"].ToString();
                model.MailAddress = row["MailAddress"].ToString();
                model.CreatedDate = DateTime.Now.ToString();
                model.Creator = UserInfo.EmployeeID.ToString();
                model.CanViewUser = UserInfo.EmployeeID.ToString() + ".";
                model.CompanyCD = UserInfo.CompanyCD;
                modelList.Add(model);
            }
            else
            {
                ErrorMsg = "第 " + index.ToString() + " 行中存在的错误：<br />" + ErrorMsg;
            }
            #endregion

            index++;
        }
        #endregion

        #region 添加数据
        if (string.IsNullOrEmpty(ErrorMsg))
        {
            try
            {
                bool res = CustomContactImportBus.ImportContact(modelList);
                if (res)
                    lblMsg.Text = "数据导入成功！";
                else
                    lblMsg.Text = "数据导入失败！";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }

        }
        else
        {
            lblMsg.Text = "数据导入失败，原因如下：<br />" + ErrorMsg;
        }

        #endregion


    }

    /*将Excel转换成datatable */
    protected System.Data.DataTable ExcelToDataTable(string FileUrl)
    {
        /*转换成DataTable*/
        string SheetName = "Sheet1";//默认为sheet1
        string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + FileUrl + ";Extended Properties='Excel 8.0;IMEX=1'";
        string strExcel = string.Format("select * from [{0}$]", SheetName);
        DataSet ds = new DataSet();
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
                adapter.Fill(ds, SheetName);
                conn.Close();
            }
        }
        catch
        {
        }
        System.Data.DataTable dt = ds.Tables[SheetName];
        return dt;
    }


    /*删除上传的Excel*/
    protected void DelExcel(string FileUrl)
    {
        FileInfo fileinfo = new FileInfo(FileUrl);
        if (fileinfo.Exists)
        {
            fileinfo.Delete();
        }
    }
}
